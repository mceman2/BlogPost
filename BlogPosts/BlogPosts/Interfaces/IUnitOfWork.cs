using BlogTags.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Interfaces
{
    public interface IUnitOfWork
    {
        public IPostRepo PostRepo { get; }
        public ITagRepo TagRepo { get; }
        void Save();
    }
}
