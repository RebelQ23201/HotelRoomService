using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clean.Model.Output
{
    public class RoleOutputModel
    {

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<AccountOutputModel> Accounts { get; set; }
    }
}
