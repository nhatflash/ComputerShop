using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ComputerShop.Repository.Context
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Email)
                .HasColumnName("email")
                .HasMaxLength(256)
                .IsRequired();

                user.Property(u => u.PasswordHash)
                .HasColumnName("password_hash")
                .HasMaxLength(256)
                .IsRequired();

                user.Property(u => u.Role)
                .HasColumnName("role")
                .HasConversion<string>();

                user.Property(u => u.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(256)
                .IsRequired();

                user.Property(u => u.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(256)
                .IsRequired();

                user.Property(u => u.PhoneNumber)
                .HasColumnName("phone_number")
                .HasMaxLength(20);

                user.Property(u => u.Gender)
                .HasColumnName("gender")
                .HasConversion<string>();

                user.Property(u => u.Address)
                .HasColumnName("address")
                .HasMaxLength(256);

                user.Property(u => u.Ward)
                .HasColumnName("ward")
                .HasMaxLength(100);

                user.Property(u => u.District)
                .HasColumnName("district")
                .HasMaxLength(100);

                user.Property(u => u.City)
                .HasColumnName("city")
                .HasMaxLength(100);

                user.Property(u => u.Province)
                .HasColumnName("province")
                .HasMaxLength(100);

                user.Property(u => u.PostalCode)
                .HasColumnName("postal_code")
                .HasMaxLength(20);

                user.Property(u => u.ProfileImage)
                .HasColumnName("profile_image");

                user.Property(u => u.Status)
                .HasColumnName("status")
                .HasConversion<string>();

                user.Property(u => u.VerifiedAt)
                .HasColumnName("verified_at");

                user.Property(u => u.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("GETUTCDATE()");

                user.Property(u => u.UpdatedAt)
                .HasColumnName("updated_at")
                .HasDefaultValueSql("GETUTCDATE()");

                user.HasIndex(u => u.Email)
                .IsUnique();

                user.HasIndex(u => u.PhoneNumber)
                .IsUnique();
            });
        }

        public DbSet<User> Users { get; set; }
    }
}
