namespace Labb2.Entities;

public partial class Book
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

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();

    public Language LanguageNavigation { get; set; } = null!;

    public Publisher PublisherNavigation { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
