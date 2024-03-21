using AddressBook.Contracts.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext
    {
        public virtual DbSet<Address> Addresses { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Address>(a =>
            {
                a.ToTable("Addresses");
                a.HasKey(k => k.AddressId);
                a.Property(x => x.AddressId).HasColumnName("AddressId").IsRequired(true);
                a.Property(x => x.OwnerAccountId).HasColumnName("OwnerAccountId").IsRequired(true);
                a.Property(x => x.AddressName).HasColumnName("AddressName").IsRequired(false);
                a.Property(x => x.Country).HasColumnName("Country").IsRequired(true);
                a.Property(x => x.StateProvice).HasColumnName("Country").IsRequired(true);
                a.Property(x => x.City).HasColumnName("City").IsRequired(true);
                a.Property(x => x.PostalCode).HasColumnName("PostalCode").IsRequired(true);
                a.Property(x => x.StreetName).HasColumnName("StreetName").IsRequired(true);
                a.Property(x => x.StreetNumber).HasColumnName("StreetNumber").IsRequired(true);
                a.Property(x => x.IsMainAddress).HasColumnName("IsMainAddress").IsRequired(true);
            });

        }
    }
}
