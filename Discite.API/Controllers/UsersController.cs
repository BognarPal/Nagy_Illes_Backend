
using Discite.Data;
using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController(DisciteDbContext context)
        {
            
        }
    }
}
