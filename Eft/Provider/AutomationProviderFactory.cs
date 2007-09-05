using System;
using System.Windows.Automation;
using Eft.Locators;
using Eft.Locators.Selectors;

namespace Eft.Provider
{
    public class AutomationProviderFactory
    {
        public static IAutomationProvider FromHandle(IntPtr mainWindowHandle)
        {
            return
                new UIAutomationProvider(AutomationElement.FromHandle(mainWindowHandle), new SelectorTranslator(),
                                         new Parser());
        }
    }
}