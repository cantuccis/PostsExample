using BusinessLogic.Domain;
using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Controllers
{
    public class FeedManager
    {
        private readonly PostsService postsService;
        private readonly TagsService tagsService;

        public FeedManager(IPostsRepository postsRepository, ITagsRepository tagsRepository) 
        {
            postsService = new PostsService(postsRepository);
            tagsService = new TagsService(tagsRepository);
        }

        public PostDTO AddPost(PostDTO postData)
        {
            var post = new Post(postData);
            postsService.AddPost(post);
            return post.ToDTO();
        }

        public IReadOnlyList<PostDTO> GetPosts(string? title = null, PostOrderBy? orderBy = null)
        {
           return postsService.GetPosts(title, orderBy).Select(p => p.ToDTO()).ToList();
        }

        public TagDTO AddTag(TagDTO tagData)
        {
            var tag = new Tag(tagData);
            tagsService.AddTag(tag);
            return tag.ToDTO();
        }

        public IReadOnlyList<TagDTO> GetTags()
        {
            return tagsService.GetTags().Select(t => t.ToDTO()).ToList();
        }

        public TagDTO UpdateTag(TagDTO tagData)
        {
            var tag = new Tag(tagData);
            tagsService.UpdateTag(tag);
            var updatedTag = tagsService.GetTag(tag.ID);
            return updatedTag.ToDTO();
        }

        public void DeleteTag(int ID) => tagsService.DeleteTag(ID);
    }
}
