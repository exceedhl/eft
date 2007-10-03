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
            pressButton.PreviewMouseDown += OnMouseDown;
            releaseButton.PreviewMouseUp += OnMouseUp;

            pressButtonWithCount.PreviewMouseDown += OnMouseDownWithCount;
        }

        private void OnMouseDownWithCount(object sender, MouseButtonEventArgs e)
        {
            LogMessage(e);
            log.Text += " " + e.ClickCount;
        }

        private void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            LogMessage(e);
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            LogMessage(e);
        }

        private void LogMessage(MouseButtonEventArgs e)
        {
            log.Text = "";

            if (Keyboard.Modifiers != ModifierKeys.None)
            {
                log.Text += Keyboard.Modifiers + " ";
            }
            log.Text += e.ChangedButton.ToString();
            log.Text += " " + e.ButtonState;
        }

        private void OnClickBigButton(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((IInputElement) sender);
            log.Text = position.ToString();
        }
    }
}