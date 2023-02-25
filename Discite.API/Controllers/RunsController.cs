using Discite.Data.Repositories;
using Discite.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace Discite.API.Controllers
{
    public class RunsController : BaseApiController
    {
        RunRepository runRepository;

        public RunsController()
        {
            this.runRepository = new RunRepository();
        }

        [HttpPost]
        public IActionResult NewGame() 
        {
            RunModel run = new RunModel();
            run.UserId = 1;
            var crun = runRepository.Insert(run);
            return Created($"api/runs/{crun.Id}", crun);
        }

        [HttpPut]
        public IActionResult Save([FromBody]RunModel run)
        {
            runRepository.Update(run);
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Load(int id)
        {
            var run = runRepository.GetAll().SingleOrDefault(r => r.Id == id);
            if (run == null)
                return NotFound();
            else
                return Ok(run);
        }
    }
}
