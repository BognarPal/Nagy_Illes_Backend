using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Controllers
{
    public class StatisticsController : BaseApiController
    {

        public StatisticsController() 
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
