using BusinessLogic.Dtos;
using BusinessLogic.Interfaces;
using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class EFTagsRepository : ITagsRepository
    {
        private IConfiguration config;
        public EFTagsRepository(IConfiguration config)
        {
            this.config = config;
        }

        public void Delete(int key)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.TagEntities.Where(e => e.Id == key).FirstOrDefault() ?? throw new DataAccessException("Tag does not exist");
                ctx.TagEntities.Remove(entity);
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
                var entity = ctx.TagEntities.Where(e => e.Id == key).FirstOrDefault();
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

        public TagDTO Get(int key)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = ctx.TagEntities.Where(e => e.Id == key).FirstOrDefault() ?? throw new DataAccessException("Tag does not exist");
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

        public List<TagDTO> GetAll()
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                return ctx.TagEntities.ToList().Select(t => t.ToDomain()).ToList();
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
        }

        public TagDTO Insert(TagDTO value)
        {
            try
            {
                using var ctx = new EFContext() { Config = config };
                var entity = TagEntity.FromDomain(value);
                ctx.TagEntities.Add(entity);
                ctx.SaveChanges();
                return entity.ToDomain();
            }
            catch (SqlException)
            {
                throw new DataAccessException("SQL Server error");
            }
        }

        public void Update(int key, TagDTO value)
        {
            throw new NotImplementedException();
        }
    }
}
