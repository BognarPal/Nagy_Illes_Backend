using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discite.Data.Repositories;
using Discite.Data.Models;
using Discite.API.DTOs;
using System.Text.Json;

namespace Discite.API.Controllers
{
    public class ConfigurationController : BaseApiController
    {
        WeaponRepository weaponRepository;
        EnemyRepository enemyRepository;
        ArtifactRepository artifactRepository;
        ClassRepository classRepository;
        public ConfigurationController()
        {
            weaponRepository = new WeaponRepository();
            enemyRepository = new EnemyRepository();
            artifactRepository = new ArtifactRepository();
            classRepository = new ClassRepository();
        }

        [HttpGet]
        public ConfigurationDto Config()
        {
            var config = new ConfigurationDto()
            {
                Weapons = weaponRepository.GetAll().Select(w => new Weapon() { Id = w.Id, Name = w.Name, Damage = w.Damage, AttackSpeed = w.AttackSpeed }),
                Enemies = enemyRepository.GetAll().Select(e => new Enemy() { Id = e.Id, Name = e.Name, Damage = e.Damage, Energy = e.Energy, MaxHp = e.MaxHp, Speed = e.Speed }),
                Classes = classRepository.GetAll().Select(c => new Class() { Id = c.Id, Name = c.Name, Damage = c.Damage, Energy = c.Energy, MaxHp = c.MaxHp, Speed = c.Speed }),
                Artifacts = artifactRepository.GetAll().Select(a => new Artifact() { Id = a.Id, Name = a.Name, MaxLevel = a.MaxLevel })
            };

            return config;
        }

        [HttpPut]
        public ActionResult<ConfigurationDto> EditConfig(ConfigurationDto config)
        {
            foreach (var w in config.Weapons)
                weaponRepository.Update(new WeaponModel() { Id = w.Id, Name = w.Name, Damage = w.Damage, AttackSpeed = w.AttackSpeed });
 
            foreach (var e in config.Enemies)
                enemyRepository.Update(new EnemyModel() { Id = e.Id, Name = e.Name, Damage = e.Damage, Energy = e.Energy, MaxHp = e.MaxHp, Speed = e.Speed });
    
            foreach (var c in config.Classes)
                classRepository.Update(new ClassModel() { Id = c.Id, Name = c.Name, Damage = c.Damage, Energy = c.Energy, MaxHp = c.MaxHp, Speed = c.Speed });
   
            foreach (var a in config.Artifacts)
                artifactRepository.Update(new ArtifactModel() { Id = a.Id, Name = a.Name, MaxLevel = a.MaxLevel });

            return Ok(Config());
        }
    }
}
