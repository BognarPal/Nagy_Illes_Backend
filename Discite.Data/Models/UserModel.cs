using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("user")]
    public class UserModel : IModelWithId
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Hash { get; set; }
        [Required]
        public byte[] Salt { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;

        [NotMapped]
        public int RunsCount { get => Runs.Count; }

        [NotMapped]
        public int TotalScores { get => Runs.Sum(r => r.Score); }

        public ICollection<RunModel> Runs { get; set; }
    }
}
