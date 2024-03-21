using Microsoft.EntityFrameworkCore;
using Orders.Contracts.Domain;

namespace Orders.Infrastructure.Persistence
{

    public class OrdersDbContext : DbContext
    {
        public virtual DbSet<Order> Orders { get; set; }

        public OrdersDbContext(DbContextOptions<OrdersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(o =>
            {
                o.ToTable("Orders");
                o.HasKey(k => k.Id);
                o.Property(x => x.Id).HasColumnName("Id").IsRequired(true);
                o.Property(x => x.Description).HasColumnName("Description").IsRequired(false);
                o.Property(x => x.CreatedAt).HasColumnName("CreatedAt").IsRequired(true);
            });

            modelBuilder.Entity<OrderedProduct>(op =>
            {
                op.ToTable("OrderedProducts");
                op.HasKey(op => new { op.OrderId, op.ProductId });
                op.Property(op => op.OrderId).HasColumnName("OrderId").IsRequired(true);
                op.Property(op => op.ProductId).HasColumnName("ProductId").IsRequired(true);
                op.Property(op => op.Qty).HasColumnName("Qty").IsRequired(true);

                op.HasOne(op => op.Order).WithMany(o => o.OrderedProducts).HasForeignKey(o => o.OrderId);
            });
        }
    }

}
