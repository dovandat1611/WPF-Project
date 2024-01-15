using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SalesWPFAppLibrary.DataAccess;

public partial class SalesWpfappContext : DbContext
{
    public SalesWpfappContext()
    {
    }

    public SalesWpfappContext(DbContextOptions<SalesWpfappContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyCnn"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B785D668B");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Members__0CF04B189E6BC052");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(30);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF3819D2BC");

            entity.ToTable("Order");

            entity.Property(e => e.OrderDate).HasColumnType("datetime");
            entity.Property(e => e.ShippedDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Order_Members");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C94815802");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD71361E4D");

            entity.ToTable("Product");

            entity.Property(e => e.ProductName).HasMaxLength(255);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Product_Category");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
