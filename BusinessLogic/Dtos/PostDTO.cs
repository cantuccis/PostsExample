using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public readonly struct PostDTO
    {
        public PostDTO()
        {
        }

        public readonly int ID { get; init; } = 0;
        public readonly string Title { get; init; } = string.Empty;
        public readonly string Description { get; init; } = string.Empty;
        public readonly string Author { get; init; } = string.Empty;
        public readonly DateTime CreatedAt { get; init; } = DateTime.Now;
        public readonly IReadOnlyList<TagDTO>? Tags { get; init; }
    }
}
