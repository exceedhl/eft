using System.Windows;

namespace stub
{
    public partial class MainWindow
    {
        private static int windowCount = 0;

        public MainWindow()
        {
            InitializeComponent();
            openNewWindow.Click += OnOpenNewWindow;
        }

        private static void OnOpenNewWindow(object sender, RoutedEventArgs e)
        {
            NewWindow window = new NewWindow();
            window.Title = "new window " + windowCount++;
            window.Show();
        }
    }
}