using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Common.Models;
using Common.Services;
using StoreFront.Views;

namespace StoreFront
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        public ObservableCollection<BookModel> ShopStoreList { get; set; } = new();

        public ObservableCollection<StoreModel> StoresList { get; set; } = new();

        public ObservableCollection<BookModel> AllBooksList { get; set; } = new();

        private StoreModel _selectedStore;

        private static event Action ShopListChanged;

        private readonly BookRepository _bookRepository;

        private readonly StoreRepository _storeRepository;

        private readonly InventoryRepository _inventoryRepository;

        public StoreView()
        {
            DataContext = this;

            InitializeComponent();
            _bookRepository = new BookRepository(ApplicationContextManager.Context);
            _storeRepository = new StoreRepository(ApplicationContextManager.Context);
            _inventoryRepository = new InventoryRepository(ApplicationContextManager.Context);

            ShopListChanged += OnShopListChanged;
            EditView.BooksChanged += EditView_BooksChanged;
            EditView.AuthorsChanged += EditView_AuthorsChanged;

            var stores = _storeRepository.GetAll();
            foreach (var storeModel in stores)
            {
                StoresList.Add(storeModel);
            }

            PopulateAllBooksList();
        }

        private void EditView_AuthorsChanged()
        {
            PopulateAllBooksList();
            ShopStoreList.Clear();
            PopulateStoreList();
        }

        private void EditView_BooksChanged()
        {
            PopulateAllBooksList();
        }

        public void PopulateAllBooksList()
        {
            AllBooksList.Clear();

            var allBooks = _bookRepository.GetAllBooks();

            var allBooksSorted = allBooks.OrderBy(b => b.Title);

            foreach (var books in allBooksSorted)
            {
                AllBooksList.Add(books);
            }
        }

        private void OnShopListChanged()
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(ShopStoreList);
            view.Refresh();
        }

        private void PopulateStoreList()
        {
            if (StoreList.SelectedItem is StoreModel selectedItem)
            {
                var selectedStore = StoresList.FirstOrDefault(i => i.StoreId == selectedItem.StoreId);

                var storeInventory = _inventoryRepository.GetAllInventoriesByStoreId(selectedStore.StoreId);

                _selectedStore = selectedStore;

                if (storeInventory is null)
                {
                    return;
                }

                var allBooks = _bookRepository.GetAllBooks();

                var matchingBooks = allBooks.Where(book => storeInventory.Any(inv => book.Isbn == inv.Isbn));

                foreach (var book in matchingBooks)
                {
                    ShopStoreList.Add(book);
                }

                ShopListChanged.Invoke();
            }
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (_selectedStore is null)
            {
                return;
            }

            if (AllBooks.SelectedItem is BookModel selectedItem)
            {
                _inventoryRepository.CopyBookToSelectedInventory(selectedItem, _selectedStore);
            }

            ShopStoreList.Clear();

            PopulateStoreList();
        }

        private void RemoveBtn_OnClick(object sender, RoutedEventArgs e)
        {

            if (_selectedStore is null)
            {
                return;
            }

            if (ShopList.SelectedItem is BookModel selectedItem)
            {
                _inventoryRepository.DeleteBookFromSelectedInventory(selectedItem, _selectedStore);
            }
            ShopStoreList.Clear();

            PopulateStoreList();
        }

        private void StoreList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShopStoreList.Clear();

            _selectedStore = null;

            PopulateStoreList();
        }

        private void Uruk_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://www.youtube.com/watch?v=dQw4w9WgXcQ", UseShellExecute = true });
        }

        private void Void_OnClick(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = "https://steamuserimages-a.akamaihd.net/ugc/5063766435480454889/75BF48A3118812AE6C2E36E6F8394AADC949848E/?imw=637&imh=358&ima=fit&impolicy=Letterbox&imcolor=%23000000&letterbox=true", UseShellExecute = true });
        }
    }
}
