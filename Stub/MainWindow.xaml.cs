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
            openTextTestWindow.Click += OnOpenTextTestWindow;
            openClickTestWindow.Click += OnOpenClickTestWindow;
        }

        private static void OnOpenClickTestWindow(object sender, RoutedEventArgs e)
        {
            ClickTestWindow window = new ClickTestWindow();
            window.Show();
        }

        private static void OnOpenTextTestWindow(object sender, RoutedEventArgs e)
        {
            TextTestWindow window = new TextTestWindow();
            window.Show();
        }

        private static void OnOpenNewWindow(object sender, RoutedEventArgs e)
        {
            NewWindow window = new NewWindow();
            window.Title = "new window " + windowCount++;
            window.Show();
        }
    }
}