using System;
using System.Collections.Generic;
using System.Diagnostics;
using Eft.Provider;

namespace Eft
{
    public class Application
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private readonly Process process;
        private Element mainWindow;

        public Application(string fileName)
        {
            process = new Process();
            process.StartInfo.FileName = fileName;
        }

        public Application(Process process)
        {
            this.process = process;
            mainWindow = new Element(AutomationProviderFactory.FromHandle(process.MainWindowHandle));
        }

        public void Start()
        {
            process.Start();
            try
            {
                process.WaitForInputIdle(MAXIMUM_WAIT_TIME_IN_SEC*1000);
                mainWindow = new Element(AutomationProviderFactory.FromHandle(process.MainWindowHandle));
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

        public Element MainWindow
        {
            get { return mainWindow; }
        }

        public static Application[] FromProcessName(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            List<Application> apps = new List<Application>();
            foreach (Process process in processes)
            {
                apps.Add(new Application(process));
            }
            return apps.ToArray();
        }
    }
}