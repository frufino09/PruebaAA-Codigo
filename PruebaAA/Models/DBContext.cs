﻿using Microsoft.EntityFrameworkCore;

namespace PruebaAA.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-5PUB6O5;Database=Prueba;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PointOfSale)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
