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
        public int RunId { get; set; }
        public RunModel Run { get; set; }

        public int ArtifactId { get; set; }
        public ArtifactModel Artifact { get; set; }

        public int Picked { get; set; }
        public int Seen { get; set; }
        public int Used { get; set; }
    }
}
