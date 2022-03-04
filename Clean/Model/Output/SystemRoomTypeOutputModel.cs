using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class SystemRoomTypeOutputModel
    {
        public int SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual List<RoomTypeOutputModel> RoomTypes { get; set; }
    }
}
