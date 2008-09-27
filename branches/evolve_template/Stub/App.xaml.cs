using System.Windows;

namespace stub
{
    public partial class App
    {
        private void AppStartup(object sender, StartupEventArgs args)
        {
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}