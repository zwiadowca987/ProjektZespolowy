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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

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
                .HasForeignKey(Customer => Customer.Id);
        }
        public DbSet<ProjektZespolowy.Models.Costs> Costs { get; set; } = default!;
        
        // Add-Migration Name
        // Update-Database
    }
}
