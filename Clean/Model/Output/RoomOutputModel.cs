using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class RoomOutputModel
    {
        public int RoomId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual HotelOutputModel Hotel { get; set; }
        public virtual RoomTypeOutputModel RoomType { get; set; }
        public virtual List<RoomOrderOutputModel> RoomOrders { get; set; }
    }
}
