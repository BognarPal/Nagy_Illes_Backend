using Discite.API.Interfaces;
using Discite.API.Services;
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
using Discite.API.Extensions;

namespace Discite.API.Controllers
{
    public class UsersController : BaseApiController
    {
        UserRepository userRepository;
        ITokenService tokenService;
        public UsersController()
        {
             userRepository= new UserRepository();
             tokenService = new TokenService();
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            if (Request.uid() != 0)
                return Unauthorized();

            return Ok(userRepository.GetAll().Select(u => new UserDto() { Id = u.Id, Email = u.Email, Username = u.UserName }));
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var user = userRepository.GetAll().SingleOrDefault(U => U.Id == id);
            int uid = Request.uid();
            if (!(uid == 0 || uid == user.Id))
                return Unauthorized();

            var ruser = new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName
            };

            return ruser;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto)
        {
            using var hmac = new HMACSHA256();

            var user = new UserModel
            {
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                Salt = hmac.Key
            };

            var newUser = userRepository.Insert(user);

            return new UserDto
            {
                Id = newUser.Id,
                Username = newUser.UserName,
                Email = newUser.Email,
                Token = tokenService.CreateToken(newUser),
            };
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto)
        {
            UserModel user = userRepository.GetAll().SingleOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            using var hmac = new HMACSHA256(user.Salt);

            if (!user.Hash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password))))
                return Unauthorized("Invalid password");

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user),
            };
        } 
    }
}
