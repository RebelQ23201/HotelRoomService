using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Clean.Model;

namespace Clean.Data
{
    public class CleanContext : DbContext
    {
        public CleanContext (DbContextOptions<CleanContext> options)
            : base(options)
        {
        }

        public DbSet<Clean.Model.Company> Company { get; set; }
    }
}
