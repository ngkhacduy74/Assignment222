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

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<RoomBooking> RoomBookings { get; set; }

    public virtual DbSet<RoomSchedule> RoomSchedules { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<TimeSlot> TimeSlots { get; set; }

    public virtual DbSet<TrainerAvailability> TrainerAvailabilities { get; set; }

    public virtual DbSet<TrainingPackage> TrainingPackages { get; set; }

    public virtual DbSet<TrainingSchedule> TrainingSchedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPtBooking> UserPtBookings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =DESKTOP-C0BL0GV; database = PrivateGymDB; uid=sa;pwd=123;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Account__A9D105359FE78615");

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
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF62F35E94A");

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
            entity.HasKey(e => e.InvoiceId).HasName("PK__Invoices__D796AAD521C92A65");

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
                .HasConstraintName("FK__Invoices__Discou__412EB0B6");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Invoices__UserID__4222D4EF");
        });

        modelBuilder.Entity<PersonalTrainer>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__Personal__A9D10535CF3F6682");

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
                .HasConstraintName("FK__PersonalT__Email__32E0915F");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1AD7F59A54");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK__Room__32863919B1B5455A");

            entity.ToTable("Room");

            entity.Property(e => e.RoomId)
                .ValueGeneratedNever()
                .HasColumnName("RoomID");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoomName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoomBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Room_Boo__73951ACDFB5E284F");

            entity.ToTable("Room_Booking");

            entity.Property(e => e.BookingId)
                .ValueGeneratedNever()
                .HasColumnName("BookingID");
            entity.Property(e => e.PtEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PT_Email");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PtEmailNavigation).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.PtEmail)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Room_Book__PT_Em__619B8048");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Room_Book__RoomI__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.RoomBookings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Room_Book__UserI__60A75C0F");
        });

        modelBuilder.Entity<RoomSchedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Room_Sch__9C8A5B69883F341E");

            entity.ToTable("Room_Schedule");

            entity.Property(e => e.ScheduleId)
                .ValueGeneratedNever()
                .HasColumnName("ScheduleID");
            entity.Property(e => e.IsBooked).HasDefaultValue(false);
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
            entity.Property(e => e.SlotId).HasColumnName("SlotID");

            entity.HasOne(d => d.Room).WithMany(p => p.RoomSchedules)
                .HasForeignKey(d => d.RoomId)
                .HasConstraintName("FK__Room_Sche__RoomI__68487DD7");

            entity.HasOne(d => d.Slot).WithMany(p => p.RoomSchedules)
                .HasForeignKey(d => d.SlotId)
                .HasConstraintName("FK__Room_Sche__SlotI__693CA210");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__Services__C51BB0EAA23CD586");

            entity.Property(e => e.ServiceId)
                .ValueGeneratedNever()
                .HasColumnName("ServiceID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TimeSlot>(entity =>
        {
            entity.HasKey(e => e.SlotId).HasName("PK__Time_Slo__0A124A4F3EB669BA");

            entity.ToTable("Time_Slots");

            entity.Property(e => e.SlotId)
                .ValueGeneratedNever()
                .HasColumnName("SlotID");
        });

        modelBuilder.Entity<TrainerAvailability>(entity =>
        {
            entity.HasKey(e => e.AvailabilityId).HasName("PK__Trainer___DA397991C95FC279");

            entity.ToTable("Trainer_Availability");

            entity.Property(e => e.AvailabilityId)
                .ValueGeneratedNever()
                .HasColumnName("AvailabilityID");
            entity.Property(e => e.IsAvailable).HasDefaultValue(true);
            entity.Property(e => e.PT_Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PT_Email");
            entity.Property(e => e.SlotId).HasColumnName("SlotID");

            entity.HasOne(d => d.PtEmailNavigation).WithMany(p => p.TrainerAvailabilities)
                .HasForeignKey(d => d.PT_Email)
                .HasConstraintName("FK__Trainer_A__PT_Em__6D0D32F4");

            entity.HasOne(d => d.Slot).WithMany(p => p.TrainerAvailabilities)
                .HasForeignKey(d => d.SlotId)
                .HasConstraintName("FK__Trainer_A__SlotI__6E01572D");
        });

        modelBuilder.Entity<TrainingPackage>(entity =>
        {
            entity.HasKey(e => e.PackageId).HasName("PK__Training__322035EC4C199C01");

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
            entity.HasKey(e => e.ScheduleId).HasName("PK__Training__9C8A5B6957D75845");

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
                .HasConstraintName("FK__TrainingS__Booki__3C69FB99");

            entity.HasOne(d => d.PtEmailNavigation).WithMany(p => p.TrainingSchedules)
                .HasForeignKey(d => d.PtEmail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingS__PT_Em__3B75D760");

            entity.HasOne(d => d.UserEmailNavigation).WithMany(p => p.TrainingSchedules)
                .HasPrincipalKey(p => p.Email)
                .HasForeignKey(d => d.UserEmail)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TrainingS__UserE__3A81B327");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC728193CD");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534C080B7F1").IsUnique();

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
            entity.Property(e => e.Img)
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
                .HasConstraintName("FK__Users__Email__2D27B809");

            entity.HasOne(d => d.Package).WithMany(p => p.Users)
                .HasForeignKey(d => d.PackageId)
                .HasConstraintName("FK__Users__PackageID__2F10007B");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__Users__RoleId__2E1BDC42");

            entity.HasOne(d => d.Service).WithMany(p => p.Users)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK__Users__ServiceID__300424B4");
        });

        modelBuilder.Entity<UserPtBooking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__User_PT___73951ACD1748FA9C");

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
                .HasConstraintName("FK__User_PT_B__PT_Em__37A5467C");

            entity.HasOne(d => d.User).WithMany(p => p.UserPtBookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__User_PT_B__UserI__36B12243");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
