namespace Common.Models;

public class GenreModel
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<BookModel> Books { get; set; } = new List<BookModel>();
}