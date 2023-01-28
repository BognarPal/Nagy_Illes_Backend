using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("eventroom")]
    public class RoomModel : IModelWithId
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RunRoomModel> Runs { get; set; }
    }
}
