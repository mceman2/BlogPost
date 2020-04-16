using BlogPosts.DataTransferObjects;
using BlogPosts.Interfaces;
using BlogPosts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPosts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postService;

        public PostsController(IPostsService postService)
        {
            _postService = postService;
        }

        // GET: api/Posts
        [HttpGet]
        public ActionResult GetAllBlogPosts(string tag)
        {
            MultiplePostsDTO mpDTO;
            if (tag == null)
            {
                mpDTO = _postService.GetAllBlogPosts();
            }
            else {
                mpDTO = _postService.GetAllBlogPostsByTag(tag);
            }
            return Ok(mpDTO);
        }

        // GET: api/Posts/slug
        [HttpGet("{slug}")]
        public ActionResult Get(string slug)
        {
            BlogPostDTO blogPost = new BlogPostDTO(); 
            blogPost.blogPost = _postService.GetBlogPostBySlug(slug);
            return Ok(blogPost);
        }

        // POST: api/Posts
        [HttpPost]
        public ActionResult Post([FromBody] BlogPostDTO blogPost)
        {
            try
            {
                if (blogPost.blogPost.Title == null || blogPost.blogPost.Description == null || blogPost.blogPost.Body == null)
                    throw new ArgumentException("Title, Description and Body are required fields.");
                if (_postService.CheckIfSlugExistByTitle(blogPost.blogPost.Title))
                    throw new ArgumentException("Blog Post with this Title already exist.");
                BlogPostDTO blogPostToReturn = new BlogPostDTO();
                blogPostToReturn.blogPost = _postService.AddNewBlogPost(blogPost.blogPost);
                return Ok(blogPostToReturn);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

            
        }

        // PUT: api/Posts/slug
        [HttpPut("{slug}")]
        public ActionResult Put(string slug, [FromBody] BlogPostDTO blogPost)
        {
            try
            {
                if (!_postService.CheckIfSlugExistBySlug(slug))
                    throw new ArgumentException("Blog Post with this slug doesn't exist.");
                if (blogPost.blogPost.Title != null && _postService.CheckIfSlugExistByTitle(blogPost.blogPost.Title))
                    throw new ArgumentException("Blog Post with this Title already exist.");
                BlogPostDTO blogPostToReturn = new BlogPostDTO();
                blogPostToReturn.blogPost = _postService.UpdateBlogPost(slug, blogPost.blogPost);
                return Ok(blogPostToReturn);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Posts/slug
        [HttpDelete("{slug}")]
        public void Delete(string slug)
        {
            _postService.DeleteBlogPost(slug);
        }
    }
}
