using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class ControlAccessContext : DbContext
{
    public ControlAccessContext()
    {
    }

    public ControlAccessContext(DbContextOptions<ControlAccessContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccessVisitor> AccessVisitors { get; set; }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Device> Devices { get; set; }

    public virtual DbSet<House> Houses { get; set; }

    public virtual DbSet<Resident> Residents { get; set; }

    public virtual DbSet<UserAc> UserAcs { get; set; }

    public virtual DbSet<Visitor> Visitors { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccessVisitor>(entity =>
        {
            entity.HasKey(e => e.AccessVisitorId).HasName("PK__AccessVi__AD7F8B29DBC9B580");

            entity.ToTable("AccessVisitor");

            entity.Property(e => e.AccessVisitorEntry).HasColumnType("datetime");
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Car__68A0342EEA1DAA51");

            entity.ToTable("Car");

            entity.Property(e => e.CarBrand)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CarColor)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CarModel)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CarPlate)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__Card__55FECDAEBF0D7C5C");

            entity.ToTable("Card");

            entity.Property(e => e.CardCode)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Resident).WithMany(p => p.Cards)
                .HasForeignKey(d => d.ResidentId)
                .HasConstraintName("FK__Card__ResidentId__412EB0B6");
        });

        modelBuilder.Entity<Device>(entity =>
        {
            entity.HasKey(e => e.DeviceId).HasName("PK__Device__49E12311B4E9FF5D");

            entity.ToTable("Device");

            entity.Property(e => e.DeviceName)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.UserAc).WithMany(p => p.Devices)
                .HasForeignKey(d => d.UserAcId)
                .HasConstraintName("FK__Device__UserCaId__49C3F6B7");

            entity.HasOne(d => d.Zone).WithMany(p => p.Devices)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK__Device__CotoId__48CFD27E");
        });

        modelBuilder.Entity<House>(entity =>
        {
            entity.HasKey(e => e.HouseId).HasName("PK__House__085D128FE20DB5B2");

            entity.ToTable("House");

            entity.Property(e => e.HouseStreet)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Zone).WithMany(p => p.Houses)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK__House__ZoneId__3B75D760");
        });

        modelBuilder.Entity<Resident>(entity =>
        {
            entity.HasKey(e => e.ResidentId).HasName("PK__Resident__07FB00DCDE2BA011");

            entity.ToTable("Resident");

            entity.Property(e => e.ResidentLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ResidentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.House).WithMany(p => p.Residents)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Resident__HouseI__3E52440B");
        });

        modelBuilder.Entity<UserAc>(entity =>
        {
            entity.HasKey(e => e.UserAcId).HasName("PK__UserCa__0077DABB70ABDDC8");

            entity.ToTable("UserAc");

            entity.Property(e => e.UserAcLastLogin).HasColumnType("datetime");
            entity.Property(e => e.UserAcMqttTopic)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserAcName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserAcPassword)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.VisitorId).HasName("PK__Visitor__B121AF88476D483A");

            entity.ToTable("Visitor");

            entity.Property(e => e.VisitorLastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VisitorName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Car).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.CarId)
                .HasConstraintName("FK__Visitor__CarId__6E01572D");

            entity.HasOne(d => d.House).WithMany(p => p.Visitors)
                .HasForeignKey(d => d.HouseId)
                .HasConstraintName("FK__Visitor__HouseId__6EF57B66");
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.HasKey(e => e.ZoneId).HasName("PK__Zone__601667B5E02638CF");

            entity.ToTable("Zone");

            entity.Property(e => e.ZoneName)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
