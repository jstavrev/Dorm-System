﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartDormitory.Data.Data;

namespace SmartDormitory.Data.Migrations
{
    [DbContext(typeof(SmartDormitoryDbContext))]
    partial class SmartDormitoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new { Id = "1", ConcurrencyStamp = "aaa", Name = "Admin", NormalizedName = "ADMIN" },
                        new { Id = "2", ConcurrencyStamp = "bbb", Name = "User", NormalizedName = "USER" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new { UserId = "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", RoleId = "1" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SmartDormitory.Data.Data.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<byte[]>("AvatarImage");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Country");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PostalCode");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Story");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasData(
                        new { Id = "d55e34c1-7550-47a4-9a0d-79e17f4cd3eb", AccessFailedCount = 0, ConcurrencyStamp = "5bec9d43-f97d-47f0-94e4-87e6f352cc91", Email = "ICBAdmin@mail.com", EmailConfirmed = true, LockoutEnabled = false, NormalizedEmail = "ICBADMIN@MAIL.COM", NormalizedUserName = "ICBADMIN", PasswordHash = "AQAAAAEAACcQAAAAENtvgf4DK+dETKWisJpZ79G/B2qGcpDn0E8bw/aBZpAlKKkobmN7fN1r7vmlE2usgA==", PhoneNumber = "+55555", PhoneNumberConfirmed = true, PostalCode = 0, SecurityStamp = "c4de9f14-695d-4eba-bed7-7f05f8c84388", TwoFactorEnabled = false, UserName = "ICBAdmin" }
                    );
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.Sensor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiId");

                    b.Property<string>("CurrentValue");

                    b.Property<string>("Description");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<int>("MinPollingIntervalInSeconds");

                    b.Property<string>("Name");

                    b.Property<int>("SensorTypeId");

                    b.HasKey("Id");

                    b.HasIndex("SensorTypeId");

                    b.ToTable("Sensors");
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.SensorDataHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AddedOn");

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<int>("SensorId");

                    b.Property<string>("UserId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.HasIndex("UserId");

                    b.ToTable("SensorsDataHistory");
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.SensorTypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("CreatedOn");

                    b.Property<DateTime?>("DeletedOn");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifiedOn");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.ToTable("SensorTypes");
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.UserSensors", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<bool>("IsPublic");

                    b.Property<bool>("IsRequiredNotification");

                    b.Property<DateTime>("LastUpdatedOn");

                    b.Property<string>("Latitude");

                    b.Property<string>("Longitude");

                    b.Property<int>("MaxValue");

                    b.Property<int>("MinValue");

                    b.Property<string>("Name");

                    b.Property<int>("SensorId");

                    b.Property<int>("UpdateInterval");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SensorId");

                    b.HasIndex("UserId");

                    b.ToTable("UserSensors");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SmartDormitory.Data.Data.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SmartDormitory.Data.Data.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartDormitory.Data.Data.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SmartDormitory.Data.Data.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.Sensor", b =>
                {
                    b.HasOne("SmartDormitory.Models.DbModels.SensorTypes", "SensorType")
                        .WithMany("Sensors")
                        .HasForeignKey("SensorTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.SensorDataHistory", b =>
                {
                    b.HasOne("SmartDormitory.Models.DbModels.Sensor", "Sensor")
                        .WithMany("DataHistory")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartDormitory.Data.Data.User")
                        .WithMany("DataHistory")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SmartDormitory.Models.DbModels.UserSensors", b =>
                {
                    b.HasOne("SmartDormitory.Models.DbModels.Sensor", "Sensor")
                        .WithMany("UserSensors")
                        .HasForeignKey("SensorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartDormitory.Data.Data.User", "User")
                        .WithMany("UserSensor")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
