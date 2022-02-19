using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Hotel
    {
        public Hotel()
        {
            Orders = new HashSet<Order>();
            RoomTypes = new HashSet<RoomType>();
            Rooms = new HashSet<Room>();
        }

        public int HotelId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RoomType> RoomTypes { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
