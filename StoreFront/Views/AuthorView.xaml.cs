using Common.Models;
using Common.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace StoreFront.Views
{
    /// <summary>
    /// Interaction logic for AuthorView.xaml
    /// </summary>
    public partial class AuthorView : UserControl
    {
        private readonly AuthorRepository _authorRepository;

        public static event Action AuthorListChanged;

        public ObservableCollection<AuthorModel> AuthorList { get; set; } = new();

        public AuthorView()
        {
            DataContext = this;

            InitializeComponent();

            AuthorListChanged += AuthorView_AuthorListChanged;

            _authorRepository = new AuthorRepository(ApplicationContextManager.Context);

            PopulateAuthorList();
        }

        private void PopulateAuthorList()
        {
            var authors = _authorRepository.GetAllAuthors()
                .OrderBy(a => a.LastName);

            foreach (var author in authors)
            {
                AuthorList.Add(author);
            }

            AuthorListChanged.Invoke();
        }

        private void ClearTextBoxes()
        {
            AuthorFirstNameTxt.Clear();
            AuthorLastNameTxt.Clear();
            AuthorDateOfBirthTxt.ClearValue(DatePicker.SelectedDateProperty);
        }

        private void AuthorView_AuthorListChanged()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(AuthorList);
            view.Refresh();
        }

        private void AuthorList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AuthorDisplayList.SelectedItem is AuthorModel selected)
            {
                AuthorFirstNameTxt.Text = selected.FirstName;
                AuthorLastNameTxt.Text = selected.LastName;
                AuthorDateOfBirthTxt.Text = selected.DateOfBirth.ToString();
            }
        }

        private void SaveNewAuthor_OnClick(object sender, RoutedEventArgs e)
        {

            if (AuthorFirstNameTxt.Text == "" || AuthorLastNameTxt.Text == "")
            {
                MessageBox.Show("Additional author information required.");
                return;
            }

            DateOnly.TryParse(AuthorDateOfBirthTxt.Text, out var authorBirth);

            var allAuthors = _authorRepository.GetAllAuthors();
            List<int> allIds = new List<int>();
            foreach (var author in allAuthors)
            {
                allIds.Add(author.AuthorId);
            }

            _authorRepository.AddAuthor
                (
                    new AuthorModel
                    {
                        AuthorId = allIds.Max() +1,
                        FirstName = AuthorFirstNameTxt.Text,
                        LastName = AuthorLastNameTxt.Text,
                        AuthorOrigin = 1, //Jag orkar inte bråka med detta
                        DateOfBirth = authorBirth,
                        DateOfDeath = DateOnly.MaxValue //Orkar inte här heller, sorry
                    }
                );

            AuthorList.Clear();
            PopulateAuthorList();

            MessageBox.Show($"The author {AuthorFirstNameTxt.Text} {AuthorLastNameTxt.Text} has been added to the database.");

            ClearTextBoxes();
            AuthorListChanged.Invoke();
        }

        private void UpdateAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = "";

            if (AuthorDisplayList.SelectedItem is AuthorModel selected)
            {
                DateOnly.TryParse(AuthorDateOfBirthTxt.Text, out var authorBirth);
                 
                _authorRepository.UpdateAuthor
                    (
                        new AuthorModel
                        {
                            AuthorId = selected.AuthorId,
                            FirstName = AuthorFirstNameTxt.Text,
                            LastName = AuthorLastNameTxt.Text,
                            AuthorOrigin = 1, //Jag orkar inte bråka med detta
                            DateOfBirth = authorBirth,
                            DateOfDeath = DateOnly.MaxValue //Orkar inte här heller, sorry
                        }
                        );

                selectedAuthor = $"{selected.FirstName} {selected.LastName}";

                AuthorList.Clear();
                PopulateAuthorList();
                ClearTextBoxes();

                MessageBox.Show($"The author {selectedAuthor} has been updated.");
            }
        }

        private void DeleteAuthor_OnClick(object sender, RoutedEventArgs e)
        {
            var selectedAuthor = "";

            if (AuthorDisplayList.SelectedItem is AuthorModel selected)
            {
                if (_authorRepository.CheckIfAuthorIsAddedToBooks(selected))
                {
                    MessageBox.Show("An author can not be deleted if it's linked to a book.");
                    return;
                }

                string message = "Are you sure?";
                string caption = "Author deletion";
                var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo);

                if (result.Equals(MessageBoxResult.No))
                {
                    return;
                }

                _authorRepository.DeleteAuthor(selected.AuthorId);

                selectedAuthor = $"{selected.FirstName} {selected.LastName}";

                AuthorList.Clear();
                PopulateAuthorList();
                ClearTextBoxes();

                MessageBox.Show($"The author {selectedAuthor} has been deleted from the database.");
            }
        }
    }
}
