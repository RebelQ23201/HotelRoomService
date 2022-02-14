using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class OrderDetailModel
    {
        public int OrderDetailId { get; set; }
        public int? RoomOrderId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ServiceId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual EmployeeModel Employee { get; set; }
        public virtual RoomOrderModel RoomOrder { get; set; }
        public virtual ServiceModel Service { get; set; }
    }
}
