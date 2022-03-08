using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class OrderDetailOutputModel
    {
        public int OrderDetailId { get; set; }
        public int? RoomOrderId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ServiceId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual EmployeeOutputModel Employee { get; set; }
        public virtual RoomOrderOutputModel RoomOrder { get; set; }
        public virtual ServiceOutputModel Service { get; set; }
    }
}
