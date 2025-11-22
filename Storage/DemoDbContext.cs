using Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Storage
{
    public class DemoDbContext : DbContext
    {
            public DemoDbContext() { }

            public DemoDbContext(DbContextOptions options) : base(options)
            { 
            }
            
            public DbSet<Client> Clients { get; set; }
            public DbSet<Product> Products { get; set; }

            public DbSet<Order> Orders { get; set; }
            //public DbSet<OrderItem> OrderItems { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                  optionsBuilder.UseSqlServer(
                      @"Server=tcp:azuresqldemo-server.database.windows.net,1433;Initial Catalog=AzureSqlDemo;Persist Security Info=False;User ID=bearcome;Password=Test1231;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                  base.OnModelCreating(modelBuilder);
                  modelBuilder.Entity<Client>(e =>
                  {
                        e.HasKey(c => c.Id);
                        e.Property(c => c.ClientName).IsRequired().HasMaxLength(100);

                  });
                  modelBuilder.Entity<Product>(e =>
                  {
                        e.HasKey(p => p.Id);
                        e.Property(p => p.ProductName).IsRequired().HasMaxLength(100);
                        e.Property(p => p.Price).IsRequired().HasColumnType("decimal(18,2)");
                  });
                  modelBuilder.Entity<Order>(e =>
                  {
                        e.HasKey(o => o.Id);
                        e.Property(o => o.OrderDate).IsRequired();
                        e.HasMany(o => o.OrderItems);
                  });
            }
      }
}
