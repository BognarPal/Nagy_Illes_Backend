using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("weapon")]
    public class WeaponModel : IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Damage { get; set; }
        public float Speed { get; set; }

        public ICollection<RunWeaponModel> Runs { get; set; }
    }
}
