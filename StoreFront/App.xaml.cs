using System.Windows;
using Labb2;

namespace StoreFront
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ApplicationContextManager.Initialize(new Labb1TobiasLindbergContext());
        }

        protected override void OnExit(ExitEventArgs e)
        {
            ApplicationContextManager.Context?.Dispose();
        }
    }
}
