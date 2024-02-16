using Common.Models;
using Labb2;
using StoreFront;

namespace Common.Services;

public class BookFormatRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public BookFormatRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public BookFormatRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public List<BookFormatModel> GetAllBookFormats()
    {
        return _context.BookFormats.Select(bf => new BookFormatModel()
        {
            BookFormatId = bf.BookFormatId,
            BookFormat1 = bf.BookFormat1,

        }).ToList();
    }
}