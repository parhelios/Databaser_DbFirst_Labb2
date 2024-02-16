namespace Common.Models;

public class StoreModel
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string StoreAddress { get; set; } = null!;

    public virtual ICollection<InventoryModel> Inventories { get; set; } = new List<InventoryModel>();
}