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
using Discite.API.Attributes;

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

        /// <summary>
        /// Get all user
        /// </summary>
        [HttpGet]
        [AuthorizeAdmin]
        [Route("api/user")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            return Ok(userRepository.GetAll().Select(u => new UserDto { Id = u.Id, Email = u.Email, Username = u.UserName, RegisterDate = u.RegisterDate, LastActive = u.LastActive }));
        }

        /// <summary>
        /// Get specific user
        /// </summary>
        [HttpGet]
        [Authorize]
        [Route("api/user/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(UserDto))]
        public ActionResult<UserDto> GetUser([FromRoute] int id)
        {
            var user = userRepository.GetAll().SingleOrDefault(u => u.Id == id);
            int uid = Request.uid();
            if (!(uid == 0 || uid == user.Id))
                return Unauthorized();

            var ruser = new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                RegisterDate = user.RegisterDate,
                LastActive = user.LastActive,
                Runs = user.Runs != null ? user.Runs.Select(r => r.Id) : null
            };

            return Ok(ruser);
        }

        /// <summary>
        /// Update user informations
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("api/user")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(UserDto))]
        public ActionResult<UserDto> EditUser([FromBody] RegisterDto registerDto)
        {
            using var hmac = new HMACSHA256();

            var user = new UserModel
            {
                Id = Request.uid(),
                Email = registerDto.Email,
                UserName = registerDto.Username,
                Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                Salt = hmac.Key
            };

            var uuser = userRepository.Update(user);

            var ruser = new UserDto
            {
                Id = uuser.Id,
                Email = uuser.Email,
                Username = uuser.UserName
            };

            return Ok(ruser);
        } 

        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/register")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(UserDto))]
        public ActionResult<UserDto> Register([FromBody] RegisterDto registerDto)
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

        /// <summary>
        /// Login
        /// </summary>
        [HttpPost]
        [AllowAnonymous]
        [Route("api/user/login")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(UserDto))]
        public ActionResult<UserDto> Login([FromBody] LoginDto loginDto)
        {
            UserModel user = userRepository.GetAll().SingleOrDefault(u => u.Email == loginDto.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            using var hmac = new HMACSHA256(user.Salt);

            if (!user.Hash.SequenceEqual(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password))))
                return Unauthorized("Invalid password");

            user.LastActive = DateTime.Now;
            userRepository.Update(user);

            return new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Token = tokenService.CreateToken(user),
            };
        }
        /// <summary>
        /// Ban user
        /// </summary>
        [HttpDelete]
        [Route("api/user/{id}")]
        [AuthorizeAdmin]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult BanUser([FromRoute] int id)
        {
            return Ok();
        }
    }
}
