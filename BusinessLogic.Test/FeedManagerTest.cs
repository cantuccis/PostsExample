using BusinessLogic.Controllers;
using BusinessLogic.Interfaces;
using DataAccess;
using DataAccess.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BusinessLogic.Test
{
    [TestClass]
    public class FeedManagerTest
    {
        IConfiguration config = ConfigReader.InitConfiguration();
        IPostsRepository postsRepository;
        ITagsRepository tagsRepository;

        [TestInitialize]
        public void Init()
        {
            using var dbcontext = new EFContext() { Config = config };
            dbcontext.PostEntities.RemoveRange(dbcontext.PostEntities.ToList());
            dbcontext.TagEntities.RemoveRange(dbcontext.TagEntities.ToList());
            dbcontext.SaveChanges();

            postsRepository = new EFPostsRepository(config);
            tagsRepository = new EFTagsRepository(config);
        }

        [TestMethod]
        public void CreatePostWithTagsTest()
        {
            var tags = new List<Dtos.TagDTO>() { 
                new Dtos.TagDTO() { Name = "DA1" } 
            };
            var postDto = new Dtos.PostDTO
            {
                Author = "El cantu",
                CreatedAt = DateTime.Now,
                Description = "description",
                Title = "Title",
                Tags = tags,
            };
            var feedManager = new FeedManager(postsRepository, tagsRepository);

            feedManager.AddPost(postDto);
            var posts = feedManager.GetPosts();

            Assert.AreEqual(1, posts.Count);
            var post = posts[0];
            Assert.IsTrue(post.ID != 0);
            Assert.IsTrue(post.Author == "El cantu");
            Assert.IsTrue(post.Tags?.Count  == 1);
        }

        [TestMethod]
        public void CreatePostsReusingTagsTest()
        {
            var tags = new List<Dtos.TagDTO>() {
                new Dtos.TagDTO() { Name = "DA1" },
                new Dtos.TagDTO() { Name = "DA2"}
            };
            var postDto1 = new Dtos.PostDTO
            {
                Author = "El cantu",
                CreatedAt = DateTime.Now,
                Description = "description",
                Title = "Title 1",
                Tags = new List<Dtos.TagDTO>() { tags[0] },
            };
           
            var feedManager = new FeedManager(postsRepository, tagsRepository);

            postDto1 = feedManager.AddPost(postDto1);

            var postDto2 = new Dtos.PostDTO
            {
                Author = "El cantu",
                CreatedAt = DateTime.Now,
                Description = "description",
                Title = "Title 2",
                Tags = new List<Dtos.TagDTO>() { postDto1.Tags![0], tags[1] },
            };
            feedManager.AddPost(postDto2);
            var posts = feedManager.GetPosts();

            Assert.AreEqual(2, posts.Count);
            var post1 = posts[0];
            Assert.IsTrue(post1.ID != 0);
            Assert.IsTrue(post1.Tags?.Count == 1);
            var post2 = posts[1];
            Assert.IsTrue(post2.Tags?.Any(t => t.ID == post1.Tags[0].ID));
        }
    }
}