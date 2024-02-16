namespace Labb2.Entities;

public partial class VAllInfo
{
    public string Title { get; set; } = null!;

    public string AuthorName { get; set; } = null!;

    public string? Language { get; set; }

    public string Genre { get; set; } = null!;

    public string Format { get; set; } = null!;

    public DateOnly AuthorDateOfBirth { get; set; }

    public string AuthorOrigin { get; set; } = null!;

    public string Publisher { get; set; } = null!;

    public long Isbn { get; set; }
}
