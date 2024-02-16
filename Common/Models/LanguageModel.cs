namespace Common.Models;

public class LanguageModel
{
    public int LanguageId { get; set; }

    public string? BookLanguage { get; set; }

    public virtual ICollection<BookModel> Books { get; set; } = new List<BookModel>();
}