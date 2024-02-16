using System.Windows;

namespace StoreFront
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
        
            InitializeComponent();
            StoreTab.Visibility = Visibility.Visible;
            EditTab.Visibility = Visibility.Visible;
            AuthorTab.Visibility = Visibility.Visible;
            StoreTab.IsSelected = true;
        }
    }
}
