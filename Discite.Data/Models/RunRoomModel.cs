using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Models
{
    [Table("run_room")]
    public class RunRoomModel
    {
        public int RunId { get; set; }
        public RunModel Run { get; set; }

        public int RoomId { get; set; }
        public RoomModel Room { get; set; }

        public int Seen { get; set; }
    }
}
