using Eft.Exception;
using Eft.Provider;

namespace Eft.Elements
{
    public class Window : Element
    {
        public Window(IAutomationProvider automationProvider)
            : base(automationProvider)
        {
            if (!automationProvider.IsWindow)
            {
                throw new ControlTypeConversionException("This element is not a Window");
            }
        }

        public WindowState WindowState
        {
            get { return provider.WindowState; }
        }

        public void Maximize()
        {
            provider.ChangeWindowState(WindowState.Maximized);
        }

        public void Minimize()
        {
            provider.ChangeWindowState(WindowState.Minimized);
        }

        public void Restore()
        {
            provider.ChangeWindowState(WindowState.Normal);
        }
    }
}