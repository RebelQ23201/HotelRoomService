using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class SystemRoomTypeModel
    {
        public int SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual List<RoomTypeModel> RoomTypes { get; set; }
    }
}
