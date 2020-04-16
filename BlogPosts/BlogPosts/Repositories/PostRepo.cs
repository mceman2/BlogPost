using BlogPosts.Data;
using BlogPosts.Interfaces;
using BlogPosts.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Repositories
{
    public class PostRepo : IPostRepo
    {
        private readonly BlogPostsContext _context;

        public PostRepo(BlogPostsContext context)
        {
            _context = context;
        }

        public void Delete(Post post)
        {
            _context.Posts.Remove(post);
        }

        public List<Post> GetAll()
        {
            return _context.Posts.Include("PostTags.Tag").ToList();
        }

        public Post GetBySlug(string slug)
        {
            return _context.Posts.Include("PostTags.Tag").FirstOrDefault(p => p.Slug == slug);
        }

        public bool CheckIfSlugExist(string slug)
        {
            return _context.Posts.Any(p => p.Slug == slug);
        }
        public void Insert(Post post)
        {
            _context.Posts.Add(post);
        }

        public void Update(Post post)
        {
            _context.Posts.Update(post);
        }
    }
}
