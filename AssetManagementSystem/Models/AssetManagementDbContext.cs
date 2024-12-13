using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssetManagementSystem.Models;

public partial class AssetManagementDbContext : DbContext
{
    public AssetManagementDbContext()
    {
    }

    public AssetManagementDbContext(DbContextOptions<AssetManagementDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetDefinition> AssetDefinitions { get; set; }

    public virtual DbSet<AssetType> AssetTypes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }

    public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-27LJRR9\\SQLEXPRESS;Initial Catalog=AssetManagementSystem;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.HasKey(e => e.AmId).HasName("PK__Assets__B95A8ED0A41E273F");

            entity.HasIndex(e => e.AmSnumber, "UQ__Assets__70178B2D449E0503").IsUnique();

            entity.HasIndex(e => e.AssetNumber, "UQ__Assets__856CE34BA21C8D1B").IsUnique();

            entity.Property(e => e.AmId).HasColumnName("am_id");
            entity.Property(e => e.AmAdId).HasColumnName("am_ad_id");
            entity.Property(e => e.AmAtypeId).HasColumnName("am_atype_id");
            entity.Property(e => e.AmMakeId).HasColumnName("am_make_id");
            entity.Property(e => e.AmModel)
                .HasMaxLength(40)
                .HasColumnName("am_model");
            entity.Property(e => e.AmMyyear)
                .HasMaxLength(10)
                .HasColumnName("am_myyear");
            entity.Property(e => e.AmPdate)
                .HasColumnType("date")
                .HasColumnName("am_pdate");
            entity.Property(e => e.AmSnumber)
                .HasMaxLength(20)
                .HasColumnName("am_snumber");
            entity.Property(e => e.AmStatus)
                .HasMaxLength(20)
                .HasDefaultValueSql("('Active')")
                .HasColumnName("am_status");
            entity.Property(e => e.AmWarranty)
                .HasMaxLength(1)
                .HasColumnName("am_warranty");

            entity.HasOne(d => d.AmAd).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AmAdId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__am_ad_id__5165187F");

            entity.HasOne(d => d.AmAtype).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AmAtypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__am_atype__4F7CD00D");

            entity.HasOne(d => d.AmMake).WithMany(p => p.Assets)
                .HasForeignKey(d => d.AmMakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Assets__am_make___5070F446");
        });

        modelBuilder.Entity<AssetDefinition>(entity =>
        {
            entity.HasKey(e => e.AdId).HasName("PK__AssetDef__CAA4A627DF256C02");

            entity.ToTable("AssetDefinition");

            entity.Property(e => e.AdId).HasColumnName("ad_id");
            entity.Property(e => e.AdClass)
                .HasMaxLength(50)
                .HasColumnName("ad_class");
            entity.Property(e => e.AdName)
                .HasMaxLength(100)
                .HasColumnName("ad_name");
            entity.Property(e => e.AdTypeId).HasColumnName("ad_type_id");

            entity.HasOne(d => d.AdType).WithMany(p => p.AssetDefinitions)
                .HasForeignKey(d => d.AdTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AssetDefi__ad_ty__49C3F6B7");
        });

        modelBuilder.Entity<AssetType>(entity =>
        {
            entity.HasKey(e => e.AtId).HasName("PK__AssetTyp__61F8598883DB66B1");

            entity.ToTable("AssetType");

            entity.Property(e => e.AtId).HasColumnName("at_id");
            entity.Property(e => e.AtName)
                .HasMaxLength(50)
                .HasColumnName("at_name");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.LId).HasName("PK__Login__A7C7B6F8F085A3AF");

            entity.ToTable("Login");

            entity.HasIndex(e => e.Username, "UQ__Login__536C85E4D2700711").IsUnique();

            entity.Property(e => e.LId).HasColumnName("l_id");
            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Login__RoleId__3A81B327");
        });

        modelBuilder.Entity<MaintenanceRecord>(entity =>
        {
            entity.HasKey(e => e.MrId).HasName("PK__Maintena__AE8D85FC4EFC6FC4");

            entity.Property(e => e.MrId).HasColumnName("mr_id");
            entity.Property(e => e.Cost).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.MaintenanceDate).HasColumnType("date");

            entity.HasOne(d => d.Asset).WithMany(p => p.MaintenanceRecords)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Maintenan__Asset__5812160E");
        });

        modelBuilder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(e => e.PoId).HasName("PK__Purchase__368DA7F0791C1E3A");

            entity.ToTable("PurchaseOrder");

            entity.Property(e => e.PoId).HasColumnName("po_id");
            entity.Property(e => e.PoDate)
                .HasColumnType("date")
                .HasColumnName("po_date");
            entity.Property(e => e.PoTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("po_total");
            entity.Property(e => e.VndId).HasColumnName("vnd_id");

            entity.HasOne(d => d.PurchasedByNavigation).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.PurchasedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__Purch__5535A963");

            entity.HasOne(d => d.Vnd).WithMany(p => p.PurchaseOrders)
                .HasForeignKey(d => d.VndId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PurchaseO__vnd_i__5441852A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A677CA43E");

            entity.ToTable("Role");

            entity.HasIndex(e => e.UserType, "UQ__Role__87E7869134327B2D").IsUnique();

            entity.Property(e => e.UserType).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UId).HasName("PK__Users__B51D3DEA849F89C2");

            entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E38C6DE2D88").IsUnique();

            entity.HasIndex(e => e.LId, "UQ__Users__A7C7B6F98B2A2FFC").IsUnique();

            entity.Property(e => e.UId).HasColumnName("u_id");
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.LId).HasColumnName("l_id");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(10);

            entity.HasOne(d => d.LIdNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.LId)
                .HasConstraintName("FK__Users__l_id__3F466844");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VndId).HasName("PK__Vendor__99928060356A469C");

            entity.ToTable("Vendor");

            entity.Property(e => e.VndId).HasColumnName("vnd_id");
            entity.Property(e => e.VndAddr)
                .HasMaxLength(200)
                .HasColumnName("vnd_addr");
            entity.Property(e => e.VndName)
                .HasMaxLength(100)
                .HasColumnName("vnd_name");

            entity.HasMany(d => d.Ats).WithMany(p => p.Vnds)
                .UsingEntity<Dictionary<string, object>>(
                    "VendorAssetType",
                    r => r.HasOne<AssetType>().WithMany()
                        .HasForeignKey("AtId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VendorAss__at_id__46E78A0C"),
                    l => l.HasOne<Vendor>().WithMany()
                        .HasForeignKey("VndId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__VendorAss__vnd_i__45F365D3"),
                    j =>
                    {
                        j.HasKey("VndId", "AtId").HasName("PK__VendorAs__0F8D05F8C281C16F");
                        j.ToTable("VendorAssetType");
                        j.IndexerProperty<int>("VndId").HasColumnName("vnd_id");
                        j.IndexerProperty<int>("AtId").HasColumnName("at_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
