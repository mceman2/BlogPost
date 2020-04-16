using BlogPosts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogTags.Interfaces
{
    public interface ITagRepo
    {
        List<Tag> GetAll();
        Tag GetById(int Id);
        void Insert(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
        Tag CheckIfTagExist(string name);
    }
}
