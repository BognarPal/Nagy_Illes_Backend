using Discite.Data;
using Discite.Data.Models;
using Discite.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Discite.API.Controllers
{
    public class UsersController : BaseApiController
    {
        UserRepository userRepository;
        public UsersController()
        {
             userRepository= new UserRepository();
        }

        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            return userRepository.GetAll();
        }

        [HttpGet("{id}")]
        public UserModel GetUsers(int id)
        {
            return userRepository.GetAll().SingleOrDefault(U => U.Id == id);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(string username, string email, string password)
        {
            using var hmac = new HMACSHA256();

            var user = new UserModel
            {
                Email = email,
                UserName = username,
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                Salt = hmac.Key
            };

            userRepository.Insert(user);

            return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(string email, string password)
        {
            UserModel user = userRepository.GetAll().SingleOrDefault(u => u.Email == email);

            if (user == null)
                return Unauthorized("Invalid email");

            using var hmac = new HMACSHA256(user.Salt);

            if (!user.Hash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(password))))
                return Unauthorized("Invalid password");

            return user;
        } 
    }
}
