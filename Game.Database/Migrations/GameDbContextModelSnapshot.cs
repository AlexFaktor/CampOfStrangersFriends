﻿// <auto-generated />
using System;
using Game.Database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Game.Database.Migrations
{
    [DbContext(typeof(GameDbContext))]
    partial class GameDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Game.Core.Database.Records.Camp.CampBuilding", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserCampUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId");

                    b.HasIndex("UserCampUserId");

                    b.ToTable("CampBuildings");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserCamp", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("UserCamps");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserCharacter", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CharacterId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "CharacterId");

                    b.ToTable("UserCharacters");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserItem", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ItemId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "ItemId");

                    b.ToTable("UserItems");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Hashtag")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserResources", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("Energy")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Fackoins")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("RandomCoin")
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("SoulValue")
                        .HasColumnType("bigint");

                    b.HasKey("UserId");

                    b.ToTable("UserResources");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserStatistics", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateOfUserRegistration")
                        .HasColumnType("datetime2");

                    b.Property<long>("NumberInteractionsWithBot")
                        .HasColumnType("bigint");

                    b.HasKey("UserId");

                    b.ToTable("UserStatistics");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserTelegram", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Language")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelegramId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Username")
                        .HasMaxLength(32)
                        .HasColumnType("nvarchar(32)");

                    b.HasKey("UserId");

                    b.ToTable("UserTelegrams");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Camp.CampBuilding", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserCamp", null)
                        .WithMany("Buildings")
                        .HasForeignKey("UserCampUserId");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserCamp", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithOne("Camp")
                        .HasForeignKey("Game.Core.Database.Records.Users.UserCamp", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserCharacter", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserItem", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithMany("Items")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserResources", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithOne("Resources")
                        .HasForeignKey("Game.Core.Database.Records.Users.UserResources", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserStatistics", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithOne("Statistics")
                        .HasForeignKey("Game.Core.Database.Records.Users.UserStatistics", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserTelegram", b =>
                {
                    b.HasOne("Game.Core.Database.Records.Users.UserRecord", null)
                        .WithOne("Telegram")
                        .HasForeignKey("Game.Core.Database.Records.Users.UserTelegram", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserCamp", b =>
                {
                    b.Navigation("Buildings");
                });

            modelBuilder.Entity("Game.Core.Database.Records.Users.UserRecord", b =>
                {
                    b.Navigation("Camp")
                        .IsRequired();

                    b.Navigation("Characters");

                    b.Navigation("Items");

                    b.Navigation("Resources")
                        .IsRequired();

                    b.Navigation("Statistics")
                        .IsRequired();

                    b.Navigation("Telegram")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
