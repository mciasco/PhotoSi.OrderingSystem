using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Contracts.Domain;

namespace Users.Infrastructure.Persistence
{
    public class UsersDbContext : DbContext
    {
        public virtual DbSet<Account> Accounts { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(a =>
            {
                a.ToTable("Accounts");
                a.HasKey(k => k.AccountId);
                a.Property(x => x.AccountId).HasColumnName("AccountId").IsRequired(true);
                a.Property(x => x.Name).HasColumnName("Name").IsRequired(true);
                a.Property(x => x.Surname).HasColumnName("Surname").IsRequired(true);
                a.Property(x => x.RegistrationEmail).HasColumnName("RegistrationEmail").IsRequired(true);
                a.Property(x => x.Username).HasColumnName("Username").IsRequired(true);
                a.Property(x => x.PasswordHash).HasColumnName("PasswordHash").IsRequired(true);
            });
        }
    }
}
