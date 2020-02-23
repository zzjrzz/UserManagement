using Microsoft.EntityFrameworkCore;
using UserManagement.Maps;

namespace UserManagement.Models
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserMap(modelBuilder.Entity<User>());
            new AccountMap(modelBuilder.Entity<Account>());

            modelBuilder.Entity<Account>()
                .HasOne(a => a.User);
        }
    }
}