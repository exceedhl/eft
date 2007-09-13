using System.Windows;

namespace stub
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            button.Click += button_Click;
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            new SubWindow().Show();
        }
    }
}