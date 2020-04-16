using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogPosts.Interfaces;
using BlogPosts.Services;
using BlogPosts.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPosts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        // GET: api/Tags
        [HttpGet]
        public ActionResult Get()
        {
            TagDTO tagDto = new TagDTO();
            tagDto.Tags =_tagsService.GetAllTags();
            return Ok(tagDto);
        }

    }
}
