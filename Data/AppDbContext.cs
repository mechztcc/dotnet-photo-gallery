using Microsoft.EntityFrameworkCore;
using AppApi.Modules.Users.Models;
using AppApi.Modules.Shared.Models;

namespace AppApi.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseEntity &&
                (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((User)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((User)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}