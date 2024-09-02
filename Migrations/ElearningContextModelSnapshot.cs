﻿// <auto-generated />
using System;
using ElearningMVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ElearningMVC.Migrations
{
    [DbContext(typeof(ElearningContext))]
    partial class ElearningContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ElearningMVC.Models.Cart", b =>
                {
                    b.Property<int>("Pid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("pid");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Pid"));

                    b.Property<string>("Course")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("course");

                    b.Property<string>("Dt")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("dt");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("price");

                    b.Property<string>("Subcourse")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("subcourse");

                    b.Property<string>("Suser")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("suser");

                    b.HasKey("Pid")
                        .HasName("PK__cart__DD37D91AFE71136A");

                    b.ToTable("cart", (string)null);
                });

            modelBuilder.Entity("ElearningMVC.Models.Certificate", b =>
                {
                    b.Property<int>("CertificateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CertificateId"));

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("IssuseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("suser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CertificateId");

                    b.ToTable("Certificates");
                });

            modelBuilder.Entity("ElearningMVC.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Banner")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("banner");

                    b.Property<string>("Cname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("cname");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("price");

                    b.Property<string>("Subname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("subname");

                    b.HasKey("Id")
                        .HasName("PK__course__3213E83FAFAA89DC");

                    b.ToTable("course", (string)null);
                });

            modelBuilder.Entity("ElearningMVC.Models.Mcqs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Correct")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("option1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("option2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("option3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("option4")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("question")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("vid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Mcqss");
                });

            modelBuilder.Entity("ElearningMVC.Models.PaymentPlace", b =>
                {
                    b.Property<int>("Pid")
                        .HasColumnType("int")
                        .HasColumnName("pid");

                    b.Property<string>("Banner")
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("banner");

                    b.Property<string>("Course")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("course");

                    b.Property<string>("Dt")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("dt");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(9, 2)")
                        .HasColumnName("price");

                    b.Property<string>("Subcourse")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("subcourse");

                    b.Property<string>("Suser")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("suser");

                    b.HasKey("Pid")
                        .HasName("PK_PaymentPlace");

                    b.ToTable("PaymentPlace", (string)null);
                });

            modelBuilder.Entity("ElearningMVC.Models.TaskAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Finishdate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Score")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoId")
                        .HasColumnType("int");

                    b.Property<string>("taskUpload")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaskAssignments");
                });

            modelBuilder.Entity("ElearningMVC.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<string>("UserEmail")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_email");

                    b.Property<string>("UserFname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_Fname");

                    b.Property<string>("UserLname")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_Lname");

                    b.Property<long?>("UserMobile")
                        .HasColumnType("bigint")
                        .HasColumnName("user_mobile");

                    b.Property<string>("UserPass")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_pass");

                    b.Property<string>("UserProfile")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_profile");

                    b.Property<string>("UserRole")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("user_role");

                    b.Property<int?>("Videocount")
                        .HasColumnType("int")
                        .HasColumnName("videocount");

                    b.HasKey("Id")
                        .HasName("PK__User_acc__3214EC07D092276C");

                    b.ToTable("User_account", (string)null);
                });

            modelBuilder.Entity("ElearningMVC.Models.UserVideoProgress", b =>
                {
                    b.Property<int>("ProgressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProgressId"));

                    b.Property<bool>("IsWatched")
                        .HasColumnType("bit");

                    b.Property<string>("Suser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("WatcheDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("videoId")
                        .HasColumnType("int");

                    b.HasKey("ProgressId");

                    b.ToTable("UserVideoProgresses");
                });

            modelBuilder.Entity("ElearningMVC.Models.Video", b =>
                {
                    b.Property<int>("Vid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("vid");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Vid"));

                    b.Property<string>("Course")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("course");

                    b.Property<string>("Subcourse")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("subcourse");

                    b.Property<string>("Topic")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("topic");

                    b.Property<string>("Url")
                        .HasMaxLength(300)
                        .IsUnicode(false)
                        .HasColumnType("varchar(300)")
                        .HasColumnName("url");

                    b.HasKey("Vid")
                        .HasName("PK__videos__DDB00C7D7E9C5B78");

                    b.ToTable("videos", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
