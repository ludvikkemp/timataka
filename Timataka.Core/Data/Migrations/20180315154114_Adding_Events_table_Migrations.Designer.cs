﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using Timataka.Core.Data;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180315154114_Adding_Events_table_Migrations")]
    partial class Adding_Events_table_Migrations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
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
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

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
                        .ValueGeneratedOnAdd();

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

            modelBuilder.Entity("Timataka.Core.Models.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int?>("CountryId");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("Gender");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MiddleName");

                    b.Property<int?>("NationalityId");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Ssn");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("NationalityId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Club", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("NameAbbreviation");

                    b.Property<string>("Phone");

                    b.Property<string>("Webpage");

                    b.HasKey("Id");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Competition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<string>("Description");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<string>("Sponsor");

                    b.Property<string>("WebPage");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.CompetitionInstance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompetitionId");

                    b.Property<string>("CompetitionName");

                    b.Property<int?>("CountryId");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<bool>("Deleted");

                    b.Property<string>("Location");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("CountryId");

                    b.ToTable("CompetitionInstances");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Alpha2");

                    b.Property<string>("Alpha3");

                    b.Property<string>("Name");

                    b.Property<string>("Nationality");

                    b.Property<string>("PhoneExtension");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Discipline", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SportId");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("ActiveChip");

                    b.Property<int>("CompetitionInstanceId");

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("DateFrom");

                    b.Property<DateTime>("DateTo");

                    b.Property<int>("DisciplineId");

                    b.Property<int>("DistanceOffset");

                    b.Property<int>("Gender");

                    b.Property<int>("Laps");

                    b.Property<string>("Name");

                    b.Property<int>("Splits");

                    b.Property<int>("StartInterval");

                    b.HasKey("Id");

                    b.HasIndex("CompetitionInstanceId");

                    b.HasIndex("DisciplineId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.ManagesCompetition", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("CompetitionId");

                    b.Property<int>("Role");

                    b.HasKey("UserId", "CompetitionId");

                    b.HasIndex("CompetitionId");

                    b.ToTable("ManagesCompetitions");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Sport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.UserInClub", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<int>("SportId");

                    b.Property<int>("ClubId");

                    b.Property<int>("Role");

                    b.HasKey("UserId", "SportId");

                    b.HasAlternateKey("SportId", "UserId");

                    b.HasIndex("ClubId");

                    b.ToTable("UsersInClubs");
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
                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser")
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

                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.Country", "RepresentingCountry")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("Timataka.Core.Models.Entities.Country", "Nation")
                        .WithMany()
                        .HasForeignKey("NationalityId");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.CompetitionInstance", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.Discipline", "_CompetitonId")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timataka.Core.Models.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Discipline", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.Sport", "ApplicationSportId")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.Event", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.CompetitionInstance", "CompInstanceId")
                        .WithMany()
                        .HasForeignKey("CompetitionInstanceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timataka.Core.Models.Entities.Discipline", "_DisciplineId")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.ManagesCompetition", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.Competition", "ApplicationCompetitonId")
                        .WithMany()
                        .HasForeignKey("CompetitionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser", "ApplicationUserId")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Timataka.Core.Models.Entities.UserInClub", b =>
                {
                    b.HasOne("Timataka.Core.Models.Entities.Sport", "ApplicationClubId")
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timataka.Core.Models.Entities.Sport", "ApplicationSportId")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Timataka.Core.Models.Entities.ApplicationUser", "ApplicationUserId")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
