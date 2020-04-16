using BlogPosts.Interfaces;
using BlogPosts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPosts.Services
{
    public class TagsService : ITagsService
    {

        private readonly IUnitOfWork _unitOfWork;

        public TagsService(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        public List<string> GetAllTags()
        {
            var allTagsDb = _unitOfWork.TagRepo.GetAll();
            var allTagsDTO = new List<string>();

            foreach (var tag in allTagsDb)
            {
                allTagsDTO.Add(tag.Name);
            }

            return allTagsDTO;
        }
    }
}
