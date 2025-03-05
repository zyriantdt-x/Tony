﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Tony.Rooms.Storage;

#nullable disable

namespace Tony.Rooms.Migrations
{
    [DbContext(typeof(RoomStorage))]
    partial class RoomStorageModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.2");

            modelBuilder.Entity("RoomModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT")
                        .HasColumnName("model_id");

                    b.Property<int>("DoorDir")
                        .HasColumnType("INTEGER")
                        .HasColumnName("door_dir");

                    b.Property<int>("DoorX")
                        .HasColumnType("INTEGER")
                        .HasColumnName("door_x");

                    b.Property<int>("DoorY")
                        .HasColumnType("INTEGER")
                        .HasColumnName("door_y");

                    b.Property<int>("DoorZ")
                        .HasColumnType("INTEGER")
                        .HasColumnName("door_z");

                    b.Property<string>("Heightmap")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("heightmap");

                    b.Property<string>("TriggerClass")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("trigger_class");

                    b.HasKey("Id");

                    b.ToTable("room_models");
                });

            modelBuilder.Entity("Tony.Rooms.Storage.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<bool>("IsNode")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_node");

                    b.Property<bool>("IsPublicSpace")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_public_space");

                    b.Property<bool>("IsTradingAllowed")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_trading_allowed");

                    b.Property<int>("MinAccess")
                        .HasColumnType("INTEGER")
                        .HasColumnName("min_access");

                    b.Property<int>("MinAssign")
                        .HasColumnType("INTEGER")
                        .HasColumnName("min_assign");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("name");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("parent_id");

                    b.HasKey("Id");

                    b.ToTable("navigator_categories");
                });

            modelBuilder.Entity("Tony.Rooms.Storage.Entities.RoomData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("id");

                    b.Property<int>("AccessType")
                        .HasColumnType("INTEGER")
                        .HasColumnName("accesstype");

                    b.Property<int>("Category")
                        .HasColumnType("INTEGER")
                        .HasColumnName("category");

                    b.Property<string>("Ccts")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("ccts");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("created_at");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("description");

                    b.Property<int>("Floor")
                        .HasColumnType("INTEGER")
                        .HasColumnName("floor");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("INTEGER")
                        .HasColumnName("is_hidden");

                    b.Property<string>("ModelId")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("model");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("varchar(11)")
                        .HasColumnName("owner_id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<int>("Rating")
                        .HasColumnType("INTEGER")
                        .HasColumnName("rating");

                    b.Property<bool>("ShowName")
                        .HasColumnType("INTEGER")
                        .HasColumnName("showname");

                    b.Property<bool>("SuperUsers")
                        .HasColumnType("INTEGER")
                        .HasColumnName("superusers");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("TEXT")
                        .HasColumnName("updated_at");

                    b.Property<int>("VisitorsMax")
                        .HasColumnType("INTEGER")
                        .HasColumnName("visitors_max");

                    b.Property<int>("VisitorsNow")
                        .HasColumnType("INTEGER")
                        .HasColumnName("visitors_now");

                    b.Property<int>("Wallpaper")
                        .HasColumnType("INTEGER")
                        .HasColumnName("wallpaper");

                    b.HasKey("Id");

                    b.HasIndex("ModelId")
                        .IsUnique();

                    b.ToTable("room_data");
                });

            modelBuilder.Entity("Tony.Rooms.Storage.Entities.RoomData", b =>
                {
                    b.HasOne("RoomModel", "Model")
                        .WithOne()
                        .HasForeignKey("Tony.Rooms.Storage.Entities.RoomData", "ModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Model");
                });
#pragma warning restore 612, 618
        }
    }
}
