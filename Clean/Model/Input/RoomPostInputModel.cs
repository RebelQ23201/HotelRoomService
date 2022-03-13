using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class RoomPostInputModel
    {
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
        public string Name { get; set; }
    }
}
