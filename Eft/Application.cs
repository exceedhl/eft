using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Automation;
using eft.Exception;
using eft.Provider;

namespace eft
{
    public class Application
    {
        private const int MAXIMUM_WAIT_TIME_FOR_WINDOW_APPEAR = 30000;
        private readonly Process process;
        private IAutomationProvider automationProvider;

        public Application(string fileName)
        {
            process = new Process();
            process.StartInfo.FileName = fileName;
        }

        public void Start()
        {
            process.Start();
            try
            {
                process.WaitForInputIdle(MAXIMUM_WAIT_TIME_FOR_WINDOW_APPEAR);
                automationProvider = AutomationProviderFactory.FromHandle(process.MainWindowHandle);
            }
            catch (InvalidOperationException)
            {
                // the app does not have a graphical interface
            }
        }

        public void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }

        public List<Window> FindWindow(string title)
        {
            List<Window> foundWindows = new List<Window>();
            AutomationElement mainWindow = AutomationElement.FromHandle(process.MainWindowHandle);
            AutomationElementCollection windows =
                mainWindow.FindAll(TreeScope.Descendants, new PropertyCondition(AutomationElement.NameProperty, title));
            foreach (AutomationElement window in windows)
            {
                foundWindows.Add(new Window(window));
            }
            return foundWindows;
        }

        public List<Element> Find(string selectorString)
        {
            return automationProvider.Find(selectorString);
        }

        public Element FindFirst(string selectorString)
        {
            List<Element> els = Find(selectorString);
            if (els.Count == 0)
            {
                throw new ElementSearchException("No elements found");
            }
            return els[0];
        }
    }
}