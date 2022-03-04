using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class RoomTypeOutputModel
    {
        public int RoomTypeId { get; set; }
        public int? HotelId { get; set; }
        public int? SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual HotelOutputModel Hotel { get; set; }
        public virtual SystemRoomTypeOutputModel SystemRoomType { get; set; }
        public virtual List<RoomOutputModel> Rooms { get; set; }
    }
}
