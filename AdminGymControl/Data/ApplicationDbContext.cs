using AdminGymControl.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminGymControl.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Member> Members { get; set; }
    }

}
