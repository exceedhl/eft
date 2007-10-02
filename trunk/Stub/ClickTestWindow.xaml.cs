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