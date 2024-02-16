using Common.Services;

var bookRepository = new BookRepository();

//bookRepository.AddBook
//(
//    new BookModel
//    {
//        Title = "Äldreomsorgen i övre Kågedalen",
//        Isbn = 9789185000951,
//        Genre = 1,
//        DateOfPublishing = new DateOnly(2012, 7, 3),
//        Language = 2,
//        Publisher = 1,
//        Price = 409,
//        Format = 2,
//        Authors = new List<Author>(13)
//    }
//);




//var allBooks = bookRepository.GetAllBooks();

//foreach (var book in allBooks)  
//{
//    var authorNames = string.Join(", ", book.Authors.Select(author => $"{author.FirstName} {author.LastName}"));

//    Console.WriteLine($"{book.Title}, {authorNames}, {book.Price} SEK");
//}



//var bookSearch = bookRepository.GetBookByIsbn(9780575081567);

//var authorNames = string.Join(", ", bookSearch.Authors.Select(author => $"{author.FirstName} {author.LastName}"));

//Console.WriteLine($"{bookSearch.Title}, {authorNames}, {bookSearch.Price} SEK");




//var bookRepository1 = new BookRepository();

//var updateBook = bookRepository1.GetBookByIsbn(9789185000951);
//updateBook.Title = "Äldreomsorgen i övre Kågedalen";
//bookRepository.UpdateBook(updateBook);

////TODO Denna fungerar bara om man lägger till alla kolumn-parametrar
//bookRepository.UpdateBook
//    (
//        new BookModel
//        {
//            Isbn = 9789185000951,
//            Title = "bananer i pyjamas"
//        }
        
//    );