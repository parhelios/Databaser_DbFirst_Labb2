using Labb2.Entities;

namespace Common.Models
{
    public class BookModel
    {
        public long Isbn { get; set; }

        public string Title { get; set; } = null!;

        public int Language { get; set; }

        public int Genre { get; set; }

        public int Price { get; set; }

        public DateOnly DateOfPublishing { get; set; }

        public DateOnly? DateOfFirstPublishing { get; set; }

        public int Format { get; set; }

        public int Publisher { get; set; }

        public BookFormat FormatNavigation { get; set; } = null!;

        public Genre GenreNavigation { get; set; } = null!;

        public ICollection<InventoryModel> Inventories { get; set; } = new List<InventoryModel>();

        public Language LanguageNavigation { get; set; } = null!;

        public Publisher PublisherNavigation { get; set; } = null!;

        public ICollection<AuthorModel> Authors { get; set; } = new List<AuthorModel>();

        public string AllAuthors
        {
            get
            {
                return string.Join(", ", Authors.Select(author => $"{author.FirstName} {author.LastName}"));
            }
        }
    }
}
