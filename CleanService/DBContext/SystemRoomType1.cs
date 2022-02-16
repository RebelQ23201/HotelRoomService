﻿using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class SystemRoomType1
    {
        public SystemRoomType1()
        {
            RoomTypes = new HashSet<RoomType>();
        }

        public int SystemRoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RoomType> RoomTypes { get; set; }
    }
}
