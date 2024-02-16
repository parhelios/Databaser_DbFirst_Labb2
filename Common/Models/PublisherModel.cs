namespace Common.Models;

public class PublisherModel
{
    public int PublisherId { get; set; }

    public string PublisherName { get; set; } = null!;

    public string PublisherCountry { get; set; } = null!;

    public virtual ICollection<BookModel> Books { get; set; } = new List<BookModel>();
}