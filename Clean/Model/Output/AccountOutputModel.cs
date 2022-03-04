using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class AccountOutputModel
    {
        public int AccountId { get; set; }
        public int? RoleId { get; set; }
        public string Email { get; set; }

        public virtual RoleOutputModel Role { get; set; }
    }
}
