using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Eft.Exception;
using Eft.Provider;

namespace Eft
{
    public class Application
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 100;
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
                process.WaitForInputIdle(MAXIMUM_WAIT_TIME_IN_SEC*1000);
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

        public List<Element> Find(string selectorString)
        {
            return automationProvider.Find(selectorString);
        }

        public List<Element> WaitAndFind(string selectorString)
        {
            return WaitAndFind(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public virtual Element FindFirst(string selectorString)
        {
            List<Element> els = Find(selectorString);
            if (els.Count == 0)
            {
                throw new ElementSearchException("No elements found");
            }
            return els[0];
        }

        public List<Element> WaitAndFind(string selectorString, int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (true)
            {
                List<Element> elements = Find(selectorString);
                if (elements.Count > 0)
                {
                    return elements;
                }
                if (elaspedTime > maximumWaitingTimeInSeconds*1000)
                {
                    throw new ElementSearchException(maximumWaitingTimeInSeconds + " seconds elapsed, no elements found");
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public Element WaitAndFindFirst(string selectorString)
        {
            return WaitAndFindFirst(selectorString, MAXIMUM_WAIT_TIME_IN_SEC);
        }

        public Element WaitAndFindFirst(string selectorString, int maximumWaitingTimeInSeconds)
        {
            return WaitAndFind(selectorString, maximumWaitingTimeInSeconds)[0];
        }
    }
}