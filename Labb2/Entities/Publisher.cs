namespace Labb2.Entities;

public partial class Publisher
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string PublisherCountry { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
