using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class HotelMember
    {
        public int MemberId { get; set; }
        public int? HotelId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}
