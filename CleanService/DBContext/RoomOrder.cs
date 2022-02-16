using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class RoomOrder
    {
        public RoomOrder()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int RoomOrderId { get; set; }
        public int? OrderId { get; set; }
        public int? RoomId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
