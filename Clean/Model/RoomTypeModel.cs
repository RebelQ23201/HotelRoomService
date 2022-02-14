using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class RoomTypeModel
    {
        public int RoomTypeId { get; set; }
        public int? HotelId { get; set; }
        public int? SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual HotelModel Hotel { get; set; }
        public virtual SystemRoomTypeModel SystemRoomType { get; set; }
        public virtual List<RoomModel> Rooms { get; set; }
    }
}
