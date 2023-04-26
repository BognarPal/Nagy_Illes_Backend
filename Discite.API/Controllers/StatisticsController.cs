using Discite.API.DTOs;
using Discite.Data.Models;
using Discite.Data.Repositories;
using Discite.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Discite.API.Controllers
{
    public class StatisticsController : BaseApiController
    {
        UserRepository userRepository;
        EnemyRepository enemyRepository;
        WeaponRepository weaponRepository;
        RunRepository runRepository;
        ArtifactRepository artifactRepository;

        public StatisticsController() 
        {
            userRepository = new UserRepository();
            enemyRepository = new EnemyRepository();
            weaponRepository = new WeaponRepository();
            runRepository = new RunRepository();
            artifactRepository = new ArtifactRepository();
        }
        /// <summary>
        /// Top 10 player
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/stats/toplist")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ToplistDto>> TopPlayers()
        {
            var users = userRepository.GetAll().OrderByDescending(u => u.Runs.Sum(r => r.Score)).Take(10);

            var tops = users.Select(x => new ToplistDto()
            {
                Id = x.Id,
                Username = x.UserName,
                Score = x.Runs.Sum(r => r.Score)
            });

            return Ok(tops);
        }

        /// <summary>
        /// Statistics about weapons
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/stats/weapons")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<WeaponStatsDto> Weapons()
        {
            var weapons = weaponRepository.GetAll().Select(w => new WeaponStatsDto()
            {
                Id = w.Id,
                Picked = w.Runs.Sum(r => r.Picked), 
            });

            return weapons;
        }

        /// <summary>
        /// Statistics about enemies
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/stats/enemies")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IEnumerable<EnemyStatsDto> Enemies()
        {
            var enemies = enemyRepository.GetAll().Select(e => new EnemyStatsDto()
            {
                Id = e.Id,
                Deaths = e.Runs.Sum(r => r.Deaths),
            });

            return enemies;
        }
    }
}
