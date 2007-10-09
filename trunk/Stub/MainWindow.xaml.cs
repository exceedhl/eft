using System.Windows;

namespace stub
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            openWindowTestWindow.Click += OnOpenNewWindow;
            openTextTestWindow.Click += OnOpenTextTestWindow;
            openClickTestWindow.Click += OnOpenClickTestWindow;
            openWaitForConditionTestWindow.Click += OnOpenWaitForConditionTestWindow;
        }

        private static void OnOpenWaitForConditionTestWindow(object sender, RoutedEventArgs e)
        {
            WaitForConditionTestWindow window = new WaitForConditionTestWindow();
            window.Show();
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
            WindowTestWindow window = new WindowTestWindow();
            window.Show();
        }
    }
}