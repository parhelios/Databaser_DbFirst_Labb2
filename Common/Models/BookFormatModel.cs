namespace Common.Models;

public class BookFormatModel
{
    public int BookFormatId { get; set; }

    public string BookFormat1 { get; set; } = null!;

    public virtual ICollection<BookModel> Books { get; set; } = new List<BookModel>();
}