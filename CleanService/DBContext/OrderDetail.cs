using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? RoomOrderId { get; set; }
        public int? EmployeeId { get; set; }
        public int? ServiceId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual RoomOrder RoomOrder { get; set; }
        public virtual Service Service { get; set; }
    }
}
