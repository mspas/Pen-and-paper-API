﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RPG.Api.Persistence;

namespace RPGApi.Migrations
{
    [DbContext(typeof(RpgDbContext))]
    [Migration("20210929154947_ForumBugfix")]
    partial class ForumBugfix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RPG.Api.Domain.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("PersonalDataId")
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PersonalDataId");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Forum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isPublic")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastActivityDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Forums");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Friend", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("isFriendRequest")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastMessageDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("player1Id")
                        .HasColumnType("int");

                    b.Property<int>("player2Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("player1Id");

                    b.HasIndex("player2Id");

                    b.ToTable("Friends");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BackgroundPhotoId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfilePhotoId")
                        .HasColumnType("int");

                    b.Property<string>("bgPhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("forumId")
                        .HasColumnType("int");

                    b.Property<bool>("hotJoin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastActivityDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("masterId")
                        .HasColumnType("int");

                    b.Property<int>("maxplayers")
                        .HasColumnType("int");

                    b.Property<bool>("needInvite")
                        .HasColumnType("bit");

                    b.Property<string>("photoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("storyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundPhotoId");

                    b.HasIndex("ProfilePhotoId");

                    b.HasIndex("forumId");

                    b.HasIndex("masterId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("date")
                        .HasColumnType("datetime2");

                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<string>("sessionName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.GameToPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("characterHealth")
                        .HasColumnType("int");

                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<bool>("isAccepted")
                        .HasColumnType("bit");

                    b.Property<bool>("isGameMaster")
                        .HasColumnType("bit");

                    b.Property<bool>("isMadeByPlayer")
                        .HasColumnType("bit");

                    b.Property<int>("playerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.HasIndex("playerId");

                    b.ToTable("GamesToPerson");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bodyMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPhoto")
                        .HasColumnType("bit");

                    b.Property<int?>("photoId")
                        .HasColumnType("int");

                    b.Property<int>("relationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("sendDdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("senderId")
                        .HasColumnType("int");

                    b.Property<bool>("wasSeen")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("photoId");

                    b.HasIndex("relationId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MessageForum", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("bodyMessage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("editDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isPhoto")
                        .HasColumnType("bit");

                    b.Property<DateTime>("sendDdate")
                        .HasColumnType("datetime2");

                    b.Property<int>("senderId")
                        .HasColumnType("int");

                    b.Property<int>("topicId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("topicId");

                    b.ToTable("MessagesForum");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MyItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("cardId")
                        .HasColumnType("int");

                    b.Property<string>("itemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("itemValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("cardId");

                    b.ToTable("MyItems");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MySkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("cardId")
                        .HasColumnType("int");

                    b.Property<string>("skillName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("skillValue")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("cardId");

                    b.ToTable("MySkills");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.NotificationData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("lastFriendNotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lastFriendNotificationSeen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lastGameNotificationDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lastGameNotificationSeen")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lastMessageDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("lastMessageSeen")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("NotificationsData");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.PersonalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BackgroundPhotoId")
                        .HasColumnType("int");

                    b.Property<int?>("NotificationDataId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfilePhotoId")
                        .HasColumnType("int");

                    b.Property<int>("age")
                        .HasColumnType("int");

                    b.Property<string>("bgPhotoName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("firstname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isPhotoUploaded")
                        .HasColumnType("bit");

                    b.Property<string>("lastname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("photoName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BackgroundPhotoId");

                    b.HasIndex("NotificationDataId");

                    b.HasIndex("ProfilePhotoId");

                    b.ToTable("PersonalData");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int?>("MessageForumId")
                        .HasColumnType("int");

                    b.Property<int>("sourceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MessageForumId");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("gameId")
                        .HasColumnType("int");

                    b.Property<string>("skillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("gameId");

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("authorId")
                        .HasColumnType("int");

                    b.Property<string>("category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("createDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("forumId")
                        .HasColumnType("int");

                    b.Property<bool>("isPublic")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastActivityDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("lastActivityUserId")
                        .HasColumnType("int");

                    b.Property<string>("topicName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("forumId");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.TopicToPerson", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("forumId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("lastActivitySeen")
                        .HasColumnType("datetime2");

                    b.Property<int>("topicId")
                        .HasColumnType("int");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("topicId");

                    b.HasIndex("userId");

                    b.ToTable("TopicsToPersons");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Account", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.PersonalData", "PersonalData")
                        .WithMany()
                        .HasForeignKey("PersonalDataId");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Friend", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.PersonalData", "player1")
                        .WithMany("MyFriends")
                        .HasForeignKey("player1Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RPG.Api.Domain.Models.PersonalData", "player2")
                        .WithMany("IamFriends")
                        .HasForeignKey("player2Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Game", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Photo", "BackgroundPhoto")
                        .WithMany()
                        .HasForeignKey("BackgroundPhotoId");

                    b.HasOne("RPG.Api.Domain.Models.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");

                    b.HasOne("RPG.Api.Domain.Models.Forum", "forum")
                        .WithMany()
                        .HasForeignKey("forumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPG.Api.Domain.Models.PersonalData", "gameMaster")
                        .WithMany("MyGamesMaster")
                        .HasForeignKey("masterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.GameSession", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Game", "game")
                        .WithMany("sessions")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.GameToPerson", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Game", "game")
                        .WithMany("participants")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RPG.Api.Domain.Models.PersonalData", "player")
                        .WithMany("MyGames")
                        .HasForeignKey("playerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Message", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Photo", "photo")
                        .WithMany()
                        .HasForeignKey("photoId");

                    b.HasOne("RPG.Api.Domain.Models.Friend", "relation")
                        .WithMany("Messages")
                        .HasForeignKey("relationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MessageForum", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Topic", "topic")
                        .WithMany("Messages")
                        .HasForeignKey("topicId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MyItem", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.GameToPerson", "card")
                        .WithMany("characterItems")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.MySkill", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.GameToPerson", "card")
                        .WithMany("characterSkills")
                        .HasForeignKey("cardId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.PersonalData", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Photo", "BackgroundPhoto")
                        .WithMany()
                        .HasForeignKey("BackgroundPhotoId");

                    b.HasOne("RPG.Api.Domain.Models.NotificationData", "NotificationData")
                        .WithMany()
                        .HasForeignKey("NotificationDataId");

                    b.HasOne("RPG.Api.Domain.Models.Photo", "ProfilePhoto")
                        .WithMany()
                        .HasForeignKey("ProfilePhotoId");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Photo", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.MessageForum", null)
                        .WithMany("photos")
                        .HasForeignKey("MessageForumId");
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Skill", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Game", "game")
                        .WithMany("skillSetting")
                        .HasForeignKey("gameId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.Topic", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Forum", "forum")
                        .WithMany("Topics")
                        .HasForeignKey("forumId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.TopicToPerson", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Topic", "Topic")
                        .WithMany("UsersConnected")
                        .HasForeignKey("topicId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RPG.Api.Domain.Models.NotificationData", "UserNotificationData")
                        .WithMany("topicsAccess")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RPG.Api.Domain.Models.UserRole", b =>
                {
                    b.HasOne("RPG.Api.Domain.Models.Account", "Account")
                        .WithMany("UserRoles")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RPG.Api.Domain.Models.Role", "Role")
                        .WithMany("UsersRole")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
