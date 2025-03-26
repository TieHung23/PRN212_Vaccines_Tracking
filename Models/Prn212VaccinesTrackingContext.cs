using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Models;

public partial class Prn212VaccinesTrackingContext : DbContext
{
    public Prn212VaccinesTrackingContext()
    {
    }

    public Prn212VaccinesTrackingContext(DbContextOptions<Prn212VaccinesTrackingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<BookingChild> BookingChildren { get; set; }

    public virtual DbSet<BookingVaccine> BookingVaccines { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineDetail> VaccineDetails { get; set; }

    public virtual DbSet<VaccinesTracking> VaccinesTrackings { get; set; }

    protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder )
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath( Directory.GetCurrentDirectory() )
            .AddJsonFile( "appsettings.json" )
            .Build();

        var connectionString = configuration.GetConnectionString( "DBDefault" );
        optionsBuilder.UseSqlServer( connectionString );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3213E83FF01F6EE2");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.Id, "UQ__Booking__3213E83EB8810537").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.FinalPrice)
                .HasColumnType("money")
                .HasColumnName("finalPrice");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<BookingChild>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.ChildId }).HasName("PK__Booking___04F3A9918AF02742");

            entity.ToTable("Booking_Child");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ChildId).HasColumnName("childId");
        });

        modelBuilder.Entity<BookingVaccine>(entity =>
        {
            entity.HasKey(e => new { e.BookingId, e.VaccineId }).HasName("PK__Booking___8ACEE812AB92C763");

            entity.ToTable("Booking_Vaccine");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.VaccineId).HasColumnName("vaccineId");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3213E83F07CC5615");

            entity.ToTable("Child");

            entity.HasIndex(e => e.Id, "UQ__Child__3213E83E1FF514EB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("dateOfBirth");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FD88348B1");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Id, "UQ__Customer__3213E83EE618B659").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("dateOfBirth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Vaccine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vaccine__3213E83FB72F8B8A");

            entity.ToTable("Vaccine");

            entity.HasIndex(e => e.Id, "UQ__Vaccine__3213E83EC42DF7B5").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");
        });

        modelBuilder.Entity<VaccineDetail>(entity =>
        {
            entity.HasKey(e => e.VaccineDetailsId).HasName("PK__VaccineD__FC7B323FFF6F72F0");

            entity.Property(e => e.VaccineDetailsId).HasColumnName("vaccineDetailsId");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("entryDate");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VaccineId).HasColumnName("vaccineId");
        });

        modelBuilder.Entity<VaccinesTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vaccines__3213E83F649F48A9");

            entity.ToTable("VaccinesTracking");

            entity.HasIndex(e => e.Id, "UQ__Vaccines__3213E83E74234945").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ChildId).HasColumnName("childId");
            entity.Property(e => e.DateVaccination)
                .HasColumnType("datetime")
                .HasColumnName("dateVaccination");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.PreviousId).HasColumnName("previousId");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
