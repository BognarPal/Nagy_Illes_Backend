using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("user_class")]
    public class UserClassModel
    {
        public int UserId { get; set; }
        public UserModel User { get; set; }

        public int ClassId { get; set; }
        public ClassModel Class { get; set; }
    }
}
