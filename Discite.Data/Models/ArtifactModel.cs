using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("artifact")]
    public class ArtifactModel : IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column("upgrade_level")]
        public int MaxLevel { get; set; }
    }
}
