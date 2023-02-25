using Discite.Data.Enums;
using Discite.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Discite.API.DTOs
{
    public class GameDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ClassId { get; set; }
        public string Path { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public int Runtime { get; set; }
        public RunStatus Status { get; set; }
        public string GameVersion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float CurrentHp { get; set; }
        public int Seed { get; set; }

        public ICollection<RunArtifactModel> Artifacts { get; set; }
        public ICollection<RunRoomModel> Rooms { get; set; }
        public ICollection<RunWeaponModel> Weapons { get; set; }
        public ICollection<RunEnemyModel> Enemies { get; set; }
    }
}
