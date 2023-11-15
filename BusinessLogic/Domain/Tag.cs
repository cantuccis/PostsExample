using BusinessLogic.Dtos;
using BusinessLogic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Domain
{
    internal class Tag
    {
        private string name = string.Empty;

        public int ID { get; set; } = 0;
        public string Name
        {
            get => name; private set
            {
                if(value  == string.Empty)
                {
                    throw new BusinessLogicException("Name cannot be empty");
                }
                name = value;
            }
        }
        public Tag(TagDTO tagData)
        {
            Update(tagData);
        }

        public override string ToString() => Name;

        public TagDTO ToDTO() => new TagDTO() with { 
            ID = ID,
            Name = Name
        };

        public void Update(TagDTO tagData)
        {
            ID = tagData.ID;
            Name = tagData.Name;
        }
        
    }
}
