using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("run_artifact")]
    public class RunArtifactModel
    {
        public int Id { get; set; }

        public int RunId { get; set; }
        public RunModel Run { get; set; }

        public int ArtifactId { get; set; }
        public ArtifactModel Artifact { get; set; }

        public int Name { get; set; }
        public int Power { get; set; }
    }
}
