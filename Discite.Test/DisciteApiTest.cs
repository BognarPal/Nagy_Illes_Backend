using Discite.Data.Models;
using Discite.Data.Repositories;
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
            var user = _RegisterUser("asd");



        }

        [Fact]
        public void Runs()
        {
            var user = _RegisterUser("testuser_runs");

            //new game
            var run = _NewGame(user.Token);

            run.Seed = new Random().Next();

            run.Path = "UDDRL";


            //save game


            //load game


            userRepository.Delete(user.Id);
        }

        [Fact]
        public void Statistics()
        {
            var toplist = client.Get<IEnumerable<ToplistDto>>(new RestRequest("/api/statistics/toplist", Method.Get));
            var classes = client.Get<IEnumerable<ClassStatsDto>>(new RestRequest("/api/statistics/classes", Method.Get));
            var weapons = client.Get<IEnumerable<WeaponStatsDto>>(new RestRequest("/api/statistics/weapons", Method.Get));
            var enemies = client.Get<IEnumerable<EnemyStatsDto>>(new RestRequest("/api/statistics/enemies", Method.Get));

            Assert.Equal(10, toplist.Count());
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