using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("run_weapon")]
    public class RunWeaponModel
    {
        public int Id { get; set; }

        public int RunId { get; set; }
        public RunModel Run { get; set; }

        public int WeaponId { get; set; }
        public WeaponModel Weapon { get; set; }

        public int Picked { get; set; }
    }
}
