using BlogPosts.DataTransferObjects;
using BlogPosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Interfaces
{
    public interface IPostsService
    {
        PostDTO GetBlogPostBySlug(string slug);
        PostDTO AddNewBlogPost(PostDTO postDTO);
        MultiplePostsDTO GetAllBlogPosts();
        MultiplePostsDTO GetAllBlogPostsByTag(string tag);
        PostDTO UpdateBlogPost(string slug, PostDTO postDTO);
        void DeleteBlogPost(string slug);
        bool CheckIfSlugExistByTitle(string title);
        bool CheckIfSlugExistBySlug(string slug);
    }
}
