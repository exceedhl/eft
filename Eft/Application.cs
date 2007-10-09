using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Eft.Elements;
using Eft.Exception;

namespace Eft
{
    public class Application
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 100;

        private readonly Process process;

        private Application(string fileName)
        {
            process = new Process();
            process.StartInfo.FileName = fileName;
            process.Start();
        }

        private Application(Process process)
        {
            this.process = process;
        }

        public static Application[] AttachProcess(string processName)
        {
            Process[] processes = Process.GetProcessesByName(processName);
            List<Application> apps = new List<Application>();
            foreach (Process process in processes)
            {
                apps.Add(new Application(process));
            }
            return apps.ToArray();
        }

        public static Application Run(string executable)
        {
            Application application = new Application(executable);
            return application;
        }

        public void Stop()
        {
            if (process != null)
            {
                process.Kill();
            }
        }

        public List<Window> FindTopWindows(int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (true)
            {
                List<Window> windows = Desktop.FindTopWindowsByProcessId(process.Id);
                if (windows.Count > 0)
                {
                    return windows;
                }
                if (elaspedTime > maximumWaitingTimeInSeconds*1000)
                {
                    throw new ElementSearchException(maximumWaitingTimeInSeconds + " seconds elapsed, no window found");
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public List<Window> FindTopWindows()
        {
            return FindTopWindows(MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Window FindTopWindow(string title, StringMatchDelegate stringMatchingCriteria,
                                    int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (true)
            {
                List<Window> windows = Desktop.FindTopWindowsByProcessId(process.Id);
                foreach (Window window in windows)
                {
                    if (stringMatchingCriteria(window.Name, title))
                    {
                        return window;
                    }
                }
                if (elaspedTime > maximumWaitingTimeInSeconds*1000)
                {
                    throw new ElementSearchException(maximumWaitingTimeInSeconds + " seconds elapsed, no window found");
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public Window FindTopWindow(string title, StringMatchDelegate stringMatchDelegate)
        {
            return FindTopWindow(title, stringMatchDelegate, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Window FindTopWindow(string title)
        {
            return FindTopWindow(title, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Window FindTopWindow(string title, int maximumWaitingTimeInSeconds)
        {
            return FindTopWindow(title, Match.Glob, maximumWaitingTimeInSeconds);
        }
    }
}