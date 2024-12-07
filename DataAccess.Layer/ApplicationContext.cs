using Entity.Layer;
using Entity.Layer.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Layer
{
    public class ApplicationContext: IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) :base(options)
        {
            
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Report> Reaports { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Member> Members { get; set; }

    }
}
