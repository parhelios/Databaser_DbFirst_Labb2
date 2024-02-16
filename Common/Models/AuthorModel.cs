using Labb2.Entities;

namespace Common.Models;

public class AuthorModel
{
    public int AuthorId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public DateOnly? DateOfDeath { get; set; }

    public int AuthorOrigin { get; set; }

    public virtual AuthorBirthplace AuthorOriginNavigation { get; set; } = null!;

    public virtual ICollection<BookModel> Isbns { get; set; } = new List<BookModel>();

    public string AuthorFullName
    {
        get
        {
            return string.Concat(FirstName, " ",LastName);
        }
    }
}