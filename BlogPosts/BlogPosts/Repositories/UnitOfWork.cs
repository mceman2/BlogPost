using BlogPosts.Data;
using BlogPosts.Interfaces;
using BlogTags.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BlogPostsContext _context;
        private IPostRepo _postRepo;
        private ITagRepo _tagRepo;

        public UnitOfWork(BlogPostsContext context)
        {
            _context = context;
        }
        public IPostRepo PostRepo
        {
            get 
            {
                if (_postRepo == null)
                {
                    _postRepo = new PostRepo(_context);
                }
                return _postRepo;
            }
        }

        public ITagRepo TagRepo
        {
            get
            {
                if (_tagRepo == null)
                {
                    _tagRepo = new TagRepo(_context);
                }
                return _tagRepo;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
