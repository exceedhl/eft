using Eft.Exception;
using Eft.Provider;

namespace Eft
{
    public class Window : Element
    {
        public Window(IAutomationProvider automationProvider) : base(automationProvider)
        {
            if (!automationProvider.IsWindow)
            {
                throw new ControlTypeConversionException("The control is not of control type Window");
            }
        }
    }
}