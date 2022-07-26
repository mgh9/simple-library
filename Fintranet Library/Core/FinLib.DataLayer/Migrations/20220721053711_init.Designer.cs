﻿// <auto-generated />
using System;
using FinLib.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinLib.DataLayer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220721053711_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FinLib.DomainClasses.CNT.MenuLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("OrderNumber")
                        .HasColumnType("int");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("ParentId");

                    b.HasIndex("Title");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("MenuLinks", "CNT");
                });

            modelBuilder.Entity("FinLib.DomainClasses.CNT.MenuLinkOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<int>("MenuLinkId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("MenuLinkId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("MenuLinkOwners", "CNT");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("Title");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.BookBorrowing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowingDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerUserRoleId")
                        .HasColumnType("int");

                    b.Property<int>("LibrarianUserRoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReturningDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("CustomerUserRoleId");

                    b.HasIndex("LibrarianUserRoleId");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("BookBorrowings");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("Title");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Roles", "SEC");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastLoggedInTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LockoutDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.ToTable("Users", "SEC");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Entity PrimaryKey");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int?>("CreatedByUserRoleId")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDefault")
                        .HasColumnType("bit");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2")
                        .HasComment("زمان ویرایش");

                    b.Property<int?>("UpdatedByUserRoleId")
                        .HasColumnType("int")
                        .HasComment("شناسه ی کاربر ویرایش کننده");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UpdatedByUserRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles", "SEC");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "SEC");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "SEC");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "SEC");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "SEC");
                });

            modelBuilder.Entity("FinLib.DomainClasses.CNT.MenuLink", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.CNT.MenuLink", "ParentMenuLink")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("ParentMenuLink");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.CNT.MenuLinkOwner", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.CNT.MenuLink", "MenuLink")
                        .WithMany()
                        .HasForeignKey("MenuLinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("MenuLink");

                    b.Navigation("Role");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.Book", b =>
                {
                    b.HasOne("FinLib.DomainClasses.DBO.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("Category");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.BookBorrowing", b =>
                {
                    b.HasOne("FinLib.DomainClasses.DBO.Book", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CustomerUserRole")
                        .WithMany()
                        .HasForeignKey("CustomerUserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "LibrarianUserRole")
                        .WithMany()
                        .HasForeignKey("LibrarianUserRoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("Book");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("CustomerUserRole");

                    b.Navigation("LibrarianUserRole");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.DBO.Category", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.Role", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.Navigation("CreateByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.User", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.Navigation("CreateByUserRole");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("FinLib.DomainClasses.SEC.UserRole", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "CreateByUserRole")
                        .WithMany()
                        .HasForeignKey("CreatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FinLib.DomainClasses.SEC.UserRole", "UpdatedByUserRole")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserRoleId");

                    b.HasOne("FinLib.DomainClasses.SEC.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreateByUserRole");

                    b.Navigation("UpdatedByUserRole");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("FinLib.DomainClasses.SEC.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
