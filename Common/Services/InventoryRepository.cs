using Common.Models;
using Labb2;
using Labb2.Entities;
using StoreFront;

namespace Common.Services;

public class InventoryRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public InventoryRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public InventoryRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public void CopyBookToSelectedInventory(BookModel book, StoreModel storeId)
    {
        bool bookExistsInInventory = _context.Inventories
            .Any(i => i.StoreId == storeId.StoreId && i.Isbn == book.Isbn);

        if (bookExistsInInventory)
        {
            return;
        }

        _context.Inventories.Add
        (new Inventory
            {
                Isbn = book.Isbn,
                StoreId = storeId.StoreId,
                Amount = 1,
        }
        );

        _context.SaveChanges();
    }

    public InventoryModel GetInventoryByStoreId(int storeId)
    {
        var store = _context.Inventories.Find(storeId);

        return new InventoryModel
        {
            Isbn = store.Isbn,
            StoreId = store.StoreId,
            Store = store.Store,
            Amount = store.Amount
        };
    }

    public bool CheckIfBookIsInInventory(BookModel book)
    {
        var inInventory = _context.Inventories.Any(i => i.Isbn == book.Isbn);

        return inInventory;
    }

    public List<InventoryModel> GetAll()
    {
        return _context.Inventories.Select(
            inventory => new InventoryModel
            {
                Isbn = inventory.Isbn,
                StoreId = inventory.StoreId,
                Store = inventory.Store,
                Amount = inventory.Amount,
            }
        ).ToList();
    }

    public List<InventoryModel> GetAllInventoriesByStoreId(int storeId)
    {
        var selectedInventories = _context.Inventories.Where(i => i.StoreId == storeId);

        return selectedInventories.Select(
            inventory => new InventoryModel
            {
                Isbn = inventory.Isbn,
                StoreId = inventory.StoreId,
                Store = inventory.Store,
                Amount = inventory.Amount,
            }
        ).ToList();
    }

    public void Delete(InventoryModel storeId)
    {
        var inventoryItem = _context.Inventories.Find(storeId);
        _context.Inventories.Remove(inventoryItem);
        _context.SaveChanges();
    }

    public void DeleteBookFromSelectedInventory(BookModel book, StoreModel storeId)
    {
        bool bookExistsInInventory = _context.Inventories
            .Any(i => i.StoreId == storeId.StoreId && i.Isbn == book.Isbn);

        if (!bookExistsInInventory)
        {
            return;
        }

        var removeBook = _context.Inventories.Find(storeId.StoreId, book.Isbn);
        _context.Inventories.Remove(removeBook);
        _context.SaveChanges();
    }
}