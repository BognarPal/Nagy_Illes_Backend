using Discite.API.DTOs;
using Discite.Data;
using Discite.Data.Models;
using Discite.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUsers()
        {
            return Ok(userRepository.GetAll());
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            return userRepository.GetAll().SingleOrDefault(U => U.Id == id);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register([FromBody]RegisterDto registerDto)
        {
            using var hmac = new HMACSHA256();

            var user = new UserModel
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                Salt = hmac.Key
            };

            return userRepository.Insert(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login([FromBody]LoginDto loginDto)
        {
            UserModel user = userRepository.GetAll().SingleOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            using var hmac = new HMACSHA256(user.Salt);

            if (!user.Hash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password))))
                return Unauthorized("Invalid password");

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(base.GetConfigValue("JWT:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = DateTime.Now.AddMinutes(int.Parse(this.GetConfigValue("JWT:ExpireInMinute"))),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Login", loginName),
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.Role, "Valami")
                })
            };
            return user;
        } 
    }
}
