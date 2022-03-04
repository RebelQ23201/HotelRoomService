using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class HotelOutputModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual List<OrderOutputModel> Orders { get; set; }
        public virtual List<RoomTypeOutputModel> RoomTypes { get; set; }
        public virtual List<RoomOutputModel> Rooms { get; set; }
    }
}
