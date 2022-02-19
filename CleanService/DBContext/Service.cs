using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Service
    {
        public Service()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ServiceId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
