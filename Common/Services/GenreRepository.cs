using Common.Models;
using Labb2;
using StoreFront;

namespace Common.Services;

public class GenreRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public GenreRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public GenreRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public List<GenreModel> GetAllGenres()
    {
        return _context.Genres.Select(g => new GenreModel
        {
            GenreId = g.GenreId,
            GenreName = g.GenreName,

        }).ToList();
    }
}