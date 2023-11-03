﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PandaIdentity.Data;

#nullable disable

namespace PandaIdentity.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20231101063053_paandaIdentityKey")]
    partial class paandaIdentityKey
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PandaIdentity.Models.MySubTask", b =>
                {
                    b.Property<Guid>("SubTaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AssignedTo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MyTaskTaskID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubTaskID");

                    b.HasIndex("MyTaskTaskID");

                    b.ToTable("MySubTasks");
                });

            modelBuilder.Entity("PandaIdentity.Models.MyTask", b =>
                {
                    b.Property<Guid>("TaskID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskID");

                    b.ToTable("MyTasks");
                });

            modelBuilder.Entity("PandaIdentity.Models.MySubTask", b =>
                {
                    b.HasOne("PandaIdentity.Models.MyTask", "MyTask")
                        .WithMany("MySubTasks")
                        .HasForeignKey("MyTaskTaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyTask");
                });

            modelBuilder.Entity("PandaIdentity.Models.MyTask", b =>
                {
                    b.Navigation("MySubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}
