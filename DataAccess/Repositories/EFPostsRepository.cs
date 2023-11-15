using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class EFPostsRepository : IPostsRepository
    {
        private IConfiguration config;
        public EFPostsRepository(IConfiguration config) 
        {
            this.config = config;
        }

        public void Delete(int key)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.PostEntities.Where(e => e.Id == key).FirstOrDefault() ?? throw new DataAccessException("Post does not exist");
                ctx.PostEntities.Remove(entity);
                ctx.SaveChanges();
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public bool Exists(int key)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.PostEntities.Where(e => e.Id == key).FirstOrDefault();
                return entity != null;
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public bool ExistsByTitle(string title)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.PostEntities.Where(e => e.Title == title).FirstOrDefault();
                return entity != null;
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public PostDTO Get(int key)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.PostEntities.Where(e => e.Id == key).FirstOrDefault() ?? throw new DataAccessException("Post does not exist");
                var domain = entity.ToDomain();
                return domain;
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public List<PostDTO> GetAll(string? title = null, PostOrderBy? orderBy = null)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var query = ctx.PostEntities
                    .Where(e => title == null || EF.Functions.Like(e.Title, title));
                query = orderBy switch
                {
                    PostOrderBy.CreatedAtASC => query.OrderBy(e => e.CreatedAt),
                    PostOrderBy.CreatedAtDESC => query.OrderByDescending(e => e.CreatedAt),
                    null => query,
                    _ => throw new NotImplementedException(),
                };
                var postEntities = query
                    .Include(e => e.Tags)
                    .ToList();
               
                var posts = new List<PostDTO>();
                foreach (var postEntity in postEntities)
                {
                    var tagDtos = postEntity.Tags.Select(t => t.ToDomain()).ToList();
                    var postDto = postEntity.ToDomain() with { Tags = tagDtos };
                    posts.Add(postDto);
                }
                return posts;

            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public PostDTO Insert(PostDTO value)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = PostEntity.FromDomain(value);
                var tagEntities = value.Tags?.Select(t => TagEntity.FromDomain(t)).ToList() ?? new List<TagEntity>();
                entity.Tags = tagEntities;
                ctx.AttachRange(tagEntities.Where(t => t.Id != 0)); // Attach new tags to avoid cascade ADD
                ctx.Add(entity);
                ctx.SaveChanges();

                var tags = entity.Tags.Select(t => t.ToDomain()).ToList();
                var newPost = entity.ToDomain() with { Tags = tags };
                return newPost;
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }

        public void Update(int key, PostDTO value)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = PostEntity.FromDomain(value);
                var tagEntities = value.Tags?.Select(t => TagEntity.FromDomain(t)).ToList() ?? new List<TagEntity>();
                entity.Tags = tagEntities;
                ctx.AttachRange(tagEntities.Where(t => t.Id != 0));
                ctx.AddRange(tagEntities.Where(t => t.Id == 0));
                ctx.Update(entity);
                ctx.SaveChanges();
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
            catch (DbUpdateException)
            {
                throw new DataAccessException("Changes could not be saved");
            }
        }
    }
}
