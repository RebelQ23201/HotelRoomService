using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class RoomTypeInputModel
    {
        public int RoomTypeId { get; set; }
        public int? HotelId { get; set; }
        public int? SystemRoomTypeId { get; set; }
        public string Name { get; set; }
    }
}
