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

            modelBuilder.Entity<Category>(c =>
            {
                c.ToTable("Categories");
                c.HasKey(k => k.Name);
                c.Property(x => x.Name).HasColumnName("Name").IsRequired(true);
                c.Property(x => x.Description).HasColumnName("Description").IsRequired(true);

                //o.HasMany(c => c.Products).WithOne(p => p.Category).HasForeignKey().IsRequired(true);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.ToTable("Products");
                p.HasKey(k => k.Id);
                p.Property(x => x.Id).HasColumnName("Id").IsRequired(true);
                p.Property(x => x.Name).HasColumnName("Name").IsRequired(true);
                p.Property(x => x.Description).HasColumnName("Description").IsRequired(true);
                p.Property(x => x.CategoryName).HasColumnName("CategoryName").IsRequired(true);
                p.Property(x => x.QtyStock).HasColumnName("QtyStock").IsRequired(true);
                p.Property(x => x.Price).HasColumnName("Price").IsRequired(true);

                p.HasOne(o => o.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryName);
            });
        }
    }
}
