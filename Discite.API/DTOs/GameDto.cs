using Discite.Data.Enums;
using Discite.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class RunArtifactDto
    {
        public int ArtifactId { get; set; }
        public int Picked { get; set; }
    }

    public class RunWeaponDto
    {
        public int WeaponId { get; set; }
        public int Picked { get; set; }
    }

    public class RunEnemyDto
    {
        public int EnemyId { get; set; }
        public int Deaths { get; set; }
        public int Seen { get; set; }
        public float Damage { get; set; }
    }

    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Wave { get; set; }

        public ICollection<RunArtifactDto> Artifacts { get; set; }
        public ICollection<RunWeaponDto> Weapons { get; set; }
        public ICollection<RunEnemyDto> Enemies { get; set; }
    }
}
