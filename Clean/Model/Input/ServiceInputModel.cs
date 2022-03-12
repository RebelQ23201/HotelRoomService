using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Input
{
    public class ServiceInputModel
    {
        public int ServiceId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
    }
}
