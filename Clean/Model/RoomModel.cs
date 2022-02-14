using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class RoomModel
    {
        public int RoomId { get; set; }
        public int? HotelId { get; set; }
        public int? RoomTypeId { get; set; }
        public string Name { get; set; }

        public virtual HotelModel Hotel { get; set; }
        public virtual RoomTypeModel RoomType { get; set; }
        public virtual List<RoomOrderModel> RoomOrders { get; set; }
    }
}
