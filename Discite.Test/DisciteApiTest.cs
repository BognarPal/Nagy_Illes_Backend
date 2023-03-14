using Discite.Data.Models;
using Discite.Data.Repositories;
using Discite.Data.Enums;
using Discite.API.DTOs;
using RestSharp;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json.Linq;

namespace Discite.Test
{
    public class DisciteApiTest : IDisposable
    {
        UserRepository userRepository;
        string url = "http://localhost:5100";
        readonly RestClient client;

        public DisciteApiTest()
        {
            this.client = new RestClient(url);
            this.userRepository = new UserRepository();
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        [Fact]
        public void Users()
        {
            UserDto? user;
            user = _RegisterUser("asd");
            Assert.Null(user);

            user = _RegisterUser("testuser_register123");
            var sUser = _RegisterUser("testuser_register123");
            Assert.Null(sUser);

            UserDto? lUser;
            lUser = _Login("testuser_register123@test.test", "jhaskdfgjhabsdgzb");
            Assert.Null(lUser);
            lUser = _Login("testuser_register123@test.test", "testuser_register123");

            Assert.NotEqual(user.Token, lUser.Token);

            userRepository.Delete(user.Id);
            userRepository.Delete(lUser.Id);
        }

        [Fact]
        public void Runs()
        {
            var user = _RegisterUser("testuser_runs");

            Assert.NotNull(user);

            var run = _NewGame(user.Token);
            Assert.NotNull(run);
            run.Seed = new Random().Next();
            run.Path = "UDDRL";
            run.Gold = 10;
            run.ClassId = 2;
            run.CurrentHp = 50f;
            run.Score = 1000;
            run.Status = RunStatus.Alive;

            var request = new RestRequest("/api/runs", Method.Put);
            request.AddHeader("Authentication", $"Bearer {user.Token}");
            request.AddJsonBody<GameDto>(run);
            client.Put<GameDto>(request);

            var request2 = new RestRequest($"/api/runs/{run.Id}", Method.Get);
            request2.AddHeader("Authentication", $"Bearer {user.Token}");
            var run2 = client.Get<GameDto>(request);
            Assert.NotNull(run2);

            Assert.True(run.Seed == run2.Seed);
            Assert.True(run.Path == run2.Path);
            Assert.True(run.ClassId == run2.ClassId);
            Assert.True(run.CurrentHp == run2.CurrentHp);
            Assert.True(run.Score == run2.Score);
            Assert.True(run.Status == run2.Status);

            userRepository.Delete(user.Id);
        }

        [Fact]
        public void Statistics()
        {
            var toplist = client.Get<IEnumerable<ToplistDto>>(new RestRequest("/api/statistics/toplist", Method.Get));
            var classes = client.Get<IEnumerable<ClassStatsDto>>(new RestRequest("/api/statistics/classes", Method.Get));
            var weapons = client.Get<IEnumerable<WeaponStatsDto>>(new RestRequest("/api/statistics/weapons", Method.Get));
            var enemies = client.Get<IEnumerable<EnemyStatsDto>>(new RestRequest("/api/statistics/enemies", Method.Get));

            Assert.NotNull(toplist);
            Assert.NotNull(classes);
            Assert.NotNull(weapons);
            Assert.NotNull(enemies);

            Assert.True(toplist.Count() == 10);
            Assert.True(classes.Count() > 0);
            Assert.True(weapons.Count() > 0);
            Assert.True(enemies.Count() > 0);
        }

        [Fact]
        public void Configuration()
        {
            var user = _RegisterUser("testuser_config");
            var admin = _Login("admin@discite.xyz", "vML74L7eUnuKRMco");

            var oldConfig = _GetConfig();

            Assert.NotNull(oldConfig);

            var newConfig = oldConfig;

            newConfig.Enemies.First().Name = "asd";

            RestRequest _request = new RestRequest("/api/configuration", Method.Put);
            _request.AddBody(newConfig);

            var request = client.Put<ConfigurationDto>(_request);
            Assert.Null(request);

            _request.AddHeader("Authentication", $"Bearer {user.Token}");
            request = client.Put<ConfigurationDto>(_request);
            Assert.Null(request);

            _request.AddHeader("Authentication", $"Bearer {admin.Token}");
            request = client.Put<ConfigurationDto>(_request);
            Assert.NotNull(request);
        }

        private ConfigurationDto? _GetConfig()
        {
            var request = new RestRequest("/api/configuration", Method.Get);
            return client.Get<ConfigurationDto>(request);
        }

        private GameDto? _NewGame(string token)
        {
            RestRequest request = new RestRequest("/api/runs", Method.Post);
            request.AddHeader("Authentication", $"Bearer {token}");

            return client.Post<GameDto>(request);
        }

        private UserDto? _Login(string email, string password) 
        {
            RestRequest request = new RestRequest("/api/users/login", Method.Post);
            request.AddJsonBody(new LoginDto { Email = email, Password = password });

            return client.Post<UserDto>(request);
        }

        private UserDto? _RegisterUser(string name)
        {
            RegisterDto registerDto = new RegisterDto() { Email = $"{name.ToLower()}@test.test", Password = name, Username = name };

            RestRequest request = new RestRequest("/api/users/register", Method.Post);

            request.AddJsonBody(registerDto);

            return client.Post<UserDto>(request);
        }
    }
}