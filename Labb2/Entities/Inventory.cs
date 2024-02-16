namespace Labb2.Entities;

public partial class Inventory
{
    public int StoreId { get; set; }

    public long Isbn { get; set; }

    public int Amount { get; set; }

    public Book IsbnNavigation { get; set; } = null!;

    public Store Store { get; set; } = null!;
}
