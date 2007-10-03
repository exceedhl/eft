using System.Windows;
using System.Windows.Input;

namespace stub
{
    public partial class ClickTestWindow
    {
        public ClickTestWindow()
        {
            InitializeComponent();
            bigButton.PreviewMouseLeftButtonDown += OnClickBigButton;
            doubleClickButton.MouseDoubleClick += OnDoubleClick;
            clickWithHoldingKey.Click += OnClickWithHoldingKey;
        }

        private void OnClickWithHoldingKey(object sender, RoutedEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                log.Text = "control click";
            }
            if (Keyboard.Modifiers == ModifierKeys.Shift)
            {
                log.Text = "shift click";
            }
        }

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            log.Text = "button double clicked";
        }

        private void OnClickBigButton(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement) sender);
            log.Text = position.ToString();
        }
    }
}