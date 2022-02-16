using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Employee
    {
        public Employee()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int EmployeeId { get; set; }
        public int? CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? Status { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
