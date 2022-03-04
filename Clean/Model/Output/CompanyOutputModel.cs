using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class CompanyOutputModel
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual List<EmployeeOutputModel> Employees { get; set; }
        public virtual List<OrderOutputModel> Orders { get; set; }
        public virtual List<ServiceOutputModel> Services { get; set; }

    }
}
