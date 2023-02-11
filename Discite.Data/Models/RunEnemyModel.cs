using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("run_enemy")]
    public class RunEnemyModel
    {
        public int Id { get; set; }

        public int RunId { get; set; }
        public RunModel Run { get; set; }

        public int EnemyId { get; set; }
        public EnemyModel Enemy { get; set; }

        public int Deaths { get; set; }
        public int Seen { get; set; }
        public int Damage { get; set; }
    }
}
