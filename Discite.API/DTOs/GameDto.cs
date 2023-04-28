using Discite.Data.Enums;
using Discite.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Score { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Wave { get; set; }

        public ICollection<RunArtifactModel> Artifacts { get; set; }
        public ICollection<RunWeaponModel> Weapons { get; set; }
        public ICollection<RunEnemyModel> Enemies { get; set; }
    }
}
