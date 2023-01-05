using System;
using System.Collections.Generic;
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
        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] Hash { get; set; }

        public byte[] Salt { get; set; }
        
        [Column("register_date")]
        public DateTime RegisterDate { get; set; }

    }
}
