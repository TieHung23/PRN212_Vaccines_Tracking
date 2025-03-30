using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Models;

public partial class Prn212VaccinesTracking2Context : DbContext
{
    public Prn212VaccinesTracking2Context()
    {
    }

    public Prn212VaccinesTracking2Context(DbContextOptions<Prn212VaccinesTracking2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Vaccine> Vaccines { get; set; }

    public virtual DbSet<VaccineDetail> VaccineDetails { get; set; }

    public virtual DbSet<VaccinesTracking> VaccinesTrackings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
       // => optionsBuilder.UseSqlServer("Server=LAPTOP-8UGAAJKM\\SQLEXPRESS01;Database=PRN212_Vaccines_Tracking_2;uid=sa;pwd=12345;TrustServerCertificate=True;");
     => optionsBuilder.UseSqlServer("Server=DESKTOP-LIE3GLO\\SQLEXPRESS;Database=PRN212_Vaccines_Tracking_2;uid=sa;pwd=123456;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Booking__3213E83FA3B861E1");

            entity.ToTable("Booking");

            entity.HasIndex(e => e.Id, "UQ__Booking__3213E83EA67F549D").IsUnique();

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

            entity.HasOne(d => d.Parent).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Booking__parentI__619B8048");

            entity.HasMany(d => d.Children).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingChild",
                    r => r.HasOne<Child>().WithMany()
                        .HasForeignKey("ChildId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Booking_C__child__656C112C"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Booking_C__booki__6477ECF3"),
                    j =>
                    {
                        j.HasKey("BookingId", "ChildId").HasName("PK__Booking___04F3A9914287485D");
                        j.ToTable("Booking_Child");
                        j.IndexerProperty<int>("BookingId").HasColumnName("bookingId");
                        j.IndexerProperty<int>("ChildId").HasColumnName("childId");
                    });

            entity.HasMany(d => d.Vaccines).WithMany(p => p.Bookings)
                .UsingEntity<Dictionary<string, object>>(
                    "BookingVaccine",
                    r => r.HasOne<Vaccine>().WithMany()
                        .HasForeignKey("VaccineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Booking_V__vacci__6383C8BA"),
                    l => l.HasOne<Booking>().WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Booking_V__booki__628FA481"),
                    j =>
                    {
                        j.HasKey("BookingId", "VaccineId").HasName("PK__Booking___8ACEE81290F462F5");
                        j.ToTable("Booking_Vaccine");
                        j.IndexerProperty<int>("BookingId").HasColumnName("bookingId");
                        j.IndexerProperty<int>("VaccineId").HasColumnName("vaccineId");
                    });
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Child__3213E83FB8CAAC14");

            entity.ToTable("Child");

            entity.HasIndex(e => e.Id, "UQ__Child__3213E83E00661B62").IsUnique();

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

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Child__parentId__60A75C0F");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3213E83FFCD2FF87");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.Id, "UQ__Customer__3213E83EB5C4FDCD").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
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
            entity.HasKey(e => e.Id).HasName("PK__Vaccine__3213E83F77F998A5");

            entity.ToTable("Vaccine");

            entity.HasIndex(e => e.Id, "UQ__Vaccine__3213E83EA2B580AA").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DosesTimes).HasColumnName("dosesTimes");
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
            entity.HasKey(e => e.VaccineDetailsId).HasName("PK__VaccineD__FC7B323F346E071B");

            entity.Property(e => e.VaccineDetailsId).HasColumnName("vaccineDetailsId");
            entity.Property(e => e.EntryDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("entryDate");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VaccineId).HasColumnName("vaccineId");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccineDetails)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaccineDe__vacci__5FB337D6");
        });

        modelBuilder.Entity<VaccinesTracking>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Vaccines__3213E83FFDBA8864");

            entity.ToTable("VaccinesTracking");

            entity.HasIndex(e => e.Id, "UQ__Vaccines__3213E83E355CF120").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.ChildId).HasColumnName("childId");
            entity.Property(e => e.DateVaccination)
                .HasColumnType("datetime")
                .HasColumnName("dateVaccination");
            entity.Property(e => e.ParentId).HasColumnName("parentId");
            entity.Property(e => e.PreviousId).HasColumnName("previousId");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.VaccineId).HasColumnName("vaccineId");

            entity.HasOne(d => d.Booking).WithMany(p => p.VaccinesTrackings)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaccinesT__booki__693CA210");

            entity.HasOne(d => d.Child).WithMany(p => p.VaccinesTrackings)
                .HasForeignKey(d => d.ChildId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaccinesT__child__6754599E");

            entity.HasOne(d => d.Parent).WithMany(p => p.VaccinesTrackings)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaccinesT__paren__68487DD7");

            entity.HasOne(d => d.Previous).WithMany(p => p.InversePrevious)
                .HasForeignKey(d => d.PreviousId)
                .HasConstraintName("FK__VaccinesT__previ__66603565");

            entity.HasOne(d => d.Vaccine).WithMany(p => p.VaccinesTrackings)
                .HasForeignKey(d => d.VaccineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VaccinesT__vacci__6A30C649");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
