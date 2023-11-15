using BusinessLogic.Domain;
using BusinessLogic.Exceptions;
using BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class TagsService
    {
        private readonly ITagsRepository tagsRepository;

        public TagsService(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        public void AddTag(Tag tag)
        {
            var newTag = tagsRepository.Insert(tag.ToDTO());
            tag.Update(newTag);
        }

        public Tag GetTag(int ID) => new(tagsRepository.Get(ID));
       
        public IReadOnlyList<Tag> GetTags()
            => tagsRepository
            .GetAll()
            .Select(dto => new Tag(dto))
            .ToList();

        public void UpdateTag(Tag tag)
        {
            if (!tagsRepository.Exists(tag.ID))
            {
                throw new BusinessLogicException("Tag does not exist");
            }

            tagsRepository.Update(tag.ID, tag.ToDTO());
        }

        public void DeleteTag(int ID) => tagsRepository.Delete(ID);
    }
}
