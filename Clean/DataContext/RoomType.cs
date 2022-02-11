using System;
using System.Collections.Generic;

#nullable disable

namespace Clean.DataContext
{
    public partial class RoomType
    {
        public RoomType()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomTypeId { get; set; }
        public int? HotelId { get; set; }
        public int? SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual SystemRoomType1 SystemRoomType { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
