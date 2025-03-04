using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Models;

public partial class PrivateGymDbContext : DbContext
{
    public PrivateGymDbContext()
    {
    }

    public PrivateGymDbContext(DbContextOptions<PrivateGymDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<PersonalTrainer> PersonalTrainers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<TrainingPackage> TrainingPackages { get; set; }

    public virtual DbSet<TrainingSchedule> TrainingSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPtBooking> UserPtBookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DUY;Database=PrivateGymDB;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D105350B457F43");

            entity.ToTable("Account");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6ABAE3D97");

            entity.Property(e => e.DiscountId)
                .ValueGeneratedNever()
                .HasColumnName("DiscountID");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DiscountPercent).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD5B0E07ACF");

            entity.Property(e => e.InvoiceId)
                .ValueGeneratedNever()
                .HasColumnName("InvoiceID");
            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserPhone)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Discount).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.DiscountId)
                .HasConstraintName("FK__Invoices__Discou__5441852A");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Invoices__UserID__5535A963");
        });

        modelBuilder.Entity<PersonalTrainer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Personal__A9D1053570E11C52");

            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Achievements).HasColumnType("text");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Expertise).HasColumnType("text");
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.EmailNavigation).WithOne(p => p.PersonalTrainer)
                .HasForeignKey<PersonalTrainer>(d => d.Email)
                .HasConstraintName("FK__PersonalT__Email__45F365D3");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A17915C74");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EA586B3A15");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("ServiceID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TrainingPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Training__322035EC42F3EA75");

            entity.Property(e => e.PackageId)
                .ValueGeneratedNever()
                .HasColumnName("PackageID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.PackageType)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TrainingSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Training__9C8A5B69A9EB9B7D");

            entity.ToTable("TrainingSchedule");

            entity.Property(e => e.ScheduleId)
                .ValueGeneratedNever()
                .HasColumnName("ScheduleID");
            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.PtEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PT_Email");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Booking).WithMany(p => p.TrainingSchedules)
                .HasForeignKey(d => d.BookingId)
                .HasConstraintName("FK__TrainingS__Booki__4F7CD00D");

            entity.HasOne(d => d.PtEmailNavigation).WithMany(p => p.TrainingSchedules)
                .HasForeignKey(d => d.PtEmail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingS__PT_Em__4E88ABD4");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.TrainingSchedules)
                .HasPrincipalKey(p => p.Email)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingS__UserE__4D94879B");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC6C87D98B");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053435B38B29").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PackageId).HasColumnName("PackageID");
            entity.Property(e => e.Phone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

            entity.HasOne(d => d.EmailNavigation).WithOne(p => p.User)
                .HasForeignKey<User>(d => d.Email)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__Email__403A8C7D");

            entity.HasOne(d => d.Package).WithMany(p => p.Users)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Users__PackageID__4222D4EF");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__412EB0B6");

            entity.HasOne(d => d.Service).WithMany(p => p.Users)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Users__ServiceID__4316F928");
        });

        modelBuilder.Entity<UserPtBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__User_PT___73951ACDE093C489");

            entity.ToTable("User_PT_Booking");

            entity.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("BookingID");
            entity.Property(e => e.PtEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PT_Email");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PtEmailNavigation).WithMany(p => p.UserPtBookings)
                .HasForeignKey(d => d.PtEmail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__User_PT_B__PT_Em__4AB81AF0");

            entity.HasOne(d => d.User).WithMany(p => p.UserPtBookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__User_PT_B__UserI__49C3F6B7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
