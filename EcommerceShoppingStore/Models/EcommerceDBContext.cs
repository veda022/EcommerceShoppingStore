using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EcommerceShoppingStore.Models
{
    public partial class EcommerceDBContext : DbContext
    {
        public EcommerceDBContext()
        {
        }

        public EcommerceDBContext(DbContextOptions<EcommerceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
         //     optionsBuilder.UseSqlServer("Server=DESKTOP-1VBGC6E\\SQLEXPRESS;Database=EcommerceDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("Category_Id");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Category_Name");

                entity.Property(e => e.Category_Description).IsUnicode(false);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .ValueGeneratedNever()
                    .HasColumnName("Customer_Id");

                entity.Property(e => e.DeliveryAddress)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("Delivery_Address");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Full_Name");

                entity.Property(e => e.Password)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Order_Id");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Order_Date");

                entity.Property(e => e.ShipDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ship_Date");

                //entity.HasOne(d => d.Customer)
                //    .WithMany(p => p.Orders)
                //    .HasForeignKey(d => d.CustomerId)
                //    .HasConstraintName("FK__Orders__Customer__114A936A");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__OrderDet__F1E4607BD8B12F31");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedNever()
                    .HasColumnName("Order_Id");

                entity.Property(e => e.ProductsId).HasColumnName("Products_Id");

                entity.Property(e => e.UnitCost).HasColumnName("Unit_Cost");

                //entity.HasOne(d => d.Products)
                //    .WithMany(p => p.OrderDetails)
                //    .HasForeignKey(d => d.ProductsId)
                //    .HasConstraintName("FK__OrderDeta__Produ__0B91BA14");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductsId)
                    .HasName("PK__Products__5EABD29AC850EAED");

                entity.Property(e => e.ProductsId)
                    .ValueGeneratedNever()
                    .HasColumnName("Products_Id");

                entity.Property(e => e.CategoryId).HasColumnName("Category_Id");

                entity.Property(e => e.Product_Description).IsUnicode(false);

                entity.Property(e => e.ModelName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UnitCost).HasColumnName("Unit_Cost");

                //entity.HasOne(d => d.Category)
                //    .WithMany(p => p.Products)
                //    .HasForeignKey(d => d.CategoryId)
                //    .HasConstraintName("FK__Products__Catego__08B54D69");
            });

            modelBuilder.Entity<ShoppingCart>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("PK__Shopping__603A0C40FC046C68");

                entity.ToTable("ShoppingCart");

                entity.Property(e => e.RecordId)
                    .ValueGeneratedNever()
                    .HasColumnName("Record_Id");

                entity.Property(e => e.CartId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Cart_Id");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                //entity.HasOne(d => d.Product)
                //    .WithMany(p => p.ShoppingCarts)
                //    .HasForeignKey(d => d.ProductId)
                //    .HasConstraintName("FK__ShoppingC__Produ__14270015");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
