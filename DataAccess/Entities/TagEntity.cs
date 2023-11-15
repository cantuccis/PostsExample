using BusinessLogic.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class TagEntity
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;

        public ICollection<PostEntity> Posts { get; set; } = new List<PostEntity>();

        public TagDTO ToDomain() => new()
        {
            ID = Id,
            Name = Name,
        };
        public static TagEntity FromDomain(TagDTO dto) => new()
        {
            Id = dto.ID,
            Name = dto.Name,
        };

    }
}
