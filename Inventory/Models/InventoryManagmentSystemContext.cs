using Inventory.DTO_S.Product;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Inventory.Models;

public partial class InventoryManagmentSystemContext : IdentityDbContext<ApplicationUser>
{
    public InventoryManagmentSystemContext()
    {
    }

    public InventoryManagmentSystemContext(DbContextOptions<InventoryManagmentSystemContext> options)
        : base(options)
    {
    }



    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductStatus> ProductStatuses { get; set; }

    public virtual DbSet<StockTransaction> StockTransactions { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<TransactionType> TransactionTypes { get; set; }

    public virtual DbSet<UserStatus> UserStatuses { get; set; }

    public virtual DbSet<FlatProductTransactionDTO> FlatProductTransactionDTOs { get; set; }

    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.Entity<IdentityRole>().HasData(
        new IdentityRole
        {
            Id = "aad9d1e7-b5b7-44b3-a90d-dc0f8fe0a1af", // fixed GUID for Admin
            Name = "Admin",
            NormalizedName = "ADMIN"
        },
        new IdentityRole
        {
            Id = "ec7312cb-1545-4dc1-b03e-4cf670e0e99f", // fixed GUID for Employee
            Name = "Employee",
            NormalizedName = "EMPLOYEE"
        }
    );


        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.gender)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.GenderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ApplicationUser>()
            .HasOne(u => u.Status)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.UserStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK__gender__9DF18F87656F8EC2");

            entity.ToTable("gender");

            entity.HasIndex(e => e.Gender1, "UQ__gender__8FA091312CF668B9").IsUnique();

            entity.HasIndex(e => e.GenderId, "UQ__gender__9DF18F86AA5B77CA").IsUnique();

            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.Gender1)
                .HasMaxLength(255)
                .HasColumnName("gender");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__product__47027DF57021F2BD");

            entity.ToTable("product");

            entity.HasIndex(e => e.ProductId, "UQ__product__47027DF425E604BD").IsUnique();

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity)
                .HasMaxLength(255)
                .HasColumnName("quantity");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fk3");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fk5");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_fk4");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK__product___1F8847F959D6C32C");

            entity.ToTable("product_category");

            entity.HasIndex(e => e.ProductCategoryId, "UQ__product___1F8847F875EAC040").IsUnique();

            entity.HasIndex(e => e.ProductCategoryName, "UQ__product___F8C236584045FA61").IsUnique();

            entity.Property(e => e.ProductCategoryId).HasColumnName("product_category_id");
            entity.Property(e => e.ProductCategoryName)
                .HasMaxLength(255)
                .HasColumnName("product_category_name");
        });

        modelBuilder.Entity<ProductStatus>(entity =>
        {
            entity.HasKey(e => e.ProductStatusId).HasName("PK__product___D299CFCFF06B02B1");

            entity.ToTable("product_status");

            entity.HasIndex(e => e.ProductStatusId, "UQ__product___D299CFCE7E291AFD").IsUnique();

            entity.HasIndex(e => e.ProductStatus1, "UQ__product___E5570513CA96E386").IsUnique();

            entity.Property(e => e.ProductStatusId).HasColumnName("product_status_id");
            entity.Property(e => e.ProductStatus1)
                .HasMaxLength(255)
                .HasColumnName("product_status");
        });

        modelBuilder.Entity<StockTransaction>(entity =>
        {
            entity.HasKey(e => e.TransId).HasName("PK__stock_tr__438CAC18DF82C25A");

            entity.ToTable("stock_transactions");

            entity.HasIndex(e => e.TransId, "UQ__stock_tr__438CAC19149220D1").IsUnique();

            entity.Property(e => e.TransId).HasColumnName("trans_id");
            entity.Property(e => e.DataTime)
                .HasColumnType("datetime")
                .HasColumnName("data_time");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasMaxLength(255)
                .HasColumnName("quantity");
            entity.Property(e => e.TransTypeId).HasColumnName("trans_type_id");
            entity.Property(e => e.UserId).HasColumnName("UserId");

            entity.HasOne(d => d.Product).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_transactions_fk1");

            entity.HasOne(d => d.TransType).WithMany(p => p.StockTransactions)
                .HasForeignKey(d => d.TransTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stock_transactions_fk4");

            entity.HasOne(d => d.User)
    .WithMany()
    .HasForeignKey(d => d.UserId)
    
    .HasConstraintName("stock_transactions_fk_user");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__supplier__6EE594E87CF0C77A");

            entity.ToTable("supplier");

            entity.HasIndex(e => e.SupplierId, "UQ__supplier__6EE594E9A64ACE3F").IsUnique();

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(255)
                .HasColumnName("contact_person");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(255)
                .HasColumnName("phone_number");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(255)
                .HasColumnName("supplier_name");
        });

        modelBuilder.Entity<TransactionType>(entity =>
        {
            entity.HasKey(e => e.TypeId).HasName("PK__transact__2C0005980B5A8712");

            entity.ToTable("transaction_type");

            entity.HasIndex(e => e.TypeId, "UQ__transact__2C000599B78C7F14").IsUnique();

            entity.HasIndex(e => e.TypeName, "UQ__transact__543C4FD99B6DD033").IsUnique();

            entity.Property(e => e.TypeId).HasColumnName("type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<UserStatus>(entity =>
        {
            entity.HasKey(e => e.UserStatusId).HasName("PK__user_sta__8E5BDDEF6E232831");

            entity.ToTable("user_status");

            entity.HasIndex(e => e.UserStatusId, "UQ__user_sta__8E5BDDEE9C0FBDE7").IsUnique();

            entity.Property(e => e.UserStatusId).HasColumnName("user_status_id");
            entity.Property(e => e.UserStatus1).HasColumnName("user_status");
        });

        modelBuilder.Entity<FlatProductTransactionDTO>().HasNoKey();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
