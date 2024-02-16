using Common.Models;
using Labb2;
using Labb2.Entities;
using Microsoft.EntityFrameworkCore;
using StoreFront;

namespace Common.Services;

public class BookRepository
{
    private readonly Labb1TobiasLindbergContext _context;

    public BookRepository()
    {
        _context = ApplicationContextManager.Context;
    }

    public BookRepository(Labb1TobiasLindbergContext context)
    {
        _context = context;
    }

    public void AddBook(BookModel book)
    {
        var bookToAdd = new Book();
        foreach (var author in book.Authors)
        {
            bookToAdd.Authors.Add(_context.Authors
                .FirstOrDefault(d =>
                    d.FirstName == author.FirstName && d.LastName == author.LastName));
        }

        _context.Add
        (
            new Book
            {
                Isbn = book.Isbn,
                Title = book.Title,
                Authors = bookToAdd.Authors,
                Genre = book.Genre,
                Format = book.Format,
                Language = book.Language,
                Price = book.Price,
                Publisher = book.Publisher,
                DateOfPublishing = book.DateOfPublishing,
                DateOfFirstPublishing = book.DateOfFirstPublishing
            }
        );

        _context.SaveChanges();
    }

    public List<BookModel> GetAllBooks()
    {
        return _context.Books
            .Include(book => book.GenreNavigation)
            .Include(book => book.LanguageNavigation)
            .Include(book => book.PublisherNavigation)
            .Include(book => book.FormatNavigation)
            .Select(book => new BookModel
            {
                Isbn = book.Isbn,
                Title = book.Title,
                Authors = book.Authors.Select(a => new AuthorModel
                {
                    AuthorId = a.AuthorId,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    AuthorOrigin = a.AuthorOrigin,
                    DateOfBirth = a.DateOfBirth,
                    DateOfDeath = a.DateOfDeath
                }
                ).ToList(),
                Genre = book.Genre,
                GenreNavigation = new Genre
                {
                    GenreId = book.GenreNavigation.GenreId,
                    GenreName = book.GenreNavigation.GenreName
                },
                Format = book.Format,
                FormatNavigation = new BookFormat
                {
                    BookFormatId = book.FormatNavigation.BookFormatId,
                    BookFormat1 = book.FormatNavigation.BookFormat1
                },
                Language = book.Language,
                LanguageNavigation = new Language
                {
                    LanguageId = book.LanguageNavigation.LanguageId,
                    BookLanguage = book.LanguageNavigation.BookLanguage
                },
                Price = book.Price,
                Publisher = book.Publisher,
                PublisherNavigation = new Publisher
                {
                    PublisherId = book.PublisherNavigation.PublisherId,
                    PublisherName = book.PublisherNavigation.PublisherName,
                    PublisherCountry = book.PublisherNavigation.PublisherCountry
                },
                DateOfPublishing = book.DateOfPublishing,
                DateOfFirstPublishing = book.DateOfFirstPublishing

            })
            .ToList();
    }


    public BookModel GetBookByIsbn(long id)
    {
        var book = _context.Books
            .Include(book => book.GenreNavigation)
            .Include(book => book.LanguageNavigation)
            .Include(book => book.PublisherNavigation)
            .Include(book => book.FormatNavigation).Include(book => book.Authors)
            .FirstOrDefault(b => b.Isbn == id);

        if (book is null)
        {
            return null;
        }

        return new BookModel
        {
            Isbn = book.Isbn,
            Title = book.Title,
            Authors = book.Authors.Select(a => new AuthorModel
            {
                AuthorId = a.AuthorId,
                FirstName = a.FirstName,
                LastName = a.LastName,
                AuthorOrigin = a.AuthorOrigin,
                DateOfBirth = a.DateOfBirth,
                DateOfDeath = a.DateOfDeath
            }
            ).ToList(),
            Genre = book.Genre,
            GenreNavigation = new Genre
            {
                GenreId = book.GenreNavigation.GenreId,
                GenreName = book.GenreNavigation.GenreName
            },
            Format = book.Format,
            FormatNavigation = new BookFormat
            {
                BookFormatId = book.FormatNavigation.BookFormatId,
                BookFormat1 = book.FormatNavigation.BookFormat1
            },
            Language = book.Language,
            LanguageNavigation = new Language
            {
                LanguageId = book.LanguageNavigation.LanguageId,
                BookLanguage = book.LanguageNavigation.BookLanguage
            },
            Price = book.Price,
            Publisher = book.Publisher,
            PublisherNavigation = new Publisher
            {
                PublisherId = book.PublisherNavigation.PublisherId,
                PublisherName = book.PublisherNavigation.PublisherName,
                PublisherCountry = book.PublisherNavigation.PublisherCountry
            },
            DateOfPublishing = book.DateOfPublishing,
            DateOfFirstPublishing = book.DateOfFirstPublishing
        };
    }

    public void UpdateBook(BookModel book)
    {
        var bookToUpdate = _context.Books.Include(b => b.Authors).SingleOrDefault(b => b.Isbn == book.Isbn);

        if (bookToUpdate is null)
        {
            return;
        }

        bookToUpdate.Authors.Clear();

        foreach (var author in book.Authors)
        {
            bookToUpdate.Authors.Add(_context.Authors
                .FirstOrDefault(d =>
                    d.FirstName == author.FirstName && d.LastName == author.LastName));
        }

        bookToUpdate.Isbn = book.Isbn;
        bookToUpdate.Title = book.Title;
        bookToUpdate.Genre = book.Genre;
        bookToUpdate.Format = book.Format;
        bookToUpdate.Language = book.Language;
        bookToUpdate.Price = book.Price;
        bookToUpdate.Publisher = book.Publisher;
        bookToUpdate.DateOfPublishing = book.DateOfPublishing;
        bookToUpdate.DateOfFirstPublishing = book.DateOfFirstPublishing;

        _context.SaveChanges();
    }

    public void DeleteBook(BookModel bookModel)
    {

        var book = _context.Books.Find(bookModel.Isbn);

        var dbAuthors = _context.Authors
            .Include(d => d.Isbns)
            .ThenInclude(d => d.Authors)
            .Where(d => d.Isbns
                .Any(t => t.Title == book.Title));

        book.Authors.Clear();


        foreach (var author in dbAuthors)
        {
            var bookToRemove = author.Isbns.FirstOrDefault(b => b.Title == book.Title);
            author.Isbns.Remove(bookToRemove);
        }

        _context.Books.Remove(book);
        _context.SaveChanges();
    }
}