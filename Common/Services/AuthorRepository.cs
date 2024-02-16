using Common.Models;
using Labb2;
using Labb2.Entities;
using StoreFront;

namespace Common.Services;

public class AuthorRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public AuthorRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public AuthorRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public bool CheckIfAuthorIsAddedToBooks(AuthorModel author)
    {
        var hasBook = _context.Books.Any(b => b.Authors.Any(a => a.AuthorId == author.AuthorId));

        return hasBook;
    }

    public void AddAuthor(AuthorModel author)
    {
        _context.Authors.Add
        (
            new Author
            {
                AuthorId = author.AuthorId,
                FirstName = author.FirstName,
                LastName = author.LastName,
                AuthorOrigin = author.AuthorOrigin,
                DateOfBirth = author.DateOfBirth,
                DateOfDeath = author.DateOfDeath
            }
        );
        _context.SaveChanges();
    }

    public List<AuthorModel> GetAllAuthors()
    {
        return _context.Authors.Select(a => new AuthorModel
        {
            AuthorId = a.AuthorId,
            FirstName = a.FirstName,
            LastName = a.LastName,
            AuthorOrigin = a.AuthorOrigin,
            DateOfBirth = a.DateOfBirth,
            DateOfDeath = a.DateOfDeath

        }).ToList();
    }

    public void UpdateAuthor(AuthorModel author)
    {
        var authorToUpdate = _context.Authors.FirstOrDefault(a => a.AuthorId == author.AuthorId);

        if (authorToUpdate is null)
        {
            return;
        }

        authorToUpdate.FirstName = author.FirstName;
        authorToUpdate.LastName = author.LastName;
        authorToUpdate.AuthorOrigin = author.AuthorOrigin;
        authorToUpdate.DateOfBirth = author.DateOfBirth;
        authorToUpdate.DateOfDeath = author.DateOfDeath;

        _context.SaveChanges();
    }

    public void DeleteAuthor(int id)
    {
        var author = _context.Authors.Find(id);

        _context.Authors.Remove(author);

        _context.SaveChanges();
    }
}