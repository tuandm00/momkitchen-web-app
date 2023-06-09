﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace momkitchen.Models;

public partial class MomkitchenContext : DbContext
{
    public MomkitchenContext()
    {
    }

    public MomkitchenContext(DbContextOptions<MomkitchenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Batch> Batches { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Chef> Chefs { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<DishFoodPackage> DishFoodPackages { get; set; }

    public virtual DbSet<DishType> DishTypes { get; set; }

    public virtual DbSet<FoodPackage> FoodPackages { get; set; }

    public virtual DbSet<FoodPackageStyle> FoodPackageStyles { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SessionPackage> SessionPackages { get; set; }

    public virtual DbSet<SessionShipper> SessionShippers { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK_User");

            entity.ToTable("Account");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.AccountStatus).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Account_Role");
        });

        modelBuilder.Entity<Batch>(entity =>
        {
            entity.ToTable("Batch");

            entity.HasOne(d => d.Session).WithMany(p => p.Batches)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Batch_Session");

            entity.HasOne(d => d.Shipper).WithMany(p => p.Batches)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK_Batch_Shipper");
        });

        modelBuilder.Entity<Building>(entity =>
        {
            entity.ToTable("Building");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Chef>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_MomKitchen");

            entity.ToTable("Chef");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.Building).WithMany(p => p.Chefs)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_Chef_Building");

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Chefs)
                .HasForeignKey(d => d.Email)
                .HasConstraintName("FK_Chef_Account");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("Customer");

            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Customers)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customer_Account");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("Dish");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Chef).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.ChefId)
                .HasConstraintName("FK_Dish_MomKitchen");

            entity.HasOne(d => d.DishType).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.DishTypeId)
                .HasConstraintName("FK_Dish_DishType");
        });

        modelBuilder.Entity<DishFoodPackage>(entity =>
        {
            entity.HasKey(e => new { e.DishId, e.FoodPackageId });

            entity.ToTable("Dish_FoodPackage");

            entity.HasOne(d => d.Dish).WithMany(p => p.DishFoodPackages)
                .HasForeignKey(d => d.DishId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dish_FoodPackage_Dish");

            entity.HasOne(d => d.FoodPackage).WithMany(p => p.DishFoodPackages)
                .HasForeignKey(d => d.FoodPackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Dish_FoodPackage_FoodPackage");
        });

        modelBuilder.Entity<DishType>(entity =>
        {
            entity.ToTable("DishType");

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Chef).WithMany(p => p.DishTypes)
                .HasForeignKey(d => d.ChefId)
                .HasConstraintName("FK_DishType_Chef");
        });

        modelBuilder.Entity<FoodPackage>(entity =>
        {
            entity.ToTable("FoodPackage");

            entity.Property(e => e.DefaultPrice).HasColumnType("money");
            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Chef).WithMany(p => p.FoodPackages)
                .HasForeignKey(d => d.ChefId)
                .HasConstraintName("FK_FoodPackage_Chef");

            entity.HasOne(d => d.FoodPackageStyle).WithMany(p => p.FoodPackages)
                .HasForeignKey(d => d.FoodPackageStyleId)
                .HasConstraintName("FK_FoodPackage_FoodPackageStyle");
        });

        modelBuilder.Entity<FoodPackageStyle>(entity =>
        {
            entity.ToTable("FoodPackageStyle");

            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Chef).WithMany(p => p.FoodPackageStyles)
                .HasForeignKey(d => d.ChefId)
                .HasConstraintName("FK_FoodPackageStyle_Chef");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.ToTable("Notification");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content)
                .HasMaxLength(50)
                .HasColumnName("content");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Order\\");

            entity.ToTable("Order");

            entity.Property(e => e.CustomerPhone).HasMaxLength(50);
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeliveryStatus).HasMaxLength(50);
            entity.Property(e => e.DeliveryTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Batch).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BatchId)
                .HasConstraintName("FK_Order_Batch");

            entity.HasOne(d => d.Building).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BuildingId)
                .HasConstraintName("FK_Order_Building");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.Session).WithMany(p => p.Orders)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_Order_Session");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SessionPackageId, e.OrderId });

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order1");

            entity.HasOne(d => d.SessionPackage).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.SessionPackageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_SessionPackage");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.Amount).HasColumnType("money");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.Type).HasMaxLength(50);

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Order");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.ToTable("Session");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndTime).HasColumnType("datetime");
            entity.Property(e => e.StartTime).HasColumnType("datetime");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");
        });

        modelBuilder.Entity<SessionPackage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FoodPackageInSession");

            entity.ToTable("SessionPackage");

            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FoodPackage).WithMany(p => p.SessionPackages)
                .HasForeignKey(d => d.FoodPackageId)
                .HasConstraintName("FK_FoodPackageInSession_FoodPackage");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionPackages)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("FK_FoodPackageInSession_Session");
        });

        modelBuilder.Entity<SessionShipper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SessionShipper_1");

            entity.ToTable("SessionShipper");

            entity.HasOne(d => d.Session).WithMany(p => p.SessionShippers)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SessionShipper_Session");

            entity.HasOne(d => d.Shipper).WithMany(p => p.SessionShippers)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK_SessionShipper_Shipper");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_SHipper");

            entity.ToTable("Shipper");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);

            entity.HasOne(d => d.EmailNavigation).WithMany(p => p.Shippers)
                .HasForeignKey(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shipper_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
