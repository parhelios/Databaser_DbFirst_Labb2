namespace Labb2.Entities;

public partial class AuthorBirthplace
{
    public int AuthorBirthplaceId { get; set; }

    public string AuthorBirthplaceCountry { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
}
