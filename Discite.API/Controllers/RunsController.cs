using Discite.Data.Repositories;
using Discite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Discite.API.Services;
using Discite.API.Extensions;
using Discite.API.DTOs;
using Discite.API.Attributes;

namespace Discite.API.Controllers
{
    public class RunsController : BaseApiController
    {
        RunRepository runRepository;

        public RunsController()
        {
            this.runRepository = new RunRepository();
        }

        /// <summary>
        /// Start new game
        /// </summary>
        [HttpPost]
        [Authorize]
        [Route("api/run")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult NewGame()
        {
            RunModel run = new RunModel();
            run.UserId = Request.uid();
            var crun = runRepository.Insert(run);
            return Created($"api/run/{crun.Id}", crun);
        }

        /// <summary>
        /// Save game
        /// </summary>
        [HttpPut]
        [Authorize]
        [Route("api/run")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Save([FromBody] GameDto run)
        {
            if (Request.uid() != run.UserId)
                return Unauthorized();

            RunModel runModel = new RunModel();

            runRepository.Update(runModel);
            return Ok();
        }
    }
}
