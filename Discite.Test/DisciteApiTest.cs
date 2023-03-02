using Discite.Data.Models;
using Discite.Data.Repositories;
using Discite.API.DTOs;
using RestSharp;
using System.Net;
using Microsoft.AspNetCore.Connections;

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
            var admin = _RegisterAdmin("admin123");



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

        }

        [Fact]
        public void Configuration()
        {

        }

        public GameDto? _NewGame(string token)
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

        private UserDto? _RegisterAdmin(string name)
        {
            var user = _RegisterUser(name);
            var user2 = userRepository[user.Id];
            user2.Id = 0;
            userRepository.Delete(user.Id);
            userRepository.Insert(user2);
            return new UserDto { Id= user2.Id, Email = user2.Email, Username = user2.UserName };
        }
    }
}