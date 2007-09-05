using System;
using System.Windows.Automation;
using eft.Locators;
using eft.Locators.Selectors;

namespace eft.Provider
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