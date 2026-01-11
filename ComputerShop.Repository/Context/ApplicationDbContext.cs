using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography.Xml;
using System.Text.Json;

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
                    .IsRequired();

                user.Property(u => u.UpdatedAt)
                    .HasColumnName("updated_at")
                    .IsRequired();

                user.HasKey(u => u.Id);

                user.HasIndex(u => u.Email)
                    .HasDatabaseName("idx_userEmail")
                    .IsUnique();

                user.HasIndex(u => u.PhoneNumber)
                    .HasDatabaseName("idx_userPhone")
                    .IsUnique();
            });


            // db-schema for Manufacturer model
            modelBuilder.Entity<Manufacturer>(mn =>
            {
                mn.Property(m => m.Id)
                    .HasColumnName("id");

                mn.Property(m => m.Name)
                    .HasMaxLength(256)
                    .HasColumnName("name")
                    .IsRequired();

                mn.Property(m => m.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                mn.HasKey(m => m.Id);

                mn.HasIndex(u => u.Name)
                    .HasDatabaseName("idx_manufacturerName")
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
                    .IsRequired();

                ct.Property(c => c.UpdatedAt)
                    .HasColumnName("updated_at")
                    .IsRequired();

                ct.HasKey(c => c.Id);

                ct.HasIndex(c => c.Name)
                    .HasDatabaseName("idx_categoryName")
                    .IsUnique();
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
                    .HasMaxLength(256)
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

                pd.Property(p => p.WarrantyMonth)
                    .HasColumnName("warranty_month")
                    .HasDefaultValue(0)
                    .IsRequired();

                pd.Property(p => p.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .IsRequired();

                pd.Property(p => p.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                pd.Property(p => p.UpdatedAt)
                    .HasColumnName("updated_at")
                    .IsRequired();

                pd.HasKey(p => p.Id);

                pd.HasIndex(p => p.Name)
                    .HasDatabaseName("idx_productName")
                    .IsUnique();

                var comparer = new ValueComparer<Dictionary<string, string>>(
                    (c1, c2) => JsonSerializer.Serialize(c1, (JsonSerializerOptions)null!) == JsonSerializer.Serialize(c2, (JsonSerializerOptions)null!), 
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.Key.GetHashCode(), v.Value.GetHashCode())),
                    c => JsonSerializer.Deserialize<Dictionary<string, string>>(JsonSerializer.Serialize(c, (JsonSerializerOptions)null!), (JsonSerializerOptions)null!)!
                );

                pd.Property(p => p.Specifications)
                    .HasColumnName("specifications")
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
                        v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions)null!)!
                    )
                    .HasColumnType("NVARCHAR(MAX)")
                    .Metadata.SetValueComparer(comparer);

                pd.HasOne(p => p.Category)
                    .WithMany()
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                pd.HasOne(p => p.Manufacturer)
                    .WithMany()
                    .HasForeignKey(p => p.ManufacturerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // db-schema for ProductImage model
            modelBuilder.Entity<ProductImage>(pdi =>
            {
                pdi.Property(i => i.Id)
                    .HasColumnName("id");

                pdi.Property(i => i.ProductId)
                    .HasColumnName("product_id")
                    .IsRequired();

                pdi.Property(i => i.ImageUrl)
                    .HasColumnName("image_url")
                    .IsRequired();

                pdi.Property(i => i.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                pdi.HasOne(i => i.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(i => i.ProductId);
            });


            // db-schema for Order model
            modelBuilder.Entity<Order>(odr =>
            {
                odr.Property(o => o.Id)
                    .HasColumnName("id");

                odr.Property(o => o.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                odr.Property(o => o.OrderDate)
                    .HasColumnName("order_date")
                    .IsRequired();

                odr.Property(o => o.SubTotal)
                    .HasColumnName("sub_total")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.Property(o => o.ShippingCost)
                    .HasColumnName("shipping_cost")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.Property(o => o.Tax)
                    .HasColumnName("tax")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.Property(o => o.TotalAmount)
                    .HasColumnName("total_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.Property(o => o.Type)
                    .HasColumnName("type")
                    .HasConversion<string>()
                    .IsRequired();

                odr.Property(o => o.ShippingAddress)
                    .HasColumnName("shipping_address")
                    .HasMaxLength(256);

                odr.Property(o => o.TrackingPhone)
                    .HasColumnName("tracking_phone")
                    .HasMaxLength(20)
                    .IsRequired();

                odr.Property(o => o.Status)
                    .HasColumnName("status")
                    .HasConversion<string>()
                    .IsRequired();

                odr.Property(o => o.Notes)
                    .HasColumnName("notes")
                    .HasMaxLength(256);

                odr.Property(o => o.UpdatedAt)
                    .HasColumnName("updated_at")
                    .IsRequired();

                odr.HasKey(o => o.Id);
                
            });


            // db-schema for OrderItem model
            modelBuilder.Entity<OrderItem>(odr =>
            {
                odr.Property(o => o.Id)
                    .HasColumnName("id");

                odr.Property(o => o.OrderId)
                    .HasColumnName("orderId")
                    .IsRequired();

                odr.Property(o => o.ProductId)
                    .HasColumnName("productId")
                    .IsRequired();

                odr.Property(o => o.Quantity)
                    .HasColumnName("quantity")
                    .HasDefaultValue(0)
                    .IsRequired();

                odr.Property(o => o.UnitPrice)
                    .HasColumnName("unit_price")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.Property(o => o.SubTotal)
                    .HasColumnName("sub_total")
                    .HasPrecision(18, 2)
                    .IsRequired();

                odr.HasOne(o => o.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(o => o.OrderId);
                
                odr.HasOne(o => o.Product)
                    .WithMany()
                    .HasForeignKey(o => o.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                
                odr.HasKey(o => o.Id);
            });

            // db-schema for Payment model
            modelBuilder.Entity<Payment>(pm =>
            {
                pm.Property(p => p.Id)
                    .HasColumnName("id");

                pm.Property(p => p.OrderId)
                    .HasColumnName("order_id")
                    .IsRequired();

                pm.Property(p => p.Amount)
                    .HasColumnName("amount")
                    .HasPrecision(18,2)
                    .IsRequired();

                pm.Property(p => p.Method)
                    .HasColumnName("method")
                    .HasConversion<string>()
                    .IsRequired();

                pm.Property(p => p.PaymentDate)
                    .HasColumnName("payment_date")
                    .IsRequired();

                pm.HasKey(p => p.Id);

                pm.HasOne(p => p.Order)
                    .WithMany()
                    .HasForeignKey(p => p.OrderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            // db-schema for Cart model
            modelBuilder.Entity<Cart>(cart =>
            {
                cart.Property(c => c.Id)
                    .HasColumnName("id");

                cart.Property(c => c.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();

                cart.HasOne(c => c.User)
                    .WithOne()
                    .HasForeignKey<Cart>(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                cart.HasKey(c => c.Id);
            });


            // db-schema for CartItem model
            modelBuilder.Entity<CartItem>(ci =>
            {
                ci.Property(c => c.Id)
                    .HasColumnName("id");
                
                ci.Property(c => c.CartId)
                    .HasColumnName("cart_id")
                    .IsRequired();
                
                ci.Property(c => c.ProductId)
                    .HasColumnName("product_id")
                    .IsRequired();
                
                ci.Property(c => c.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired();
                
                ci.Property(c => c.AttachedAt)
                    .HasColumnName("attached_at")
                    .IsRequired();

                ci.HasKey(c => c.Id);

                ci.HasOne(c => c.Cart)
                    .WithMany()
                    .HasForeignKey(c => c.CartId);

                ci.HasOne(c => c.Product)
                    .WithMany()
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // db-schema for Review model
            modelBuilder.Entity<Review>(rv =>
            {
                rv.Property(r => r.Id)
                    .HasColumnName("id");
                
                rv.Property(r => r.ProductId)
                    .HasColumnName("product_id")
                    .IsRequired();

                rv.Property(r => r.UserId)
                    .HasColumnName("user_id")
                    .IsRequired();
                
                rv.Property(r => r.Rating)
                    .HasColumnName("rating")
                    .IsRequired();

                rv.Property(r => r.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(256)
                    .IsRequired();

                rv.Property(r => r.Attachment)
                    .HasColumnName("attachment");

                rv.Property(r => r.CreatedAt)
                    .HasColumnName("created_at")
                    .IsRequired();

                rv.HasKey(r => r.Id);

                rv.HasOne(r => r.User)
                    .WithMany()
                    .HasForeignKey(r => r.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                rv.HasOne(r => r.Product)
                    .WithMany()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
    }
}
