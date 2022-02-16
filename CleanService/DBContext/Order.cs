using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Order
    {
        public Order()
        {
            RoomOrders = new HashSet<RoomOrder>();
        }

        public int OrderId { get; set; }
        public int? HotelId { get; set; }
        public string Name { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Status { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<RoomOrder> RoomOrders { get; set; }
    }
}
