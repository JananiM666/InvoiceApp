using Microsoft.EntityFrameworkCore;
using InvoiceApp.Models;

namespace InvoiceApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<CompanyProfile> CompanyProfiles { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<CustomerVendor> CustomerVendors { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<NumberSeries> NumberSeries { get; set; }

        // ADD THIS
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });
        }
    }
}