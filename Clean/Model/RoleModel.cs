using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model
{
    public class RoleModel
    {

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AccountModel> Accounts { get; set; }
    }
}
