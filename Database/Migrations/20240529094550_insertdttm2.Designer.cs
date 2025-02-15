﻿// <auto-generated />
using System;
using Bissell.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bissell.Database.Migrations
{
    [DbContext(typeof(BugTrackerDbContext))]
    [Migration("20240529094550_insertdttm2")]
    partial class insertdttm2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bissell.Database.Entities.Bug", b =>
                {
                    b.Property<int>("BugId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BugId"));

                    b.Property<int?>("AssignedPersonId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("InsertedDttm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedDttm")
                        .HasColumnType("datetime2");

                    b.HasKey("BugId");

                    b.HasIndex("AssignedPersonId");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("Bissell.Database.Entities.BugHistory", b =>
                {
                    b.Property<int>("BugHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BugHistoryId"));

                    b.Property<int?>("AssignedPersonId")
                        .HasColumnType("int");

                    b.Property<int>("BugId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("InsertedDttm")
                        .HasColumnType("datetime2");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedDttm")
                        .HasColumnType("datetime2");

                    b.HasKey("BugHistoryId");

                    b.HasIndex("AssignedPersonId");

                    b.HasIndex("BugId");

                    b.ToTable("BugsHistory");
                });

            modelBuilder.Entity("Bissell.Database.Entities.Person", b =>
                {
                    b.Property<int>("PersonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonId"));

                    b.Property<string>("EmailAddress")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Forename")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime>("InsertedDttm")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("TelephoneNo")
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("UpdatedDttm")
                        .HasColumnType("datetime2");

                    b.HasKey("PersonId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("Bissell.Database.Entities.Bug", b =>
                {
                    b.HasOne("Bissell.Database.Entities.Person", "AssignedPerson")
                        .WithMany("AssignedBugs")
                        .HasForeignKey("AssignedPersonId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("AssignedPerson");
                });

            modelBuilder.Entity("Bissell.Database.Entities.BugHistory", b =>
                {
                    b.HasOne("Bissell.Database.Entities.Person", "AssignedPerson")
                        .WithMany()
                        .HasForeignKey("AssignedPersonId");

                    b.HasOne("Bissell.Database.Entities.Bug", "CurrentBug")
                        .WithMany("History")
                        .HasForeignKey("BugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedPerson");

                    b.Navigation("CurrentBug");
                });

            modelBuilder.Entity("Bissell.Database.Entities.Bug", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("Bissell.Database.Entities.Person", b =>
                {
                    b.Navigation("AssignedBugs");
                });
#pragma warning restore 612, 618
        }
    }
}
