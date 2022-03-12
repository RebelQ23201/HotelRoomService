using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class ServicePOSTInputModel
    {
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
