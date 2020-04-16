using BlogPosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.DataTransferObjects
{
    public class MultiplePostsDTO
    {
        public List<PostDTO> blogPosts { get; set; }
        public int postsCount { get; set; }
    }
}
