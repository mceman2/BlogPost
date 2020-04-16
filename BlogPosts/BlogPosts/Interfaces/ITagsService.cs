using BlogPosts.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Interfaces
{
    public interface ITagsService
    {
        List<string> GetAllTags();
    }
}
