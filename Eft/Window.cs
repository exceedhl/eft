using System.Windows.Automation;

namespace Eft
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