using Common.Models;
using Labb2;
using StoreFront;

namespace Common.Services;

public class LanguageRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public LanguageRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public LanguageRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public List<LanguageModel> GetAllLanguages()
    {
        return _context.Languages.Select(l => new LanguageModel
        {
            LanguageId = l.LanguageId,
            BookLanguage = l.BookLanguage,

        }).ToList();
    }
}