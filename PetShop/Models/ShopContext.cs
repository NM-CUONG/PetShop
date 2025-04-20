using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Badminton.Models
{
    public partial class ShopContext : DbContext
    {
        public ShopContext()
            : base("name=ShopContext")
        {
        }

        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tb_Brand> tb_Brand { get; set; }
        public virtual DbSet<tb_Category> tb_Category { get; set; }
        public virtual DbSet<tb_Order> tb_Order { get; set; }
        public virtual DbSet<tb_OrderDetail> tb_OrderDetail { get; set; }
        public virtual DbSet<tb_Product> tb_Product { get; set; }
        public virtual DbSet<tb_Role> tb_Role { get; set; }
        public virtual DbSet<tb_User> tb_User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tb_Brand>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Brand>()
                .HasMany(e => e.tb_Product)
                .WithOptional(e => e.tb_Brand)
                .HasForeignKey(e => e.brand_id);

            modelBuilder.Entity<tb_Category>()
                .HasMany(e => e.tb_Product)
                .WithOptional(e => e.tb_Category)
                .HasForeignKey(e => e.category_id);

            modelBuilder.Entity<tb_Order>()
                .Property(e => e.sdt)
                .IsUnicode(false);

            modelBuilder.Entity<tb_Order>()
                .HasMany(e => e.tb_OrderDetail)
                .WithOptional(e => e.tb_Order)
                .HasForeignKey(e => e.order_id);

            modelBuilder.Entity<tb_Product>()
                .Property(e => e.size)
                .IsFixedLength();

            modelBuilder.Entity<tb_Product>()
                .HasMany(e => e.tb_OrderDetail)
                .WithOptional(e => e.tb_Product)
                .HasForeignKey(e => e.product_id);

            modelBuilder.Entity<tb_Role>()
                .HasMany(e => e.tb_User)
                .WithOptional(e => e.tb_Role)
                .HasForeignKey(e => e.role_id);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.phonenumber)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .Property(e => e.passwd)
                .IsUnicode(false);

            modelBuilder.Entity<tb_User>()
                .HasMany(e => e.tb_Order)
                .WithOptional(e => e.tb_User)
                .HasForeignKey(e => e.userid);
        }
    }
}
