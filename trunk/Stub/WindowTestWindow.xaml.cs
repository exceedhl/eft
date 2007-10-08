using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace stub
{
    public partial class WindowTestWindow
    {
        private static int windowCount = 0;
        public WindowTestWindow()
        {
            InitializeComponent();
            openPopupWindow.Click += OnOpenPopupWindow;
            openNewWindow.Click += OnOpenNewWindow;
            openUnresizableWindow.Click += OnOpenUnresizableWindow;
        }

        private static void OnOpenUnresizableWindow(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "Unresizable window";
            window.ResizeMode = ResizeMode.NoResize;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Show();
        }

        private static void OnOpenNewWindow(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            window.Title = "new window " + windowCount++;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.Show();
        }

        private static void OnOpenPopupWindow(object sender, RoutedEventArgs e)
        {
            Popup popupWindow = new Popup();
            popupWindow.Name = "PopupWindow";

            TextBlock popupText = new TextBlock();
            popupText.Text = "Popup Text";
            popupText.Background = Brushes.White;
            popupText.Foreground = Brushes.Black;
            popupWindow.Child = popupText;
            popupWindow.IsOpen = true;
        }
    }
}