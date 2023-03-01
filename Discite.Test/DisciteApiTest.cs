using Discite.Data.Models;
using Discite.Data.Repositories;
using Discite.API.DTOs;
using RestSharp;
using System.Net;

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
        public void Register()
        {
            var user = RegisterUser("asd");

        }

        [Fact]
        public void EditProfile()
        {

        }

        private UserDto? Login(string email, string password) 
        {
            RestRequest request = new RestRequest("/api/users/login", Method.Post);
            request.AddJsonBody(new LoginDto { Email = email, Password = password });

            return client.Post<UserDto>(request);
        }

        private UserDto? RegisterUser(string name)
        {
            RegisterDto registerDto = new RegisterDto() { Email = $"{name.ToLower()}@test.test", Password = name, Username = name };

            RestRequest request = new RestRequest("/api/users/register", Method.Post);

            request.AddJsonBody(registerDto);

            return client.Post<UserDto>(request);
        }

        private UserDto? RegisterAdmin()
        {
            var user = RegisterUser("admin");
            var user2 = userRepository[user.Id];
            user2.Id = 0;
            userRepository.Delete(user.Id);
            userRepository.Insert(user2);
            return new UserDto { Id= user2.Id, Email = user2.Email, Username = user2.UserName };
        }
    }
}