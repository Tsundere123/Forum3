﻿// <auto-generated />
using System;
using Forum3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Forum3.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20231121142428_InitForumDb")]
    partial class InitForumDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("Forum3.Models.ForumCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ForumCategory");
                });

            modelBuilder.Entity("Forum3.Models.ForumPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("EditedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("EditedBy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsSoftDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ThreadId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ThreadId");

                    b.ToTable("ForumPost");
                });

            modelBuilder.Entity("Forum3.Models.ForumThread", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ForumCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ForumThreadId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ForumCategoryId");

                    b.HasIndex("ForumThreadId");

                    b.ToTable("ForumThread");
                });

            modelBuilder.Entity("Forum3.Models.WallPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProfileId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WallPost");
                });

            modelBuilder.Entity("Forum3.Models.WallPostReply", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AuthorId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("WallPostId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("WallPostId");

                    b.ToTable("WallPostReply");
                });

            modelBuilder.Entity("Forum3.Models.ForumPost", b =>
                {
                    b.HasOne("Forum3.Models.ForumThread", "Thread")
                        .WithMany()
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Thread");
                });

            modelBuilder.Entity("Forum3.Models.ForumThread", b =>
                {
                    b.HasOne("Forum3.Models.ForumCategory", null)
                        .WithMany("Threads")
                        .HasForeignKey("ForumCategoryId");

                    b.HasOne("Forum3.Models.ForumThread", null)
                        .WithMany("Threads")
                        .HasForeignKey("ForumThreadId");
                });

            modelBuilder.Entity("Forum3.Models.WallPostReply", b =>
                {
                    b.HasOne("Forum3.Models.WallPost", "WallPost")
                        .WithMany("Replies")
                        .HasForeignKey("WallPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WallPost");
                });

            modelBuilder.Entity("Forum3.Models.ForumCategory", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("Forum3.Models.ForumThread", b =>
                {
                    b.Navigation("Threads");
                });

            modelBuilder.Entity("Forum3.Models.WallPost", b =>
                {
                    b.Navigation("Replies");
                });
#pragma warning restore 612, 618
        }
    }
}
