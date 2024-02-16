using Labb2.Entities;

namespace Common.Models;

public class InventoryModel
{
    public int StoreId { get; set; }

    public long Isbn { get; set; }

    public int Amount { get; set; }

    public Book IsbnNavigation { get; set; } = null!;

    public Store Store { get; set; } = null!;
}