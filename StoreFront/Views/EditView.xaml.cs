using System.Windows;
using Common.Services;
using System.Windows.Controls;
using Common.Models;
using System.Collections.ObjectModel;

namespace StoreFront.Views
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        public ObservableCollection<AuthorModel> AuthorList { get; set; } = new();

        private readonly AuthorRepository _authorRepository;

        private readonly BookRepository _bookRepository;

        private readonly GenreRepository _genreRepository;

        private readonly LanguageRepository _languageRepository;

        private readonly BookFormatRepository _bookFormatRepository;

        private readonly PublisherRepository _publisherRepository;

        private readonly InventoryRepository _inventoryRepository;

        public static event Action BooksChanged;

        public static event Action AuthorsChanged;

        public EditView()
        {
            InitializeComponent();

            _authorRepository = new AuthorRepository(ApplicationContextManager.Context!);
            _bookRepository = new BookRepository(ApplicationContextManager.Context!);
            _genreRepository = new GenreRepository(ApplicationContextManager.Context!);
            _languageRepository = new LanguageRepository(ApplicationContextManager.Context!);
            _bookFormatRepository = new BookFormatRepository(ApplicationContextManager.Context!);
            _publisherRepository = new PublisherRepository(ApplicationContextManager.Context!);
            _inventoryRepository = new InventoryRepository(ApplicationContextManager.Context!);

            PopulateComboboxes();

            AuthorView.AuthorListChanged += AuthorView_AuthorListChanged;
        }

        private void AuthorView_AuthorListChanged()
        {
            AuthorCb.Items.Clear();

            PopulateAuthorCombobox();
        }

        public void PopulateAuthorCombobox()
        {
            var authors = _authorRepository.GetAllAuthors()
                .OrderBy(a => a.LastName);

            foreach (var author in authors)
            {
                AuthorCb.Items.Add(author);
            }
        }

        private void PopulateComboboxes()
        {
            PopulateAuthorCombobox();

            var genres = _genreRepository.GetAllGenres()
                .OrderBy(g => g.GenreName);

            foreach (var genre in genres)
            {
                GenreCb.Items.Add(genre);
            }

            var languages = _languageRepository.GetAllLanguages();

            foreach (var language in languages)
            {
                LanguageCb.Items.Add(language);
            }

            var bookFormats = _bookFormatRepository.GetAllBookFormats();

            foreach (var bookFormat in bookFormats)
            {
                BookFormatCb.Items.Add(bookFormat);
            }

            var publishers = _publisherRepository.GetAllPublishers()
                .OrderBy(p => p.PublisherName);

            foreach (var publisher in publishers)
            {
                PublisherCb.Items.Add(publisher);
            }

            var selectBooks = _bookRepository.GetAllBooks()
                .OrderBy(b => b.Title);

            foreach (var book in selectBooks)
            {
                BookSelectCb.Items.Add(book);
            }
        }

        public void ClearAllComboBoxes()
        {
            AuthorCb.Items.Clear();

            TitleTxt.Clear();
            IsbnTxt.Clear();
            LanguageCb.Items.Clear();
            GenreCb.Items.Clear();
            BookFormatCb.Items.Clear();
            PriceTxt.Clear();
            PublisherCb.Items.Clear();
            DateOfPublishing.ClearValue(DatePicker.SelectedDateProperty);
            BookSelectCb.Items.Clear();

            PopulateComboboxes();
        }

        private void SaveNewBook_OnClick(object sender, RoutedEventArgs e)
        {
            if (IsbnTxt.Text.Length != 13)
            {
                MessageBox.Show("The ISBN-number needs to be exactly 13 digits.");
                return;
            }

            if (TitleTxt.Text == "" || IsbnTxt.Text == "")
            {
                MessageBox.Show("Additional book information required.");
                return;
            }

            int genre = 0;
            if (GenreCb.SelectedValue is GenreModel g) { genre = g.GenreId; }

            int format = 0;
            if (BookFormatCb.SelectedValue is BookFormatModel f) { format = f.BookFormatId; }

            int lang = 0;
            if (LanguageCb.SelectedValue is LanguageModel l) { lang = l.LanguageId; }

            int pub = 0;
            if (PublisherCb.SelectedValue is PublisherModel p) { pub = p.PublisherId; }

            List<AuthorModel> authorListCopy = AuthorList != null ? new List<AuthorModel>(AuthorList) : new List<AuthorModel>();

            if (authorListCopy.Count < 1)
            {
                MessageBox.Show("No author was added to the book addition.");
                return;
            }

            DateOnly.TryParse(DateOfPublishing.Text, out DateOnly output);
            _bookRepository.AddBook
                (
                    new BookModel
                    {
                        Isbn = long.Parse(IsbnTxt.Text),
                        Title = TitleTxt.Text,
                        Authors = authorListCopy,
                        Genre = genre,
                        Format = format,
                        Language = lang,
                        Price = int.Parse(PriceTxt.Text),
                        Publisher = pub,
                        DateOfPublishing = output
                    }
                );

            MessageBox.Show($"The book {TitleTxt.Text} has been added!");
            ClearAllComboBoxes();
            AuthorList.Clear();

            BooksChanged.Invoke();
        }

        private void UpdateBook_OnClick(object sender, RoutedEventArgs e)
        {
            if (BookSelectCb.SelectedValue is BookModel bm)
            {
                int genre = 0;
                if (GenreCb.SelectedValue is GenreModel g) { genre = g.GenreId; }

                int format = 0;
                if (BookFormatCb.SelectedValue is BookFormatModel f) { format = f.BookFormatId; }

                int lang = 0;
                if (LanguageCb.SelectedValue is LanguageModel l) { lang = l.LanguageId; }

                int pub = 0;
                if (PublisherCb.SelectedValue is PublisherModel p) { pub = p.PublisherId; }

                List<AuthorModel> authorListCopy = AuthorList != null ? new List<AuthorModel>(AuthorList) : new List<AuthorModel>();

                if (!DateOnly.TryParse(DateOfPublishing.Text, out DateOnly output))
                {
                    return;
                }

                _bookRepository.UpdateBook
                (
                    new BookModel
                    {
                        Isbn = long.Parse(IsbnTxt.Text),
                        Title = TitleTxt.Text,
                        Authors = authorListCopy,
                        Genre = genre,
                        Format = format,
                        Language = lang,
                        Price = int.Parse(PriceTxt.Text),
                        Publisher = pub,
                        DateOfPublishing = output
                    }
                );
                MessageBox.Show($"The book {TitleTxt.Text} has been updated!");
                ClearAllComboBoxes();
                AuthorList.Clear();
                BooksChanged.Invoke();
                AuthorsChanged.Invoke();
            }
        }

        private void BookSelectCb_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorList.Clear();

            if (BookSelectCb.SelectedValue is BookModel bm)
            {
                TitleTxt.Text = bm.Title;
                IsbnTxt.Text = bm.Isbn.ToString();
                LanguageCb.Text = bm.LanguageNavigation.BookLanguage;
                GenreCb.Text = bm.GenreNavigation.GenreName;
                PriceTxt.Text = bm.Price.ToString();
                BookFormatCb.Text = bm.FormatNavigation.BookFormat1;
                PublisherCb.Text = bm.PublisherNavigation.PublisherName;
                DateOfPublishing.Text = bm.DateOfPublishing.ToString();

                foreach (var author in bm.Authors)
                {
                    AuthorList.Add(author);
                }
                AuthorListView.ItemsSource = AuthorList;
            }
        }

        private void AddAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (AuthorCb.SelectedValue is AuthorModel author)
            {
                AuthorList.Add(author);

                AuthorListView.ItemsSource = AuthorList;
            }
        }

        private void ClearAuthorDataBtn_OnClick(object sender, RoutedEventArgs e)
        {
            ClearAllComboBoxes();
        }

        private void DeleteBook_OnClick(object sender, RoutedEventArgs e)
        {

            if (BookSelectCb.SelectedValue is BookModel bm)
            {
                if (_inventoryRepository.CheckIfBookIsInInventory(bm))
                {
                    MessageBox.Show("A book can not be deleted if it's contained in a store's inventory.");
                    return;
                }

                string message = "Are you sure?";
                string caption = "Book deletion";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

                if (result.Equals(MessageBoxResult.No))
                {
                    return;
                }

                _bookRepository.DeleteBook(bm);

                MessageBox.Show($"The book titled {bm.Title} has been removed from the database.");
                ClearAllComboBoxes();
                AuthorList.Clear();
                BooksChanged.Invoke();
            }
        }

        private void RemoveAuthorFromList_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthorListView.SelectedValue is AuthorModel am)
            {
                AuthorList.Remove(am);

                AuthorListView.ItemsSource = AuthorList;
            }
        }
    }
}
