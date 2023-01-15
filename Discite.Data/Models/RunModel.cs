using Discite.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("run")]
    public class RunModel : IModelWithId
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public ClassModel Class { get; set; }
        public string Path { get; set; }
        public int Gold { get; set; }
        public int Score { get; set; }
        public int Runtime { get; set; }
        public RunStatus Status { get; set; }
        [Column("version")]
        public string GameVersion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public float CurrentHp { get; set; }
    }
}
