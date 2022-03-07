using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class RoomOrderOutputModel
    {
        public int RoomOrderId { get; set; }
        public int? OrderId { get; set; }
        public int? RoomId { get; set; }

        public virtual OrderOutputModel Order { get; set; }
        public virtual RoomOutputModel Room { get; set; }
        public virtual List<OrderDetailOutputModel> OrderDetails { get; set; }
    }
}
