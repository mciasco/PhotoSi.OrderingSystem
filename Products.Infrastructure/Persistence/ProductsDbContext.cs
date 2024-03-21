using Microsoft.EntityFrameworkCore;
using Products.Contracts.Domain;

namespace Products.Infrastructure.Persistence
{
    public class ProductsDbContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(o =>
            {
                o.ToTable("Categories");
                o.HasKey(k => k.Name);
                o.Property(x => x.Name).HasColumnName("Name").IsRequired(true);
                o.Property(x => x.Description).HasColumnName("Description").IsRequired(true);

                //o.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey().IsRequired(true);
            });

            modelBuilder.Entity<Product>(o =>
            {
                o.ToTable("Products");
                o.HasKey(k => k.Id);
                o.Property(x => x.Id).HasColumnName("Id").IsRequired(true);
                o.Property(x => x.Name).HasColumnName("Name").IsRequired(true);
                o.Property(x => x.Description).HasColumnName("Description").IsRequired(true);
                o.Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired(true);
                o.Property(x => x.QtyStock).HasColumnName("QtyStock").IsRequired(true);
                o.Property(x => x.Price).HasColumnName("Price").IsRequired(true);

                o.HasOne(o => o.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryName);
            });
        }
    }
}
