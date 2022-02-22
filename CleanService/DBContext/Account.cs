using System;
using System.Collections.Generic;

#nullable disable

namespace CleanService.DBContext
{
    public partial class Account
    {
        public int AccountId { get; set; }
        public int? RoleId { get; set; }
        public string Email { get; set; }

        public virtual Role Role { get; set; }
    }
}
