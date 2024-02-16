using Common.Models;
using Labb2;
using Labb2.Entities;
using StoreFront;

namespace Common.Services;

public class StoreRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public StoreRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public StoreRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public StoreModel GetStoreById(string id)
    {
        var store = _context.Stores.Find(id);

        return new StoreModel
        {
            StoreId = store.StoreId,
            StoreName = store.StoreName,
            StoreAddress = store.StoreAddress,
            Inventories = store.Inventories.Select(p => new InventoryModel
                {
                    StoreId = p.StoreId,
                    Isbn = p.Isbn,
                    Amount = p.Amount
                }

            ).ToList()
        };
    }

    public List<StoreModel> GetAll()
    {
        return _context.Stores.Select(
            store => new StoreModel
            {
                StoreId = store.StoreId,
                StoreName = store.StoreName,
                StoreAddress = store.StoreAddress,
                Inventories = store.Inventories.Select(p => new InventoryModel
                    {
                        StoreId = p.StoreId,
                        Isbn = p.Isbn,
                        Amount = p.Amount
                    }
                ).ToList()
            }
        ).ToList();
    }

    public void Add(StoreModel store)
    {
        _context.Stores.Add
        (new Store
        {
            StoreName = store.StoreName,
            StoreAddress = store.StoreAddress
        }
        );
        _context.SaveChanges();
    }

    public void Update(StoreModel store)
    {
        _context.Stores.Update
        (new Store
            {
                StoreName = store.StoreName,
                StoreAddress = store.StoreAddress
            }
        );
        _context.SaveChanges();
    }

    public void Delete(string id)
    {
        var store = _context.Stores.Find(id);
        _context.Stores.Remove(store);
        _context.SaveChanges();
    }
}