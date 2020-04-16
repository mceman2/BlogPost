using BlogPosts.DataTransferObjects;
using BlogPosts.Interfaces;
using BlogPosts.Models;
using BlogPosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogPosts.Services
{
    public class PostService : IPostsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PostService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public PostDTO AddNewBlogPost(PostDTO postDTO)
        {
            Post postDB = new Post();

            postDB.Title = postDTO.Title;
            postDB.Description = postDTO.Description;
            postDB.Body = postDTO.Body;
            postDB.Slug = GenerateSlug(postDTO.Title);
            postDB.CreatedAt = DateTime.UtcNow;
            postDB.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PostRepo.Insert(postDB);

            var allTagsDB = _unitOfWork.TagRepo.GetAll();
            postDB.PostTags = new List<PostTag>();

            foreach (string tagDTO in postDTO.TagList)
            {
                bool tagDtoExist = false;
                int tagDbId = 0;
                foreach (Tag tagDb in allTagsDB)
                {
                    if (tagDb.Name == tagDTO)
                    {
                        tagDtoExist = true;
                        tagDbId = tagDb.Id;
                    }
                }
                if (tagDtoExist)
                {
                    postDB.PostTags.Add(new PostTag { TagId = tagDbId });
                }
                else {
                    Tag tagToAdd = new Tag
                    {                      
                        Name = tagDTO
                    };
                    tagToAdd.PostTags = new List<PostTag>();
                    
                    tagToAdd.PostTags.Add(new PostTag { Post = postDB });
                    _unitOfWork.TagRepo.Insert(tagToAdd);

                }
            }

            _unitOfWork.Save();

            return GetBlogPostBySlug(postDB.Slug);
        }

        public PostDTO GetBlogPostBySlug(string slug)
        {
            Post dbPost = _unitOfWork.PostRepo.GetBySlug(slug);

            PostDTO postDTO = new PostDTO();
            postDTO.TagList = new List<string>();

            postDTO.Slug = dbPost.Slug;
            postDTO.Title = dbPost.Title;
            postDTO.Description = dbPost.Description;
            postDTO.Body = dbPost.Body;
            postDTO.CreatedAt = dbPost.CreatedAt;
            postDTO.UpdatedAt = dbPost.UpdatedAt;

            postDTO.TagList = new List<string>();
            foreach (PostTag postTag in dbPost.PostTags)
            {
                postDTO.TagList.Add(postTag.Tag.Name);
            }

            return postDTO;
        }

        public MultiplePostsDTO GetAllBlogPosts()
        {
            var allPosts = _unitOfWork.PostRepo.GetAll().OrderByDescending(p => p.CreatedAt);

            MultiplePostsDTO mpDto = new MultiplePostsDTO();
            mpDto.blogPosts = new List<PostDTO>();

            foreach (var post in allPosts)
            {
                PostDTO pDto = new PostDTO();
                pDto.TagList = new List<string>();

                pDto.Slug = post.Slug;
                pDto.Title = post.Title;
                pDto.Description = post.Description;
                pDto.Body = post.Body;
                pDto.CreatedAt = post.CreatedAt;
                pDto.UpdatedAt = post.UpdatedAt;
                foreach (PostTag postTag in post.PostTags)
                {
                    pDto.TagList.Add(postTag.Tag.Name);
                }

                mpDto.blogPosts.Add(pDto);
            }
            mpDto.postsCount = mpDto.blogPosts.Count;

            return mpDto;
        }

        public MultiplePostsDTO GetAllBlogPostsByTag(string tag)
        {
            var allPosts = _unitOfWork.PostRepo.GetAll().OrderByDescending(p => p.CreatedAt);

            MultiplePostsDTO mpDto = new MultiplePostsDTO();
            mpDto.blogPosts = new List<PostDTO>();

            foreach (var post in allPosts)
            {
                bool postToAdd = false;
                foreach (PostTag postTag in post.PostTags)
                {
                    if (postTag.Tag.Name == tag)
                    {
                        postToAdd = true;
                    }
                }

                if (postToAdd)
                {
                    PostDTO pDto = new PostDTO();
                    pDto.TagList = new List<string>();

                    pDto.Slug = post.Slug;
                    pDto.Title = post.Title;
                    pDto.Description = post.Description;
                    pDto.Body = post.Body;
                    pDto.CreatedAt = post.CreatedAt;
                    pDto.UpdatedAt = post.UpdatedAt;
                    foreach (PostTag postTag in post.PostTags)
                    {
                        pDto.TagList.Add(postTag.Tag.Name);
                    }

                    mpDto.blogPosts.Add(pDto);
                }
            }
            mpDto.postsCount = mpDto.blogPosts.Count;

            return mpDto;
        }

        public void DeleteBlogPost(string slug)
        {
            Post dbPostToDelete = _unitOfWork.PostRepo.GetBySlug(slug);
            _unitOfWork.PostRepo.Delete(dbPostToDelete);
            _unitOfWork.Save();
        }

        public PostDTO UpdateBlogPost(string slug, PostDTO postDTO)
        {
            Post dbPost = _unitOfWork.PostRepo.GetBySlug(slug);

            if (postDTO.Title != null)
            {
                dbPost.Title = postDTO.Title;
                dbPost.Slug = GenerateSlug(postDTO.Title);
                foreach (PostTag postTag in dbPost.PostTags)
                {
                    postTag.Post = dbPost;
                }
            }
            if (postDTO.Description != null)
                dbPost.Description = postDTO.Description;
            if (postDTO.Body != null)
                dbPost.Body = postDTO.Body;

            dbPost.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.PostRepo.Update(dbPost);

            _unitOfWork.Save();

            return GetBlogPostBySlug(dbPost.Slug);
        }

        public bool CheckIfSlugExistByTitle(string title) 
        {
            return _unitOfWork.PostRepo.CheckIfSlugExist(GenerateSlug(title));
        }

        public bool CheckIfSlugExistBySlug(string slug)
        {
            return _unitOfWork.PostRepo.CheckIfSlugExist(slug);
        }

        public static string RemovePunctuation(string text)
        {
            var sb = new StringBuilder();

            foreach (char c in text)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }

            return sb.ToString();
        }
        public static string GenerateSlug(string phrase)
        {
            string str = RemovePunctuation(RemoveDiacritics(phrase).ToLower());
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public static string RemoveDiacritics(string text)
        {
            var s = new string(text.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

            return s.Normalize(NormalizationForm.FormC);
        }
    }
}
