using BusinessLogic.Dtos;
using BusinessLogic.Exceptions;

namespace BusinessLogic.Domain
{
    internal class Post
    {
        private string title = string.Empty;
        private string description = string.Empty;
        private string author = string.Empty;
        private DateTime createdAt = DateTime.MinValue;
        private List<Tag> tags = new();

        public Post(PostDTO postData)
        {
            Update(postData);
        }

        public int ID { get; set; }
        public string Title
        {
            get => title;
            private set
            {
                if (value == string.Empty)
                {
                    throw new BusinessLogicException("Post name cannot be empty");
                }
                title = value;
            }
        }
        public string Description
        {
            get => description;
            private set => description = value;
        }
        public string Author
        {
            get => author;
            private set => author = value;
        }

        public DateTime CreatedAt
        {
            get => createdAt;
            private set => createdAt = value;
        }

        public IReadOnlyList<Tag> Tags { get => tags; }

        public void AddTag(Tag tag)
        {
            if (!HasTag(tag))
            {
                tags.Add(tag);
            }
        }

        public void RemoveTag(Tag tag)
        {
            if (HasTag(tag))
            {
                tags.RemoveAll(t => t.ID == tag.ID);
            }
        }

        public bool HasTag(Tag tag) => tags.Any(t => t.ID == tag.ID);

        public PostDTO ToDTO() => new PostDTO() with
        {
            ID = ID,
            Title = Title,
            Author = Author,
            CreatedAt = CreatedAt,
            Description = Description,
            Tags = Tags.Select(t => t.ToDTO()).ToList()
        };

        public void Update(PostDTO postData)
        {
            ID = postData.ID;
            Title = postData.Title;
            Description = postData.Description;
            Author = postData.Author;
            tags = postData.Tags?.Select(t => new Tag(t))
                .ToList() ?? tags;
        }

    }
}