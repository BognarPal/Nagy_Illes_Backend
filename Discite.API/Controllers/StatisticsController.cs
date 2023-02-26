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
        RoomRepository roomRepository;
        WeaponRepository weaponRepository;
        RunRepository runRepository;
        ClassRepository classRepository;
        ArtifactRepository artifactRepository;

        public StatisticsController() 
        {
            userRepository = new UserRepository();
            enemyRepository = new EnemyRepository();
            roomRepository = new RoomRepository();
            weaponRepository = new WeaponRepository();
            runRepository = new RunRepository();
            classRepository = new ClassRepository();
            artifactRepository = new ArtifactRepository();
        }

        [HttpGet("toplist")]
        [AllowAnonymous]
        public IEnumerable<ToplistDto> TopPlayers()
        {
            var users = userRepository.GetAll().OrderByDescending(u => u.Runs.Sum(r => r.Score)).Take(10);

            var tops = users.Select(x => new ToplistDto()
            {
                Id = x.Id,
                Username = x.UserName,
                Score = x.Runs.Sum(r => r.Score)
            });

            return tops;
        }

        [HttpGet("classes")]
        [AllowAnonymous]
        public IEnumerable<ClassStatsDto> Classes()
        {
            var classes = classRepository.GetAll().Select(c => new ClassStatsDto()
            {
                Id = c.Id,
                Owned = c.Users.Count(),
                Used = runRepository.GetAll().Count(r => r.Class.Id == c.Id),
                Deaths = runRepository.GetAll().Count(r => r.Class.Id == c.Id && r.Status == RunStatus.Dead),
                Kills = runRepository.GetAll().Where(r => r.Class.Id == c.Id).Sum(r => r.Enemies.Sum(e => e.Deaths))
            });
            
            return classes;
        }

        [HttpGet("weapons")]
        [AllowAnonymous]
        public IEnumerable<WeaponStatsDto> Weapons()
        {
            var weapons = weaponRepository.GetAll().Select(w => new WeaponStatsDto()
            {
                Id = w.Id,
                Picked = w.Runs.Sum(r => r.Picked), 
                Seen = w.Runs.Sum(r => r.Seen)
            });

            return weapons;
        }

        [HttpGet("enemies")]
        [AllowAnonymous]
        public IEnumerable<EnemyStatsDto> Enemies()
        {
            var enemies = enemyRepository.GetAll().Select(e => new EnemyStatsDto()
            {
                Id = e.Id,
                Deaths = e.Runs.Sum(r => r.Deaths),
                Kills = e.Runs.Count(r => r.Run.Status == RunStatus.Dead)
            });

            return enemies;
        }
    }
}
