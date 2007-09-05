using System.Windows.Automation;

namespace eft
{
    public class Window
    {
        private readonly AutomationElement window;

        public Window(AutomationElement window)
        {
            this.window = window;
        }

        public string Title
        {
            get { return window.Current.Name; }
        }
    }
}