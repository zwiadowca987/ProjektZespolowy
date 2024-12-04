using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjektZespolowy.Models;

namespace ProjektZespolowy.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Raport> Raports { get; set; }
        public DbSet<RaportItem> RaportItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<OrderWarehouse> OrderWarehouses { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderDetails>()
                .HasOne(OrderDetails => OrderDetails.Product)
                .WithMany(Product => Product.OrderDetails)
                .HasForeignKey(OrderDetails => OrderDetails.ProductId);

            builder.Entity<OrderDetails>()
                .HasOne(OrderDetails => OrderDetails.Order)
                .WithMany(Order => Order.OrderDetails)
                .HasForeignKey(OrderDetails => OrderDetails.OrderId);

            builder.Entity<Order>()
                .HasOne(Order => Order.Customer)
                .WithMany(Customer => Customer.Orders)
                .HasForeignKey(Customer => Customer.CustomerId);

            builder.Entity<Warehouse>()
                .HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProduktId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Raport>()
                .HasMany(r => r.RaportItems)
                .WithOne(ri => ri.Raport)
                .HasForeignKey(ri => ri.RaportId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            builder.Entity<OrderWarehouse>()
                .HasOne(ow => ow.Warehouse)
                .WithMany(w => w.OrderWarehouses)
                .HasForeignKey(ow => ow.ProduktId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<OrderDetails>()
                .HasKey(od => new { od.OrderId, od.ProductId });

            builder.Entity<OrderWarehouse>()
                .HasOne(ow => ow.Supplier)
                .WithMany(s => s.OrderWarehouses)
                .HasForeignKey(ow => ow.DostawcaID);
        }
        public DbSet<ProjektZespolowy.Models.Costs> Costs { get; set; } = default!;
        public DbSet<ProjektZespolowy.Models.Employee> Employee { get; set; } = default!;

        // Add-Migration Name
        // Update-Database
    }
}
