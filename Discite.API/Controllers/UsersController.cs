
using Discite.Data;
using Discite.Data.Models;
using Discite.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
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

        

    }
}
