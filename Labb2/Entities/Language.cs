namespace Labb2.Entities;

public partial class Language
{
    public int LanguageId { get; set; }

    public string? BookLanguage { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
