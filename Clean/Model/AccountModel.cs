using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class AccountModel
    {
        public int AccountId { get; set; }
        public int? RoleId { get; set; }
        public string Email { get; set; }

        public virtual RoleModel Role { get; set; }
    }
}
