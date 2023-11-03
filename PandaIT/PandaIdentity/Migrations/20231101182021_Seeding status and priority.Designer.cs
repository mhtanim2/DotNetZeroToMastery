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
    [Migration("20231101182021_Seeding status and priority")]
    partial class Seedingstatusandpriority
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
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("MyTaskTaskID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PriorityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StatusId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SubTaskID");

                    b.HasIndex("MyTaskTaskID");

                    b.HasIndex("PriorityId");

                    b.HasIndex("StatusId");

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
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskID");

                    b.ToTable("MyTasks");
                });

            modelBuilder.Entity("PandaIdentity.Models.Priority", b =>
                {
                    b.Property<Guid>("PriorityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PriorityType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PriorityId");

                    b.ToTable("priorities");

                    b.HasData(
                        new
                        {
                            PriorityId = new Guid("d9dc7ae1-5ec5-422a-a902-5fc375d29d2d"),
                            PriorityType = "Easy"
                        },
                        new
                        {
                            PriorityId = new Guid("d077477f-7aca-4cd0-8dd3-6f01865232c1"),
                            PriorityType = "Medium"
                        },
                        new
                        {
                            PriorityId = new Guid("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"),
                            PriorityType = "Hard"
                        });
                });

            modelBuilder.Entity("PandaIdentity.Models.Status", b =>
                {
                    b.Property<Guid>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("StatusType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            StatusId = new Guid("57c9293e-73f9-43eb-9d8d-2dcfc0d3aa00"),
                            StatusType = "ToDo"
                        },
                        new
                        {
                            StatusId = new Guid("be0885f8-885c-47bc-bd90-f6d9e4f7f568"),
                            StatusType = "Doing"
                        },
                        new
                        {
                            StatusId = new Guid("bcd426c5-dd7c-48fd-bab0-673d2c18f50c"),
                            StatusType = "Done"
                        });
                });

            modelBuilder.Entity("PandaIdentity.Models.MySubTask", b =>
                {
                    b.HasOne("PandaIdentity.Models.MyTask", "MyTask")
                        .WithMany("MySubTasks")
                        .HasForeignKey("MyTaskTaskID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PandaIdentity.Models.Priority", "Priority")
                        .WithMany()
                        .HasForeignKey("PriorityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PandaIdentity.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MyTask");

                    b.Navigation("Priority");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PandaIdentity.Models.MyTask", b =>
                {
                    b.Navigation("MySubTasks");
                });
#pragma warning restore 612, 618
        }
    }
}