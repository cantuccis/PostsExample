using DataAccess.Entities;
using DataAccess.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class EFContext : DbContext
    {
        public DbSet<TagEntity> TagEntities { get; set; }
        public DbSet<PostEntity> PostEntities { get; set; }

        public IConfiguration? Config { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Config ??= new ConfigurationBuilder()
                .AddJsonFile($@"{Directory.GetCurrentDirectory()}/appsettings.json")
                .Build();

            var parsed = bool.TryParse(Config?.GetSection("Env")["Testing"], out bool isTesting);
            isTesting = parsed && isTesting;

            string connectionString = Config?
                .GetConnectionString(isTesting ? "PostsDBTest" : "PostsDB") 
                ?? throw new DataAccessException("Connection string not found");
            
            optionsBuilder.UseSqlServer(connectionString);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PostEntity>()
            .HasMany(e => e.Tags)
            .WithMany(e => e.Posts);
        }
    }
}
