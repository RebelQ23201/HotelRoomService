using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class ServiceModel
    {
        public int ServiceId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }

        public virtual CompanyModel Company { get; set; }
        public virtual List<OrderDetailModel> OrderDetails { get; set; }
    }
}
