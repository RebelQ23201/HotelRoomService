using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class EmployeeOutputModel
    {
        public int EmployeeId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }

        public virtual CompanyOutputModel Company { get; set; }
        public virtual List<OrderDetailOutputModel> OrderDetails { get; set; }
    }
}
