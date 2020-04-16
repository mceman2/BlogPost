﻿// <auto-generated />
using System;
using BlogPosts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BlogPosts.Migrations
{
    [DbContext(typeof(BlogPostsContext))]
    [Migration("20200416113616_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BlogPosts.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Body")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Body = "This page has a list of Drifter Planet’s most popular travel blog posts based on number of views and shares",
                            CreatedAt = new DateTime(2020, 4, 16, 11, 36, 16, 296, DateTimeKind.Utc).AddTicks(7555),
                            Description = "Do you want to read some of our most popular travel blog posts?",
                            Slug = "most-popular-travel-blog-posts",
                            Title = "Most Popular Travel Blog Posts",
                            UpdatedAt = new DateTime(2020, 4, 16, 11, 36, 16, 296, DateTimeKind.Utc).AddTicks(7922)
                        },
                        new
                        {
                            Id = 2,
                            Body = "There are so many cases in South Korea, Italy and Iran that it’s hard to see the rest of the countries, ... ",
                            CreatedAt = new DateTime(2020, 4, 16, 11, 36, 16, 297, DateTimeKind.Utc).AddTicks(8422),
                            Description = "Coronavirus: Why You Must Act Now",
                            Slug = "coronavirus",
                            Title = "Coronavirus",
                            UpdatedAt = new DateTime(2020, 4, 16, 11, 36, 16, 297, DateTimeKind.Utc).AddTicks(8432)
                        });
                });

            modelBuilder.Entity("BlogPosts.Models.PostTag", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("PostTag");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            TagId = 1
                        },
                        new
                        {
                            PostId = 1,
                            TagId = 2
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 3
                        },
                        new
                        {
                            PostId = 2,
                            TagId = 4
                        });
                });

            modelBuilder.Entity("BlogPosts.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Travel"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Vacation"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Corona"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Virus"
                        });
                });

            modelBuilder.Entity("BlogPosts.Models.PostTag", b =>
                {
                    b.HasOne("BlogPosts.Models.Post", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlogPosts.Models.Tag", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}