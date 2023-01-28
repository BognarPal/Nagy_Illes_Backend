using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("class_artifact")]
    public class ClassArtifactModel
    {
        public int ClassId { get; set; }
        public ClassModel Class { get; set; }

        public int ArtifactId { get; set; }
        public ArtifactModel Artifact { get; set; }
    }
}
