﻿// <auto-generated />
using System;
using House.Flix.PostgreSQL.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace House.Flix.PostgreSQL.Migrations
{
    [DbContext(typeof(PostgresHouseFlixStorage))]
    partial class PostgresHouseFlixStorageModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("House.Flix.Core.Common.Entities.VideoFileEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Size")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("VideoFileEntity");
                });

            modelBuilder.Entity("House.Flix.Core.Movies.Entities.MovieEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Rating")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("VideoFileId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("VideoFileId");

                    b.ToTable("MovieEntity");
                });

            modelBuilder.Entity("House.Flix.Core.Movies.Entities.MovieRoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<Guid>("MovieId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("PersonId");

                    b.HasIndex("RoleId");

                    b.ToTable("MovieRoleEntity");
                });

            modelBuilder.Entity("House.Flix.Core.People.Entities.PersonEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("PersonEntity");
                });

            modelBuilder.Entity("House.Flix.Core.People.Entities.RoleEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasDefaultValueSql("gen_random_uuid()");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoleEntity");
                });

            modelBuilder.Entity("House.Flix.Core.Movies.Entities.MovieEntity", b =>
                {
                    b.HasOne("House.Flix.Core.Common.Entities.VideoFileEntity", "VideoFile")
                        .WithMany()
                        .HasForeignKey("VideoFileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VideoFile");
                });

            modelBuilder.Entity("House.Flix.Core.Movies.Entities.MovieRoleEntity", b =>
                {
                    b.HasOne("House.Flix.Core.Movies.Entities.MovieEntity", "Movie")
                        .WithMany("MovieRoles")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("House.Flix.Core.People.Entities.PersonEntity", "Person")
                        .WithMany("MovieRoles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("House.Flix.Core.People.Entities.RoleEntity", "Role")
                        .WithMany("MovieRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("Person");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("House.Flix.Core.Movies.Entities.MovieEntity", b =>
                {
                    b.Navigation("MovieRoles");
                });

            modelBuilder.Entity("House.Flix.Core.People.Entities.PersonEntity", b =>
                {
                    b.Navigation("MovieRoles");
                });

            modelBuilder.Entity("House.Flix.Core.People.Entities.RoleEntity", b =>
                {
                    b.Navigation("MovieRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
