using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Dtos
{
    public readonly struct TagDTO
    {
        public TagDTO() { }

        public readonly int ID { get; init; } = 0;
        public readonly string Name { get; init; } = string.Empty;
    }
}
