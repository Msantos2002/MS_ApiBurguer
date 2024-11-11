using System;
using System.Collections.Generic;
using MS_ApiBurguer.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MS_ApiBurguer.Data;

public partial class MsWebBurguerContextContext : DbContext
{
    public MsWebBurguerContextContext()
    {
    }

    public MsWebBurguerContextContext(DbContextOptions<MsWebBurguerContextContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Burguer> Burguers { get; set; }

    public virtual DbSet<Promo> Promos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Ms_WebBurguerContext");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Burguer>(entity =>
        {
            entity.ToTable("Burguer");

            entity.Property(e => e.BurguerId).HasColumnName("BurguerID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PriceCelling).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Promo>(entity =>
        {
            entity.ToTable("Promo");

            entity.HasIndex(e => e.BurgerId, "IX_Promo_BurgerID");

            entity.Property(e => e.BurgerId).HasColumnName("BurgerID");

            entity.HasOne(d => d.Burger).WithMany(p => p.Promos).HasForeignKey(d => d.BurgerId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
