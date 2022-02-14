﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class HotelModel
    {
        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual List<OrderModel> Orders { get; set; }
        public virtual List<RoomTypeModel> RoomTypes { get; set; }
        public virtual List<RoomModel> Rooms { get; set; }
    }
}
