﻿// <auto-generated />
using RPG.Api.Persistence;
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
    [Migration("20190801170236_AddNotificationSeen")]
    partial class AddNotificationSeen
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

                    b.Property<bool>("isAccepted");

                    b.Property<bool>("isFriendRequest");

                    b.Property<DateTime?>("lastMessageDate");

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

                    b.Property<string>("book");

                    b.Property<string>("category");

                    b.Property<string>("comment");

                    b.Property<DateTime>("date");

                    b.Property<string>("description");

                    b.Property<bool>("isActive");

                    b.Property<string>("location");

                    b.Property<int>("masterId");

                    b.Property<bool>("needInvite");

                    b.Property<int>("nofparticipants");

                    b.Property<int>("nofplayers");

                    b.Property<string>("title");

                    b.HasKey("Id");

                    b.HasIndex("masterId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("mdRPG.Models.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<int>("gameId");

                    b.Property<string>("sessionName");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("mdRPG.Models.GameToPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("characterHealth");

                    b.Property<int>("gameId");

                    b.Property<bool>("isAccepted");

                    b.Property<bool>("isGameMaster");

                    b.Property<bool>("isMadeByPlayer");

                    b.Property<int>("playerId");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.HasIndex("playerId");

                    b.ToTable("GamesToPerson");
                });

            modelBuilder.Entity("mdRPG.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("bodyMessage");

                    b.Property<int>("relationId");

                    b.Property<DateTime>("sendDdate");

                    b.Property<int>("senderId");

                    b.Property<bool>("wasSeen");

                    b.HasKey("Id");

                    b.HasIndex("relationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("mdRPG.Models.MyItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("cardId");

                    b.Property<string>("itemName");

                    b.Property<int?>("itemValue");

                    b.HasKey("Id");

                    b.HasIndex("cardId");

                    b.ToTable("MyItems");
                });

            modelBuilder.Entity("mdRPG.Models.MySkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("cardId");

                    b.Property<string>("skillName");

                    b.Property<int>("skillValue");

                    b.HasKey("Id");

                    b.HasIndex("cardId");

                    b.ToTable("MySkills");
                });

            modelBuilder.Entity("mdRPG.Models.NotificationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("lastFriendNotificationDate");

                    b.Property<DateTime?>("lastFriendNotificationSeen");

                    b.Property<DateTime?>("lastGameNotificationDate");

                    b.Property<DateTime?>("lastGameNotificationSeen");

                    b.Property<DateTime?>("lastMessageDate");

                    b.Property<DateTime?>("lastMessageSeen");

                    b.HasKey("Id");

                    b.ToTable("NotificationsData");
                });

            modelBuilder.Entity("mdRPG.Models.PersonalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("NotificationDataId");

                    b.Property<int?>("ProfilePhotoId");

                    b.Property<int>("age");

                    b.Property<string>("city");

                    b.Property<string>("email");

                    b.Property<string>("firstname");

                    b.Property<string>("lastname");

                    b.Property<string>("login");

                    b.Property<string>("photoName");

                    b.HasKey("Id");

                    b.HasIndex("NotificationDataId");

                    b.HasIndex("ProfilePhotoId");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("mdRPG.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("mdRPG.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("gameId");

                    b.Property<string>("skillName");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.ToTable("Skills");
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

            modelBuilder.Entity("mdRPG.Models.GameSession", b =>
                {
                    b.HasOne("mdRPG.Models.Game", "game")
                        .WithMany("sessions")
                        .HasForeignKey("gameId")
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

            modelBuilder.Entity("mdRPG.Models.Message", b =>
                {
                    b.HasOne("mdRPG.Models.Friend", "relation")
                        .WithMany("Messages")
                        .HasForeignKey("relationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mdRPG.Models.MyItem", b =>
                {
                    b.HasOne("mdRPG.Models.GameToPerson", "card")
                        .WithMany("characterItems")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mdRPG.Models.MySkill", b =>
                {
                    b.HasOne("mdRPG.Models.GameToPerson", "card")
                        .WithMany("characterSkills")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("mdRPG.Models.PersonalData", b =>
                {
                    b.HasOne("mdRPG.Models.NotificationData", "NotificationData")
                        .WithMany()
                        .HasForeignKey("NotificationDataId");

                    b.HasOne("mdRPG.Models.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");
                });

            modelBuilder.Entity("mdRPG.Models.Skill", b =>
                {
                    b.HasOne("mdRPG.Models.Game", "game")
                        .WithMany("skillSetting")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
