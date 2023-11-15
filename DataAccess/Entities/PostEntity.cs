using BusinessLogic.Dtos;

namespace DataAccess.Entities
{
    public class PostEntity
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public List<TagEntity> Tags { get; set; } = new();
        
        public PostDTO ToDomain() => new()
        {
            ID = Id,
            Description = Description,
            Author = Author,
            Title = Title,
            CreatedAt = CreatedAt,
        };
        public static PostEntity FromDomain(PostDTO dto) => new()
        {
            Id = dto.ID,
            Author = dto.Author,
            Title = dto.Title,
            CreatedAt = dto.CreatedAt,
            Description = dto.Description
        };

    }
}
