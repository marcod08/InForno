using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InForno.Models;

public partial class InFornoContext : DbContext
{
    public InFornoContext()
    {
    }

    public InFornoContext(DbContextOptions<InFornoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Detail> Details { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Pizza> Pizzas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Detail>(entity =>
        {
            entity.ToTable("Detail");

            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Drink).WithMany(p => p.Details)
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK_Detail_Drink");

            entity.HasOne(d => d.Order).WithMany(p => p.Details)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detail_Order");

            entity.HasOne(d => d.Pizza).WithMany(p => p.Details)
                .HasForeignKey(d => d.PizzaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Detail_Pizza");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.ToTable("Drink");

            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_User");
        });

        modelBuilder.Entity<Pizza>(entity =>
        {
            entity.ToTable("Pizza");

            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Role).HasMaxLength(5);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
