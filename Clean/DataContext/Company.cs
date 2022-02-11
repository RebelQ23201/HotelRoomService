using System;
using System.Collections.Generic;

#nullable disable

namespace Clean.DataContext
{
    public partial class Company
    {
        public Company()
        {
            Employees = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Services = new HashSet<Service>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
