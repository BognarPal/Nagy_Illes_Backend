using Discite.Data.Models;
using Discite.API.DTOs;
using RestSharp;
using System.Net;

namespace Discite.Test
{
    public class DisciteApiTest : IDisposable
    {
        string url = "http://localhost:5100";
        readonly RestClient client;

        public DisciteApiTest()
        {
            this.client = new RestClient(url);
        }

        public void Dispose()
        {
            this.client.Dispose();
        }

        [Fact]
        public void RegisterUser()
        {
            RegisterDto registerDto = new RegisterDto() { Email = "asd@asd.asd", Password = "asdasdasd", Username = "asdasd"};

            RestRequest request = new RestRequest("/api/users/register", Method.Get);

            client.Get<UserDto>(request);
        }
    }
}