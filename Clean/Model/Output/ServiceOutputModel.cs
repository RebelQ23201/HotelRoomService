using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class ServiceOutputModel
    {
        public int ServiceId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual CompanyOutputModel Company { get; set; }
        public virtual List<OrderDetailOutputModel> OrderDetails { get; set; }
    }
}
