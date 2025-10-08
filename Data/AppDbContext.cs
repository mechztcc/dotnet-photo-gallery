using Microsoft.EntityFrameworkCore;
using AppApi.Modules.Users.Models;

namespace AppApi.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}