using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GulyaevLib;

public partial class ApplicationContext : DbContext
{
    public ApplicationContext()
    {
    }

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<MaterialType> MaterialTypes { get; set; }

    public virtual DbSet<Partner> Partners { get; set; }

    public virtual DbSet<PartnerProduct> PartnerProducts { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("HOST=localhost;DATABASE=gulyaev_db;USERNAME=app;PASSWORD=123456789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<MaterialType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_type_pkey");

            entity.ToTable("material_type", "app");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoafBreak).HasColumnName("coaf_break");
            entity.Property(e => e.MaterialType1)
                .HasMaxLength(50)
                .HasColumnName("material_type");
        });

        modelBuilder.Entity<Partner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partners_import_pkey");

            entity.ToTable("partners", "app");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('app.partners_import_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.PartnerAddress)
                .HasMaxLength(240)
                .HasColumnName("partner_address");
            entity.Property(e => e.PartnerDirector)
                .HasMaxLength(120)
                .HasColumnName("partner_director");
            entity.Property(e => e.PartnerEmail)
                .HasMaxLength(120)
                .HasColumnName("partner_email");
            entity.Property(e => e.PartnerInn)
                .HasColumnType("character varying")
                .HasColumnName("partner_inn");
            entity.Property(e => e.PartnerName)
                .HasMaxLength(50)
                .HasColumnName("partner_name");
            entity.Property(e => e.PartnerNumber)
                .HasMaxLength(12)
                .HasColumnName("partner_number");
            entity.Property(e => e.PartnerRating).HasColumnName("partner_rating");
            entity.Property(e => e.PartnerType)
                .HasMaxLength(5)
                .HasColumnName("partner_type");
        });

        modelBuilder.Entity<PartnerProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("partner_products_pkey");

            entity.ToTable("partner_products", "app");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Partner).HasColumnName("partner");
            entity.Property(e => e.PartnerCount).HasColumnName("partner_count");
            entity.Property(e => e.Product).HasColumnName("product");
            entity.Property(e => e.SaleDate).HasColumnName("sale_date");

            entity.HasOne(d => d.PartnerNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Partner)
                .HasConstraintName("partner_products_partner_fkey");

            entity.HasOne(d => d.ProductNavigation).WithMany(p => p.PartnerProducts)
                .HasForeignKey(d => d.Product)
                .HasConstraintName("partner_products_product_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products", "app");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PartnerArtc).HasColumnName("partner_artc");
            entity.Property(e => e.PartnerPrice).HasColumnName("partner_price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(120)
                .HasColumnName("product_name");
            entity.Property(e => e.ProductType).HasColumnName("product_type");

            entity.HasOne(d => d.ProductTypeNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.ProductType)
                .HasConstraintName("products_product_type_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_type_pkey");

            entity.ToTable("product_type", "app");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CoafProductType).HasColumnName("coaf_product_type");
            entity.Property(e => e.ProductType1)
                .HasMaxLength(20)
                .HasColumnName("product_type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
