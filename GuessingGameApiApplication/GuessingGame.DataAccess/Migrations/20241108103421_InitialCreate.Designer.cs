﻿// <auto-generated />
using System;
using GuessingGame.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GuessingGame.DataAccess.Migrations
{
    [DbContext(typeof(GuessingGameDbContext))]
    [Migration("20241108103421_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameAttempt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AttemptNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("AttemptTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("GameSessionId")
                        .HasColumnType("int");

                    b.Property<int>("GuessedNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId");

                    b.ToTable("GameAttempt");
                });

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("GameSessionId")
                        .HasColumnType("int");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GameSessionId")
                        .IsUnique();

                    b.ToTable("GameResults");
                });

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SecretNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("GameSessions");
                });

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameAttempt", b =>
                {
                    b.HasOne("GuessingGame.DataAccess.Entities.GameSession", "GameSession")
                        .WithMany("GameAttempts")
                        .HasForeignKey("GameSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameResult", b =>
                {
                    b.HasOne("GuessingGame.DataAccess.Entities.GameSession", "GameSession")
                        .WithOne("GameResult")
                        .HasForeignKey("GuessingGame.DataAccess.Entities.GameResult", "GameSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GameSession");
                });

            modelBuilder.Entity("GuessingGame.DataAccess.Entities.GameSession", b =>
                {
                    b.Navigation("GameAttempts");

                    b.Navigation("GameResult")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
