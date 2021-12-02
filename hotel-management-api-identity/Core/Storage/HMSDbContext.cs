using hotel_management_api_identity.Core.Storage.Models;
using Microsoft.EntityFrameworkCore;

namespace hotel_management_api_identity.Core.Storage
{
    public class HMSDbContext : DbContext
    {
        public HMSDbContext(DbContextOptions<HMSDbContext> options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<Tokens> Tokens { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tokens>(b =>
            {
                b.HasIndex(i => i.Token).IsUnique(false);
                b.HasIndex(i => i.CreatedById).IsUnique(false);
                b.HasIndex(i => i.ExpiryDate).IsUnique(false);
            });

            modelBuilder.Entity<Employee>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.Email).IsUnique(false);
            });

            modelBuilder.Entity<Customer>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.Email).IsUnique(false);
                b.HasIndex(i => i.CreatedOn).IsUnique(false);
            });

            modelBuilder.Entity<Login>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.Password).IsUnique(false);
            });

            modelBuilder.Entity<Menu>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.Category).IsUnique(false);
                b.HasIndex(i => i.Item).IsUnique(false);
            });

            modelBuilder.Entity<Room>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.Name).IsUnique(false);
                b.HasIndex(i => i.Price).IsUnique(false);
            });

            modelBuilder.Entity<Sales>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.CreatedOn).IsUnique(false);
                b.HasIndex(i => i.CreatedById).IsUnique(false);
                b.HasIndex(i => i.Price).IsUnique(false);
            });

            modelBuilder.Entity<Booking>(b =>
            {
                b.HasIndex(i => i.Id).IsUnique(false);
                b.HasIndex(i => i.CreatedOn).IsUnique(false);
                b.HasIndex(i => i.CreatedById).IsUnique(false);
                b.HasIndex(i => i.AmountPaid).IsUnique(false);
                b.HasIndex(i => i.CheckOutDate).IsUnique(false);
                b.HasIndex(i => i.HasDiscount).IsUnique(false);
            });
        }
    }
}