using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class HotelMemberModel
    {
        public int MemberId { get; set; }
        public int? HotelId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual HotelModel Hotel { get; set; }
    }
}
