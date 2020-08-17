using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BarWebApplication.Models
{
    public partial class WebAppContext : DbContext
    {
        public WebAppContext()
        {
        }

        public WebAppContext(DbContextOptions<WebAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DailyDeal> DailyDeal { get; set; }
        public virtual DbSet<DailyDealOrder> DailyDealOrder { get; set; }
        public virtual DbSet<Item> Item { get; set; }
        public virtual DbSet<ItemOrder> ItemOrder { get; set; }
        public virtual DbSet<OrderType> OrderType { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("DeployedApp");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DailyDeal>(entity =>
            {
                entity.HasKey(e => e.DailyDealId)
                    .HasName("PK__DailyDea__2E1B31B29C667241");

                entity.Property(e => e.DailyDealId).HasColumnName("DailyDealID");

                entity.Property(e => e.DailyDealDate).HasColumnType("datetime");

                entity.Property(e => e.DailyDealDescription).HasMaxLength(500);

                entity.Property(e => e.DailyDealName).HasMaxLength(50);

                entity.Property(e => e.DailyDealPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<DailyDealOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderMessage).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.DailyDealNavigation)
                    .WithMany(p => p.DailyDealOrder)
                    .HasForeignKey(d => d.DailyDeal)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.OrderTypeNavigation)
                    .WithMany(p => p.DailyDealOrder)
                    .HasForeignKey(d => d.OrderType)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.Property(e => e.ItemId).HasColumnName("ItemID");

                entity.Property(e => e.ItemDescription).HasMaxLength(500);

                entity.Property(e => e.ItemName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Item)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Item__Category__398D8EEE");
            });

            modelBuilder.Entity<ItemOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__ItemOrde__C3905BAF9BDD4A3C");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.OrderMessage).HasMaxLength(500);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ItemNavigation)
                    .WithMany(p => p.ItemOrder)
                    .HasForeignKey(d => d.Item)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ItemOrder__Item__3E52440B");

                entity.HasOne(d => d.OrderTypeNavigation)
                    .WithMany(p => p.ItemOrder)
                    .HasForeignKey(d => d.OrderType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ItemOrder__Order__3F466844");
            });

            modelBuilder.Entity<OrderType>(entity =>
            {
                entity.HasKey(e => e.TypeId)
                    .HasName("PK__OrderTyp__516F0395FC748589");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.OrderType1)
                    .IsRequired()
                    .HasColumnName("OrderType")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(e => e.ReservationId).HasColumnName("ReservationID");

                entity.Property(e => e.DateAndTime).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
