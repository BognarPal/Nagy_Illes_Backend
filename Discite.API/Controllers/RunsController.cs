using Discite.Data.Repositories;
using Discite.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Discite.API.Services;
using Discite.API.Extensions;
using Discite.API.DTOs;
using Discite.API.Attributes;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Collections;

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

            RunModel runModel = runRepository[run.Id];

            runModel.EndDate = DateTime.Now;

            runModel.Score = run.Score;
            runModel.Artifacts = new List<RunArtifactModel>();
            runModel.Weapons = new List<RunWeaponModel>();
            runModel.Enemies = new List<RunEnemyModel>();

            run.Artifacts.ToList().ForEach(
                r => runModel.Artifacts.Add(new RunArtifactModel 
                { 
                    ArtifactId = r.ArtifactId, 
                    Picked = r.Picked,
                }));

            run.Weapons.ToList().ForEach(
                r => runModel.Weapons.Add(new RunWeaponModel
                {
                    WeaponId = r.WeaponId,
                    Picked = r.Picked,
                }));

            run.Enemies.ToList().ForEach(
                r => runModel.Enemies.Add(new RunEnemyModel
                {
                    EnemyId = r.EnemyId,
                    Seen = r.Seen,
                    Damage = r.Damage,
                    Deaths = r.Deaths,
                }));

            runRepository.Update(runModel);
            return Ok();
        }
    }
}
