using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class HotelMemberOutputModel
    {
        public int MemberId { get; set; }
        public int? HotelId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual HotelOutputModel Hotel { get; set; }
    }
}
