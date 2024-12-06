using Entity.Layer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Layer
{
    public class ApplicationContext: IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
            
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Reaport> Reaports { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
