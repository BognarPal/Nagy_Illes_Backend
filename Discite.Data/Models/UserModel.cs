using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    public class UserModel : IModelWithId
    {
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Hash { get; set; }

        public string Salt { get; set; }

        public DateTime RegisterDate { get; set; }

    }
}
