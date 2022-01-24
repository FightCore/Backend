﻿// <auto-generated />
using System;
using FightCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FightCore.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211028174255_Added_User_Type")]
    partial class Added_User_Type
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FightCore.Models.ApiClient", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("RateLimit")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("ApiClients");
                });

            modelBuilder.Entity("FightCore.Models.ApplicationUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirebaseUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasAlternateKey("FirebaseUserId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterImageId")
                        .HasColumnType("bigint");

                    b.Property<long?>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("GeneralInformation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("SeriesId")
                        .HasColumnType("bigint");

                    b.Property<long?>("StockIconId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CharacterImageId");

                    b.HasIndex("GameId");

                    b.HasIndex("SeriesId");

                    b.HasIndex("StockIconId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Contributor", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<int>("ContributorType")
                        .HasColumnType("int");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("UserId");

                    b.ToTable("Contributor");
                });

            modelBuilder.Entity("FightCore.Models.Characters.GameSeries", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("GameIconId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameIconId");

                    b.ToTable("GameSeries");
                });

            modelBuilder.Entity("FightCore.Models.Characters.NotablePlayer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Country")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("NotablePlayer");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Stage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Stage");
                });

            modelBuilder.Entity("FightCore.Models.Characters.SuggestedEdit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ApprovedByUserId")
                        .HasColumnType("bigint");

                    b.Property<int>("EditType")
                        .HasColumnType("int");

                    b.Property<int>("Editable")
                        .HasColumnType("int");

                    b.Property<long>("EntityId")
                        .HasColumnType("bigint");

                    b.Property<string>("Original")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Target")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ApprovedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("SuggestedEdit");
                });

            modelBuilder.Entity("FightCore.Models.Characters.WebsiteResource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("WebsiteResources");
                });

            modelBuilder.Entity("FightCore.Models.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BannerUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("IconId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IconId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("FightCore.Models.Globals.FightCoreImage", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FightCoreImage");
                });

            modelBuilder.Entity("FightCore.Models.Globals.InformationSource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("InformationSource");
                });

            modelBuilder.Entity("FightCore.Models.Globals.VideoResource", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YoutubeId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("VideoResource");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("PostedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ParentId");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Like", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("PostId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Like");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Post", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("AuthorId")
                        .HasColumnType("bigint");

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Body");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<long?>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Featured")
                        .HasColumnType("bit");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<string>("HTMLContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("bit");

                    b.Property<string>("Iv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Posted")
                        .HasColumnType("datetime2");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("GameId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Character", b =>
                {
                    b.HasOne("FightCore.Models.Globals.FightCoreImage", "CharacterImage")
                        .WithMany()
                        .HasForeignKey("CharacterImageId");

                    b.HasOne("FightCore.Models.Game", "Game")
                        .WithMany("Characters")
                        .HasForeignKey("GameId");

                    b.HasOne("FightCore.Models.Characters.GameSeries", "Series")
                        .WithMany()
                        .HasForeignKey("SeriesId");

                    b.HasOne("FightCore.Models.Globals.FightCoreImage", "StockIcon")
                        .WithMany()
                        .HasForeignKey("StockIconId");

                    b.Navigation("CharacterImage");

                    b.Navigation("Game");

                    b.Navigation("Series");

                    b.Navigation("StockIcon");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Contributor", b =>
                {
                    b.HasOne("FightCore.Models.Characters.Character", "Character")
                        .WithMany("Contributors")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FightCore.Models.ApplicationUser", "User")
                        .WithMany("Contributors")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FightCore.Models.Characters.GameSeries", b =>
                {
                    b.HasOne("FightCore.Models.Globals.FightCoreImage", "GameIcon")
                        .WithMany()
                        .HasForeignKey("GameIconId");

                    b.Navigation("GameIcon");
                });

            modelBuilder.Entity("FightCore.Models.Characters.NotablePlayer", b =>
                {
                    b.HasOne("FightCore.Models.Characters.Character", "Character")
                        .WithMany("NotablePlayers")
                        .HasForeignKey("CharacterId");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Stage", b =>
                {
                    b.HasOne("FightCore.Models.Game", "Game")
                        .WithMany("Stages")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("FightCore.Models.Characters.SuggestedEdit", b =>
                {
                    b.HasOne("FightCore.Models.ApplicationUser", "ApprovedByUser")
                        .WithMany()
                        .HasForeignKey("ApprovedByUserId");

                    b.HasOne("FightCore.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApprovedByUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FightCore.Models.Characters.WebsiteResource", b =>
                {
                    b.HasOne("FightCore.Models.Characters.Character", "Character")
                        .WithMany("Websites")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");
                });

            modelBuilder.Entity("FightCore.Models.Game", b =>
                {
                    b.HasOne("FightCore.Models.Globals.FightCoreImage", "Icon")
                        .WithMany()
                        .HasForeignKey("IconId");

                    b.Navigation("Icon");
                });

            modelBuilder.Entity("FightCore.Models.Globals.InformationSource", b =>
                {
                    b.HasOne("FightCore.Models.Characters.Character", null)
                        .WithMany("InformationSources")
                        .HasForeignKey("CharacterId");
                });

            modelBuilder.Entity("FightCore.Models.Globals.VideoResource", b =>
                {
                    b.HasOne("FightCore.Models.Characters.Character", "Character")
                        .WithMany("Videos")
                        .HasForeignKey("CharacterId");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Comment", b =>
                {
                    b.HasOne("FightCore.Models.ApplicationUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FightCore.Models.Posts.Comment", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("FightCore.Models.Posts.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Parent");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Like", b =>
                {
                    b.HasOne("FightCore.Models.Posts.Post", "Post")
                        .WithMany("Likes")
                        .HasForeignKey("PostId")
                        .IsRequired();

                    b.HasOne("FightCore.Models.ApplicationUser", "User")
                        .WithMany("Likes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Post", b =>
                {
                    b.HasOne("FightCore.Models.ApplicationUser", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId");

                    b.HasOne("FightCore.Models.Characters.Character", "Character")
                        .WithMany()
                        .HasForeignKey("CharacterId");

                    b.HasOne("FightCore.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Character");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("FightCore.Models.ApplicationUser", b =>
                {
                    b.Navigation("Contributors");

                    b.Navigation("Likes");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("FightCore.Models.Characters.Character", b =>
                {
                    b.Navigation("Contributors");

                    b.Navigation("InformationSources");

                    b.Navigation("NotablePlayers");

                    b.Navigation("Videos");

                    b.Navigation("Websites");
                });

            modelBuilder.Entity("FightCore.Models.Game", b =>
                {
                    b.Navigation("Characters");

                    b.Navigation("Stages");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Comment", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("FightCore.Models.Posts.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });
#pragma warning restore 612, 618
        }
    }
}
