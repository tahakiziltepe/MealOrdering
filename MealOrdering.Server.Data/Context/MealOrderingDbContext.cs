using MealOrdering.Server.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MealOrdering.Server.Data.Context
{
	public class MealOrderingDbContext:DbContext
	{
        public MealOrderingDbContext(DbContextOptions<MealOrderingDbContext> options) : base(options)
        {   
        }

        public virtual DbSet<Users> Users { get; set; }
		public virtual DbSet<Orders> Orders { get; set; }
		public virtual DbSet<OrderItems> OrderItems { get; set; }
		public virtual DbSet<Suppliers> Suppliers { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Users>(entity =>
			{
				entity.ToTable("users", "dbo");
				
				entity.HasKey(e => e.Id).HasName("pk_user_id");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("uuid")
					.IsRequired();

				entity.Property(e => e.FirstName)
					.HasColumnName("first_name")
					.HasColumnType("varchar")
					.HasMaxLength(100);

				entity.Property(e => e.LastName)
					.HasColumnName("last_name")
					.HasColumnType("varchar")
					.HasMaxLength(100);

				entity.Property(e => e.EMailAddress)
					.HasColumnName("email_address")
					.HasColumnType("varchar")
					.HasMaxLength(100);

				entity.Property(e => e.CreateDate)
					.HasColumnName("create_date")
					.HasColumnType("timestamp without time zone")
					.HasDefaultValueSql("now()");

				entity.Property(e => e.IsActive)
					.HasColumnName("is_active")
					.HasColumnType("boolean")
					.HasDefaultValueSql("true");
			});

			modelBuilder.Entity<Orders>(entity =>
			{
				entity.ToTable("orders", "dbo");

				entity.HasKey(e => e.Id).HasName("pk_order_id");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("uuid")
					.IsRequired();

				entity.Property(e => e.CreateDate)
					.HasColumnName("create_date")
					.HasColumnType("timestamp without time zone")
					.HasDefaultValueSql("now()")
					.ValueGeneratedOnAdd();

				entity.Property(e => e.CreateUserId)
					.HasColumnName("create_user_id")
					.HasColumnType("uuid");

				entity.Property(e => e.SupplierId)
					.HasColumnName("supplier_id")
					.HasColumnType("uuid");

				entity.Property(e => e.Name)
					.HasColumnName("name")
					.HasColumnType("varchar")
					.HasMaxLength(100);

				entity.Property(e => e.Description)
					.HasColumnName("description")
					.HasColumnType("varchar")
					.HasMaxLength(1000);

				entity.Property(e => e.ExpireDate)
					.HasColumnName("expire_date")
					.HasColumnType("timestamp without time zone");

				entity.HasOne(d => d.CreateUser)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.CreateUserId)
					.HasConstraintName("fk_user_order_id")
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(d => d.Supplier)
					.WithMany(p => p.Orders)
					.HasForeignKey(d => d.SupplierId)
					.HasConstraintName("fk_supplier_order_id")
					.OnDelete(DeleteBehavior.Cascade);

			});

			modelBuilder.Entity<OrderItems>(entity =>
			{
				entity.ToTable("order_items", "dbo");

				entity.HasKey(e => e.Id).HasName("pk_order_item_id");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.HasColumnType("uuid")
					.IsRequired();

				entity.Property(e => e.CreateDate)
					.HasColumnName("create_date")
					.HasColumnType("timestamp without time zone")
					.HasDefaultValueSql("now()")
					.ValueGeneratedOnAdd();

				entity.Property(e => e.CreatedUserId)
					.HasColumnName("created_user_id")
					.HasColumnType("uuid");

				entity.Property(e => e.OrderId)
					.HasColumnName("order_id")
					.HasColumnType("uuid");

				entity.Property(e => e.Description)
					.HasColumnName("description")
					.HasColumnType("varchar")
					.HasMaxLength(1000);

				entity.HasOne(d => d.Order)
					.WithMany(p => p.OrderItems)
					.HasForeignKey(d => d.OrderId)
					.HasConstraintName("fk_orderitems_order_id")
					.OnDelete(DeleteBehavior.Cascade);

				entity.HasOne(d => d.CreatedUser)
					.WithMany(p => p.CreatedOrderItems)
					.HasForeignKey(d => d.CreatedUserId)
					.HasConstraintName("fk_orderitems_user_id")
					.OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<Suppliers>(entity =>
			{
				entity.ToTable("suppliers", "dbo");

				entity.HasKey(e => e.Id).HasName("pk_supplier_id");

				entity.Property(e => e.Id)
					.HasColumnName("id")
					.IsRequired();

				entity.Property(e => e.CreateDate)
					.HasColumnName("create_date")
					.HasDefaultValueSql("getdate()")
					.ValueGeneratedOnAdd();

				entity.Property(e => e.Name)
					.HasColumnName("name")
					.HasColumnType("varchar")
					.HasMaxLength(100);

				entity.Property(e => e.IsActive)
					.HasColumnName("is_active")
					.HasColumnType("boolean")
					.HasDefaultValueSql("true");

				entity.Property(e => e.WebURL)
					.HasColumnName("web_url")
					.HasColumnType("varchar")
					.HasMaxLength(500);

				
			});	

			base.OnModelCreating(modelBuilder);
		}
	}
}
