namespace Labb2.Entities;

public partial class BookFormat
{
    public int BookFormatId { get; set; }

    public string BookFormat1 { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
