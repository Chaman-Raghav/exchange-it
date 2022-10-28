using Api.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Api.Domain
{
    public class ViceContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ViceContext()
        {
        }

        public ViceContext(DbContextOptions<ViceContext> context) : base(context)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}