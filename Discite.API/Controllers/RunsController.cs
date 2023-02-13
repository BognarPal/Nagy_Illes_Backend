using Discite.Data.Repositories;
using Discite.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Controllers
{
    public class RunsController : BaseApiController
    {
        RunRepository runRepository;

        public RunsController(RunRepository runRepository)
        {
            this.runRepository = runRepository;
        }

        [HttpGet]
        public IActionResult Load(int rid)
        {
            var run = runRepository.GetAll().SingleOrDefault(r => r.Id == rid);
            if (run == null)
                return NotFound();
            else
                return Ok(run);
        }

        [HttpPost]
        public IActionResult Save([FromBody]RunModel run)
        {
            runRepository.Update(run);
            return Ok();
        }

        [HttpPost("new")]
        public IActionResult NewGame() 
        {
            RunModel run = new RunModel();
            var crun = runRepository.Insert(run);
            return Created($"api/Runs/{crun.Id}", crun);
        }
    }
}
