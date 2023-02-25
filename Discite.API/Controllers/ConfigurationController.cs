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
                Weapons = weaponRepository.GetAll().Select(x => new Weapon() { Id = x.Id, Name = x.Name, Damage = x.Damage, AttackSpeed = x.AttackSpeed }),
                Enemies = enemyRepository.GetAll().Select(x => new Enemy() { Id = x.Id, Name = x.Name, Damage = x.Damage, Energy = x.Energy, MaxHp = x.MaxHp, Speed = x.Speed }),
                Classes = classRepository.GetAll().Select(x => new Class() { Id = x.Id, Name = x.Name, Damage = x.Damage, Energy = x.Energy, MaxHp = x.MaxHp, Speed = x.Speed }),
                Artifacts = artifactRepository.GetAll().Select(x => new Artifact() { Id = x.Id, Name = x.Name, MaxLevel = x.MaxLevel })
            };

            return config;
        }
    }
}
