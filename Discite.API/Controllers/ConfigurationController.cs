using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Discite.Data.Repositories;
using Discite.Data.Models;
using System.Text.Json;

namespace Discite.API.Controllers
{
    public class ConfigurationController : BaseApiController
    {
        private class Config
        {
            public Config(WeaponRepository weaponRepository, EnemyRepository enemyRepository, ArtifactRepository artifactRepository, ClassRepository classRepository) 
            {
                this.Weapons = weaponRepository.GetAll();
                this.Enemies = enemyRepository.GetAll();
                this.Artifacts = artifactRepository.GetAll();
                this.Classes = classRepository.GetAll();
            }
            public IEnumerable<WeaponModel> Weapons { get; set; }
            public IEnumerable<ArtifactModel> Artifacts { get; set; }
            public IEnumerable<EnemyModel> Enemies { get; set; }
            public IEnumerable<ClassModel> Classes { get; set; }

        }
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
        public string Configuration()
        {
            return JsonSerializer.Serialize(new Config(weaponRepository, enemyRepository, artifactRepository, classRepository));
        }
    }
}
