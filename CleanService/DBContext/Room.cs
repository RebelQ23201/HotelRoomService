using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Room
    {
        public Room()
        {
            RoomOrders = new HashSet<RoomOrder>();
        }

        public int RoomId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual RoomType RoomType { get; set; }
        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }
}
