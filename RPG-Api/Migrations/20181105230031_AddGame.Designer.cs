﻿// <auto-generated />
using mdRPG.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace RPGApi.Migrations
{
    [DbContext(typeof(RpgDbContext))]
    [Migration("20181105230031_AddGame")]
    partial class AddGame
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("mdRPG.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("PersonalDataId");

                    b.Property<string>("email");

                    b.Property<string>("login");

                    b.Property<string>("password");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("mdRPG.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("player1Id");

                    b.Property<int>("player2Id");

                    b.HasKey("Id");

                    b.HasIndex("player1Id");

                    b.HasIndex("player2Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("mdRPG.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("category");

                    b.Property<DateTime>("date");

                    b.Property<string>("description");

                    b.Property<string>("location");

                    b.Property<int>("masterId");

                    b.Property<string>("title");

                    b.HasKey("Id");

                    b.HasIndex("masterId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("mdRPG.Models.GameToPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("gameId");

                    b.Property<int>("playerId");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.HasIndex("playerId");

                    b.ToTable("GamesToPerson");
                });

            modelBuilder.Entity("mdRPG.Models.PersonalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("firstname");

                    b.Property<string>("lastname");

                    b.Property<string>("login");

                    b.HasKey("Id");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("mdRPG.Models.Account", b =>
                {
                    b.HasOne("mdRPG.Models.PersonalData", "PersonalData")
                        .WithMany()
                        .HasForeignKey("PersonalDataId");
                });

            modelBuilder.Entity("mdRPG.Models.Friend", b =>
                {
                    b.HasOne("mdRPG.Models.PersonalData", "player1")
                        .WithMany("MyFriends")
                        .HasForeignKey("player1Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("mdRPG.Models.PersonalData", "player2")
                        .WithMany("IamFriends")
                        .HasForeignKey("player2Id")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mdRPG.Models.Game", b =>
                {
                    b.HasOne("mdRPG.Models.PersonalData", "gameMaster")
                        .WithMany("MyGamesMaster")
                        .HasForeignKey("masterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mdRPG.Models.GameToPerson", b =>
                {
                    b.HasOne("mdRPG.Models.Game", "game")
                        .WithMany("participants")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("mdRPG.Models.PersonalData", "player")
                        .WithMany("MyGames")
                        .HasForeignKey("playerId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
