using System.Collections.Generic;
using System.Windows.Automation;
using Eft.Provider;

namespace Eft.Elements
{
    public class Desktop
    {
        public static List<Window> FindTopWindowsByProcessId(int processId)
        {
            AutomationElementCollection allWindows =
                AutomationElement.RootElement.FindAll(TreeScope.Children,
                                                      new AndCondition(
                                                          new PropertyCondition(AutomationElement.ControlTypeProperty,
                                                                                ControlType.Window),
                                                          new PropertyCondition(AutomationElement.ProcessIdProperty,
                                                                                processId)));
            List<Window> windows = new List<Window>();
            foreach (AutomationElement automationElement in allWindows)
            {
                windows.Add(new Window(AutomationProviderFactory.FromAutomationElement(automationElement)));
            }
            return windows;
        }
    }
}