using Discite.Data.Models;
using Discite.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable<UserModel> TopPlayers()
        {
            return userRepository.GetAll().OrderByDescending(u => u.Runs.Sum(r => r.Score));
        }
    }
}
