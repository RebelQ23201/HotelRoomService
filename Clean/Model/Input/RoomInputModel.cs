using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class RoomInputModel
    {
        public int RoomId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
        public string Name { get; set; }
    }
}
