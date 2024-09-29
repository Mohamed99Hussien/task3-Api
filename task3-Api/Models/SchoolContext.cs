using Microsoft.EntityFrameworkCore;

namespace task3_Api.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<SchoolUser> SchoolUsers { get; set; }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SchoolUser>().ToTable("Users"); // Change default table name
        }
    }
}
