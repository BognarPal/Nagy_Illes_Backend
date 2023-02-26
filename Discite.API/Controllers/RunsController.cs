using Discite.Data.Repositories;
using Discite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Discite.API.Services;

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
        [Authorize]
        public IActionResult NewGame()
        {
            RunModel run = new RunModel();
            run.UserId = GetUid.uid(Request.Headers["Authorization"]);
            var crun = runRepository.Insert(run);
            return Created($"api/runs/{crun.Id}", crun);
        }

        [HttpPut]
        [Authorize]
        public IActionResult Save([FromBody]RunModel run)
        {
            if (GetUid.uid(Request.Headers["Authorization"]) != run.UserId)
                return Unauthorized();

            runRepository.Update(run);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult Load(int id)
        {
            var run = runRepository.GetAll().SingleOrDefault(r => r.Id == id);

            if (run == null)
                return NotFound();
            else
                if (GetUid.uid(Request.Headers["Authorization"]) != run.UserId)
                    return Unauthorized();

                return Ok(run);
        }
    }
}
