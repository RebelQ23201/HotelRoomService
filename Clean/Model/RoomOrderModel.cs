using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class RoomOrderModel
    {
        public int RoomOrderId { get; set; }
        public int? OrderId { get; set; }
        public int? RoomId { get; set; }

        public virtual OrderModel Order { get; set; }
        public virtual RoomModel Room { get; set; }
        public virtual List<OrderDetailModel> OrderDetails { get; set; }
    }
}
