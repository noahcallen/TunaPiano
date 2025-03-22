﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TunaPiano.Migrations
{
    [DbContext(typeof(TunaPianoDbContext))]
    [Migration("20250322165406_Calls")]
    partial class Calls
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.Property<int>("GenresId")
                        .HasColumnType("integer");

                    b.Property<int>("SongsId")
                        .HasColumnType("integer");

                    b.HasKey("GenresId", "SongsId");

                    b.HasIndex("SongsId");

                    b.ToTable("GenreSong");

                    b.HasData(
                        new
                        {
                            GenresId = 2,
                            SongsId = 1
                        },
                        new
                        {
                            GenresId = 5,
                            SongsId = 1
                        },
                        new
                        {
                            GenresId = 2,
                            SongsId = 2
                        },
                        new
                        {
                            GenresId = 5,
                            SongsId = 2
                        },
                        new
                        {
                            GenresId = 2,
                            SongsId = 3
                        },
                        new
                        {
                            GenresId = 3,
                            SongsId = 3
                        },
                        new
                        {
                            GenresId = 1,
                            SongsId = 4
                        },
                        new
                        {
                            GenresId = 5,
                            SongsId = 4
                        });
                });

            modelBuilder.Entity("Tuna.Models.Artist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Age = 26,
                            Bio = "A talented rapper known for his introspective lyrics and smooth beats.",
                            Name = "Mac Miller"
                        },
                        new
                        {
                            Id = 2,
                            Age = 28,
                            Bio = "A Canadian rock duo blending gritty, raw guitar sounds with emotionally charged lyrics.",
                            Name = "Cleopatrick"
                        },
                        new
                        {
                            Id = 3,
                            Age = 20,
                            Bio = "An iconic emo rock band known for their dramatic style and powerful anthems.",
                            Name = "My Chemical Romance"
                        });
                });

            modelBuilder.Entity("Tuna.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Indie"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Rock"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Emo"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Alternative"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Pop"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Hip Hop"
                        });
                });

            modelBuilder.Entity("Tuna.Models.Song", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Album")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ArtistId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Length")
                        .HasColumnType("numeric");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ArtistId");

                    b.ToTable("Song");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Album = "Circles",
                            ArtistId = 1,
                            Length = 5.39m,
                            Title = "Good News"
                        },
                        new
                        {
                            Id = 2,
                            Album = "Bummer",
                            ArtistId = 2,
                            Length = 4.12m,
                            Title = "Hometown"
                        },
                        new
                        {
                            Id = 3,
                            Album = "The Black Parade",
                            ArtistId = 3,
                            Length = 5.11m,
                            Title = "Welcome to the Black Parade"
                        },
                        new
                        {
                            Id = 4,
                            Album = "Swimming",
                            ArtistId = 1,
                            Length = 4.14m,
                            Title = "Clever"
                        });
                });

            modelBuilder.Entity("GenreSong", b =>
                {
                    b.HasOne("Tuna.Models.Genre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Tuna.Models.Song", null)
                        .WithMany()
                        .HasForeignKey("SongsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tuna.Models.Song", b =>
                {
                    b.HasOne("Tuna.Models.Artist", null)
                        .WithMany("Songs")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Tuna.Models.Artist", b =>
                {
                    b.Navigation("Songs");
                });
#pragma warning restore 612, 618
        }
    }
}
