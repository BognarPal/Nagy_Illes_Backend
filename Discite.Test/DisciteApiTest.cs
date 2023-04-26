using Discite.Data.Models;
using Discite.Data.Repositories;
using Discite.Data.Enums;
using Discite.API.DTOs;
using RestSharp;
using System.Net;
using Microsoft.AspNetCore.Connections;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace Discite.Test
{
    public class DisciteApiTest : IDisposable
    {
        UserRepository userRepository;
        RunRepository runRepository;
        string url = "http://localhost:5241";
        readonly RestClient client;

        public DisciteApiTest()
        {
            this.client = new RestClient(url);
            this.userRepository = new UserRepository();
            this.runRepository = new RunRepository();
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        [Fact]
        public void Users()
        {
            Assert.Throws<HttpRequestException>(() => _RegisterUser("a"));

            var user = _RegisterUser("testuser_register123");
            Assert.Throws<JsonException>(() => _RegisterUser("testuser_register123"));
            Assert.Throws<JsonException>(() => _Login("testuser_register123@test.test", "jhaskdfgjhabsdgzb"));
            var lUser = _Login("testuser_register123@test.test", "testuser_register123");

            Assert.Equal(user.Token, lUser.Token);

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
            request.AddHeader("Authorization", $"Bearer {user.Token}");
            request.AddJsonBody<GameDto>(run);
            client.Put<GameDto>(request);

            var request2 = new RestRequest($"/api/runs/{run.Id}", Method.Get);
            request2.AddHeader("Authorization", $"Bearer {user.Token}");
            var run2 = client.Get<GameDto>(request2);
            Assert.NotNull(run2);

            Assert.True(run.Seed == run2.Seed);
            Assert.True(run.Path == run2.Path);
            Assert.True(run.ClassId == run2.ClassId);
            Assert.True(run.CurrentHp == run2.CurrentHp);
            Assert.True(run.Score == run2.Score);
            Assert.True(run.Status == run2.Status);

            userRepository.Delete(user.Id);
            runRepository.Delete(run.Id);
        }

        [Fact]
        public void Statistics()
        {
            var toplist = client.Get<IEnumerable<ToplistDto>>(new RestRequest("/api/statistics/toplist", Method.Get));
            var weapons = client.Get<IEnumerable<WeaponStatsDto>>(new RestRequest("/api/statistics/weapons", Method.Get));
            var enemies = client.Get<IEnumerable<EnemyStatsDto>>(new RestRequest("/api/statistics/enemies", Method.Get));

            Assert.NotNull(toplist);
            Assert.NotNull(weapons);
            Assert.NotNull(enemies);

            Assert.True(toplist.Count() == 10);
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

            Assert.Throws<HttpRequestException>(() => client.Put<ConfigurationDto>(_request));

            _request.AddHeader("Authorization", $"Bearer {user.Token}");
            Assert.Throws<HttpRequestException>(() => client.Put<ConfigurationDto>(_request));

            _request.AddOrUpdateHeader("Authorization", $"Bearer {admin.Token}");
            var request = client.Put<ConfigurationDto>(_request);
            Assert.NotNull(request);

            userRepository.Delete(user.Id);
        }

        private ConfigurationDto? _GetConfig()
        {
            var request = new RestRequest("/api/configuration", Method.Get);
            return client.Get<ConfigurationDto>(request);
        }

        private GameDto? _NewGame(string token)
        {
            RestRequest request = new RestRequest("/api/runs", Method.Post);
            request.AddHeader("Authorization", $"Bearer {token}");

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