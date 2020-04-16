using BlogPosts.Data;
using BlogPosts.Models;
using BlogTags.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Repositories
{
    public class TagRepo : ITagRepo
    {
        private readonly BlogPostsContext _context;

        public TagRepo(BlogPostsContext context)
        {
            _context = context;
        }

        public void Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
        }

        public List<Tag> GetAll()
        {
            return _context.Tags.ToList();
        }

        public Tag GetById(int Id)
        {
            return _context.Tags.FirstOrDefault(t => t.Id == Id);
        }

        public void Insert(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public void Update(Tag tag)
        {
            _context.Tags.Update(tag);
        }

        public Tag CheckIfTagExist(string name)
        {
            return _context.Tags.FirstOrDefault(t => t.Name == name);
        }
    }
}
