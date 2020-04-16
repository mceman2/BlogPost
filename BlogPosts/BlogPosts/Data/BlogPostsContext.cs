using BlogPosts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Data
{
    public class BlogPostsContext : DbContext
    {
        public BlogPostsContext(DbContextOptions<BlogPostsContext> options) : base (options)
        {
            
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTag> PostTag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>().HasKey(pt => new { pt.PostId, pt.TagId });

            modelBuilder.Entity<PostTag>().HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>().HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId);

            Tag firstTag = new Tag
            {
                Id = 1,
                Name = "Travel"
            };

            Tag secondTag = new Tag
            {
                Id = 2,
                Name = "Vacation"
            };

            Post firstPost = new Post
            {
                Id = 1,
                Slug = "most-popular-travel-blog-posts",
                Title = "Most Popular Travel Blog Posts",
                Description = "Do you want to read some of our most popular travel blog posts?",
                Body = "This page has a list of Drifter Planet’s most popular travel blog posts based on number of views and shares",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            PostTag firstPostTag = new PostTag
            {
                TagId = firstTag.Id,
                PostId = firstPost.Id
            };

            PostTag secondPostTag = new PostTag
            {
                TagId = secondTag.Id,
                PostId = firstPost.Id
            };

            modelBuilder.Entity<Tag>().HasData(firstTag);
            modelBuilder.Entity<Tag>().HasData(secondTag);
            modelBuilder.Entity<Post>().HasData(firstPost);
            modelBuilder.Entity<PostTag>().HasData(firstPostTag);
            modelBuilder.Entity<PostTag>().HasData(secondPostTag);

            Post secondPost = new Post
            {
                Id = 2,
                Slug = "coronavirus",
                Title = "Coronavirus",
                Description = "Coronavirus: Why You Must Act Now",
                Body = "There are so many cases in South Korea, Italy and Iran that it’s hard to see the rest of the countries, ... ",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Tag thirdTag = new Tag
            {
                Id = 3,
                Name = "Corona"
            };

            Tag fourthTag = new Tag
            {
                Id = 4,
                Name = "Virus"
            };

            PostTag thirdPostTag = new PostTag
            {
                TagId = thirdTag.Id,
                PostId = secondPost.Id
            };

            PostTag fourthPostTag = new PostTag
            {
                TagId = fourthTag.Id,
                PostId = secondTag.Id
            };

            modelBuilder.Entity<Post>().HasData(secondPost);
            modelBuilder.Entity<Tag>().HasData(thirdTag);
            modelBuilder.Entity<Tag>().HasData(fourthTag);
            modelBuilder.Entity<PostTag>().HasData(thirdPostTag);
            modelBuilder.Entity<PostTag>().HasData(fourthPostTag);
        }

    }
}
