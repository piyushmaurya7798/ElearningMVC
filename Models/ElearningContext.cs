using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ElearningMVC.Models;

public partial class ElearningContext : DbContext
{
    public ElearningContext()
    {
    }

    public ElearningContext(DbContextOptions<ElearningContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<PaymentPlace> PaymentPlaces { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<Video> Videos { get; set; }
    public virtual DbSet<Mcqs> Mcqss { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-2K57GBFM\\MSSQLSERVER01;Database=Elearning;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Pid).HasName("PK__cart__DD37D91AFE71136A");

            entity.ToTable("cart");

            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Course)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course");
            entity.Property(e => e.Dt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dt");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Subcourse)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subcourse");
            entity.Property(e => e.Suser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("suser");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__course__3213E83FAFAA89DC");

            entity.ToTable("course");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Banner).HasColumnName("banner");
            entity.Property(e => e.Cname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cname");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Subname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subname");
        });

        modelBuilder.Entity<PaymentPlace>(entity =>
        {
            entity.HasKey(e => e.Pid);

            entity.ToTable("PaymentPlace");

            entity.Property(e => e.Pid)
                .ValueGeneratedNever()
                .HasColumnName("pid");
            entity.Property(e => e.Banner)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("banner");
            entity.Property(e => e.Course)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course");
            entity.Property(e => e.Dt)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("dt");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("price");
            entity.Property(e => e.Subcourse)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subcourse");
            entity.Property(e => e.Suser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("suser");
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User_acc__3214EC07D092276C");

            entity.ToTable("User_account");

            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_email");
            entity.Property(e => e.UserFname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_Fname");
            entity.Property(e => e.UserLname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_Lname");
            entity.Property(e => e.UserMobile).HasColumnName("user_mobile");
            entity.Property(e => e.UserPass)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_pass");
            entity.Property(e => e.UserProfile)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_profile");
            entity.Property(e => e.UserRole)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("user_role");
            entity.Property(e => e.Videocount).HasColumnName("videocount");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.Vid).HasName("PK__videos__DDB00C7D7E9C5B78");

            entity.ToTable("videos");

            entity.Property(e => e.Vid).HasColumnName("vid");
            entity.Property(e => e.Course)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("course");
            entity.Property(e => e.Subcourse)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subcourse");
            entity.Property(e => e.Topic)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("topic");
            entity.Property(e => e.Url)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("url");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
