using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discite.Data.Repositories;
using Discite.Data.Models;
using Discite.API.DTOs;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Discite.API.Services;
using Discite.API.Extensions;
using Swashbuckle.Swagger.Annotations;
using Microsoft.AspNetCore.Components.Forms;
using Discite.API.Attributes;

namespace Discite.API.Controllers
{
    public class ConfigurationController : BaseApiController
    {
        WeaponRepository weaponRepository;
        EnemyRepository enemyRepository;
        ArtifactRepository artifactRepository;
        public ConfigurationController()
        {
            weaponRepository = new WeaponRepository();
            enemyRepository = new EnemyRepository();
            artifactRepository = new ArtifactRepository();
        }

        /// <summary>
        /// Get game configuration
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/config")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(ConfigurationDto))]
        public ActionResult<ConfigurationDto> Config()
        {
            var config = new ConfigurationDto()
            {
                Weapons = weaponRepository.GetAll().Select(w => new Weapon(w)),
                Enemies = enemyRepository.GetAll().Select(e => new Enemy(e)),
                Artifacts = artifactRepository.GetAll().Select(a => new Artifact(a))
            };

            return Ok(config);
        }

        /// <summary>
        /// Update game configuration
        /// </summary>
        [HttpPut]
        [AuthorizeAdmin]
        [Route("api/config")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<ConfigurationDto> EditConfig(ConfigurationDto config)
        {
            foreach (var w in config.Weapons)
                weaponRepository.Update(new WeaponModel() { Id = w.Id, Name = w.Name, Damage = w.Damage, Speed = w.Speed });

            foreach (var e in config.Enemies)
                enemyRepository.Update(new EnemyModel() { Id = e.Id, Name = e.Name, Damage = e.Damage, Health = e.Health, Speed = e.Speed });

            foreach (var a in config.Artifacts)
                artifactRepository.Update(new ArtifactModel() { Id = a.Id, Name = a.Name, Power = a.Power });

            return Ok();
        }
    }
}
