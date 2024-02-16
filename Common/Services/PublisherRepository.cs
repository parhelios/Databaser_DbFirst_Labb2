using Common.Models;
using Labb2;
using StoreFront;

namespace Common.Services;

public class PublisherRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public PublisherRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public PublisherRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public List<PublisherModel> GetAllPublishers()
    {
        return _context.Publishers.Select(p => new PublisherModel()
        {
            PublisherId = p.PublisherId,
            PublisherName = p.PublisherName,
            PublisherCountry = p.PublisherCountry

        }).ToList();
    }
}