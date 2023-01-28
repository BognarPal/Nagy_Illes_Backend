using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("class")]
    public class ClassModel : IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float MaxHp { get; set; }
        public float Damage { get; set; }
        public float Energy { get; set; }
        public float Speed { get; set; }
        public ICollection<ClassArtifactModel> Artifacts { get; set; }
        public ICollection<UserClassModel> Users { get; set; }
    }
}
