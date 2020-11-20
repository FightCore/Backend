﻿// <auto-generated />
using System;
using FightCore.FrameData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FightCore.FrameData.Migrations
{
    [DbContext(typeof(FrameDataContext))]
    [Migration("20201120065311_AddNewSpecificFields")]
    partial class AddNewSpecificFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FightCore.FrameData.Models.Character", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("CharacterStatisticsId")
                        .HasColumnType("bigint");

                    b.Property<long>("FightCoreId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CharacterStatisticsId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.CharacterStatistics", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanWallJump")
                        .HasColumnType("bit");

                    b.Property<int>("DashFrames")
                        .HasColumnType("int");

                    b.Property<double>("Gravity")
                        .HasColumnType("float");

                    b.Property<double>("InitialDash")
                        .HasColumnType("float");

                    b.Property<int>("JumpSquat")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PLAIntangibilityFrames")
                        .HasColumnType("int");

                    b.Property<double>("RunSpeed")
                        .HasColumnType("float");

                    b.Property<double>("WalkSpeed")
                        .HasColumnType("float");

                    b.Property<double>("WaveDashLength")
                        .HasColumnType("float");

                    b.Property<int>("WaveDashLengthRank")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CharacterStatistics");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.Hitbox", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Angle")
                        .HasColumnType("bigint");

                    b.Property<long>("BaseKnockback")
                        .HasColumnType("bigint");

                    b.Property<long>("Damage")
                        .HasColumnType("bigint");

                    b.Property<string>("Effect")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HitlagAttacker")
                        .HasColumnType("int");

                    b.Property<int>("HitlagDefender")
                        .HasColumnType("int");

                    b.Property<long>("KnockbackGrowth")
                        .HasColumnType("bigint");

                    b.Property<long?>("MoveId")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SetKnockback")
                        .HasColumnType("bigint");

                    b.Property<int>("Shieldstun")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MoveId");

                    b.ToTable("Hitbox");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.Move", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutoCancelAfter")
                        .HasColumnType("int");

                    b.Property<int?>("AutoCancelBefore")
                        .HasColumnType("int");

                    b.Property<long>("CharacterId")
                        .HasColumnType("bigint");

                    b.Property<int?>("End")
                        .HasColumnType("int");

                    b.Property<int?>("IASA")
                        .HasColumnType("int");

                    b.Property<int?>("LCanceledLandLag")
                        .HasColumnType("int");

                    b.Property<int?>("LandLag")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Percent")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Start")
                        .HasColumnType("int");

                    b.Property<int>("TotalFrames")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.ToTable("Move");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.Character", b =>
                {
                    b.HasOne("FightCore.FrameData.Models.CharacterStatistics", "CharacterStatistics")
                        .WithMany()
                        .HasForeignKey("CharacterStatisticsId");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.Hitbox", b =>
                {
                    b.HasOne("FightCore.FrameData.Models.Move", null)
                        .WithMany("Hitboxes")
                        .HasForeignKey("MoveId");
                });

            modelBuilder.Entity("FightCore.FrameData.Models.Move", b =>
                {
                    b.HasOne("FightCore.FrameData.Models.Character", "Character")
                        .WithMany("Moves")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
