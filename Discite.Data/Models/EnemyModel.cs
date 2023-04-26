using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("enemy")]
    public class EnemyModel : IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }

        public ICollection<RunEnemyModel> Runs { get; set; }
    }
}
