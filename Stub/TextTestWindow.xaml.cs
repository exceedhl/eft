using System.Collections.Generic;

namespace stub
{
    public partial class TextTestWindow
    {
        public TextTestWindow()
        {
            InitializeComponent();
            List<ListViewRow> items = new List<ListViewRow>();
            items.Add(new ListViewRow("0, 0", "0, 1"));
            items.Add(new ListViewRow("1, 0", "1, 1"));
            listView.ItemsSource = items;
        }
    }

    internal class ListViewRow
    {
        private readonly string col0;
        private readonly string col1;

        public ListViewRow(string col0, string col1)
        {
            this.col0 = col0;
            this.col1 = col1;
        }

        public string Col0
        {
            get { return col0; }
        }

        public string Col1
        {
            get { return col1; }
        }
    }
}