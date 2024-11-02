﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NuGet.Next.Sqlite;

#nullable disable

namespace BaGet.Database.Sqlite.Migrations
{
    [DbContext(typeof(SqliteContext))]
    partial class SqliteContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("NuGet.Next.Core.Package", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Authors")
                        .IsRequired()
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Creator")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<long>("Downloads")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasEmbeddedIcon")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasReadme")
                        .HasColumnType("INTEGER");

                    b.Property<string>("IconUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<bool>("IsPrerelease")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Language")
                        .HasMaxLength(20)
                        .HasColumnType("TEXT");

                    b.Property<string>("LicenseUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<bool>("Listed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MinClientVersion")
                        .HasMaxLength(44)
                        .HasColumnType("TEXT");

                    b.Property<string>("Modifier")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedVersionString")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("TEXT COLLATE NOCASE")
                        .HasColumnName("Version");

                    b.Property<string>("OriginalVersionString")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT")
                        .HasColumnName("OriginalVersion");

                    b.Property<string>("ProjectUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Published")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReleaseNotes")
                        .HasColumnType("TEXT")
                        .HasColumnName("ReleaseNotes");

                    b.Property<string>("RepositoryType")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("RepositoryUrl")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<bool>("RequireLicenseAcceptance")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("BLOB");

                    b.Property<int>("SemVerLevel")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Summary")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Tags")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.HasIndex("Id");

                    b.HasIndex("Id", "NormalizedVersionString")
                        .IsUnique();

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("NuGet.Next.Core.PackageDependency", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Id")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<string>("PackageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PackageKey")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TargetFramework")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("VersionRange")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.HasIndex("Id");

                    b.HasIndex("PackageKey");

                    b.ToTable("PackageDependencies");
                });

            modelBuilder.Entity("NuGet.Next.Core.PackageType", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(512)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<int>("PackageKey")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Version")
                        .HasMaxLength(64)
                        .HasColumnType("TEXT");

                    b.HasKey("Key");

                    b.HasIndex("Name");

                    b.HasIndex("PackageKey");

                    b.ToTable("PackageTypes");
                });

            modelBuilder.Entity("NuGet.Next.Core.PackageUpdateRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("OperationDescription")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OperationIP")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("OperationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("OperationType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PackageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Version")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.HasIndex("UserId");

                    b.ToTable("PackageUpdateRecords");
                });

            modelBuilder.Entity("NuGet.Next.Core.TargetFramework", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Moniker")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT COLLATE NOCASE");

                    b.Property<string>("PackageId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PackageKey")
                        .HasColumnType("INTEGER");

                    b.HasKey("Key");

                    b.HasIndex("Moniker");

                    b.HasIndex("PackageKey");

                    b.ToTable("TargetFrameworks");
                });

            modelBuilder.Entity("NuGet.Next.Core.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Avatar")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "16e374d6-13a4-476a-8c8f-3432a378e43b",
                            Avatar = "https://avatars.githubusercontent.com/u/61819790?v=4",
                            Email = "239573049@qq.com",
                            FullName = "token",
                            Password = "38681107d166e47ed9aaab87c86a21a1",
                            PasswordHash = "632472d34b5e45b99dfc4bbc8286fcc2",
                            Role = "admin",
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("NuGet.Next.Core.UserKey", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Enabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.HasIndex("UserId");

                    b.ToTable("UserKeys");

                    b.HasData(
                        new
                        {
                            Id = "4aeb5cef239e4d449391b64e97f44610",
                            CreatedTime = new DateTimeOffset(new DateTime(2024, 11, 2, 22, 43, 23, 699, DateTimeKind.Unspecified).AddTicks(2195), new TimeSpan(0, 8, 0, 0, 0)),
                            Enabled = true,
                            Key = "key-37971b1b9fe44e7bb4d1da2836cab187",
                            UserId = "16e374d6-13a4-476a-8c8f-3432a378e43b"
                        });
                });

            modelBuilder.Entity("NuGet.Next.Core.PackageDependency", b =>
                {
                    b.HasOne("NuGet.Next.Core.Package", "Package")
                        .WithMany("Dependencies")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("NuGet.Next.Core.PackageType", b =>
                {
                    b.HasOne("NuGet.Next.Core.Package", "Package")
                        .WithMany("PackageTypes")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("NuGet.Next.Core.TargetFramework", b =>
                {
                    b.HasOne("NuGet.Next.Core.Package", "Package")
                        .WithMany("TargetFrameworks")
                        .HasForeignKey("PackageKey")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Package");
                });

            modelBuilder.Entity("NuGet.Next.Core.UserKey", b =>
                {
                    b.HasOne("NuGet.Next.Core.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("NuGet.Next.Core.Package", b =>
                {
                    b.Navigation("Dependencies");

                    b.Navigation("PackageTypes");

                    b.Navigation("TargetFrameworks");
                });
#pragma warning restore 612, 618
        }
    }
}
