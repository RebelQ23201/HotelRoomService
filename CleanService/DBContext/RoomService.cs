using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class RoomService
    {
        public int? ServiceId { get; set; }
        public int? SystemRoomTypeId { get; set; }

        public virtual Service Service { get; set; }
        public virtual SystemRoomType SystemRoomType { get; set; }
        //public int? RoomServiceId { get; set; }
    }
}
