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

            // db-schema for User model
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Id)
                    .HasColumnName("id");

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

                user.HasKey(u => u.Id);

                user.HasIndex(u => u.Email)
                    .IsUnique();

                user.HasIndex(u => u.PhoneNumber)
                    .IsUnique();
            });


            // db-schema for Manufacturer model
            modelBuilder.Entity<Manufacturer>(mn =>
            {
                mn.Property(m => m.Id)
                    .HasColumnName("id");

                mn.Property(m => m.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .IsRequired();

                mn.Property(m => m.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                mn.HasKey(m => m.Id);

                mn.HasIndex(u => u.Name)
                    .IsUnique();
            });


            // db-schema for Category model
            modelBuilder.Entity<Category>(ct =>
            {
                ct.Property(c => c.Id)
                    .HasColumnName("id");

                ct.Property(c => c.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .IsRequired();

                ct.Property(c => c.Description)
                    .HasColumnName("description")
                    .IsRequired();

                ct.Property(c => c.ImageUrl)
                    .HasColumnName("image_url");

                ct.Property(c => c.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                ct.Property(c => c.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                ct.HasKey(c => c.Id);

                ct.HasIndex(c => c.Name).IsUnique();
            });



            // db-schema for Product model
            modelBuilder.Entity<Product>(pd =>
            {
                pd.Property(p => p.Id)
                    .HasColumnName("id");

                pd.Property(p => p.CategoryId)
                    .HasColumnName("category_id")
                    .IsRequired();

                pd.Property(p => p.ManufacturerId)
                    .HasColumnName("manufacturer_id")
                    .IsRequired();

                pd.Property(p => p.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name")
                    .IsRequired();

                pd.Property(p => p.Description)
                    .HasColumnName("description")
                    .IsRequired();

                pd.Property(p => p.Price)
                    .HasColumnName("price")
                    .HasPrecision(18, 2)
                    .IsRequired();

                pd.Property(p => p.StockQuantity)
                    .HasColumnName("stock_quantity")
                    .HasDefaultValue(0)
                    .IsRequired();

                pd.Property(p => p.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .IsRequired();

                pd.Property(p => p.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                pd.Property(p => p.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETUTCDATE()");

                pd.HasKey(p => p.Id);

                pd.HasIndex(p => p.Name).IsUnique();

                pd.OwnsOne(p => p.Specifications, builder =>
                {
                    builder.ToJson("specifications");
                });

                pd.Navigation(p => p.Specifications)
                    .IsRequired();

                pd.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                pd.HasOne(p => p.Manufacturer)
                    .WithMany()
                    .HasForeignKey(p => p.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
