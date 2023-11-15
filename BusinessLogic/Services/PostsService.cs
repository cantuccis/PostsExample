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
    internal class PostsService
    {
        private readonly IPostsRepository postsRepository;

        public PostsService(IPostsRepository postsRepository)
        {
            this.postsRepository = postsRepository;
        }

        public void AddPost(Post post)
        {
            if (postsRepository.ExistsByTitle(post.Title))
            {
                throw new BusinessLogicException("Post title already exists");
            }
            var newPost = postsRepository.Insert(post.ToDTO());
            post.Update(newPost);
        }

        public IReadOnlyList<Post> GetPosts(string? title = null, PostOrderBy? orderBy = null)
            => postsRepository
            .GetAll(title: title, orderBy: orderBy)
            .Select(dto => new Post(dto))
            .ToList();
    }
}
