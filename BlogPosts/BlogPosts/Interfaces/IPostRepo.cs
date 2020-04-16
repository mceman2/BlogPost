using BlogPosts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Interfaces
{
    public interface IPostRepo
    {
        List<Post> GetAll();
        Post GetBySlug(string slug);
        bool CheckIfSlugExist(string slug);
        void Insert(Post post);
        void Update(Post post);
        void Delete(Post post);
    }
}
