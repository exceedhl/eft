using System;
using System.Collections.Generic;
using Eft.Elements;
using NUnit.Framework;

namespace Eft.Tester
{
    public class Tester
    {
        private readonly Application app;
        private Window currentWindow;

        private Tester(string fileName)
        {
            app = Application.Run(fileName);
            List<Window> windows = app.FindTopWindows();
            currentWindow = windows[windows.Count - 1];
        }

        public static Tester Run(string fileName)
        {
            return new Tester(fileName);
        }

        public void Retire()
        {
            app.Stop();
        }

        public void SelectWindow(string titlePattern)
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer(titlePattern);
            currentWindow = app.FindTopWindow(recoganizer.Pattern, recoganizer.Match);
        }

        public int WindowCount
        {
            get { return app.WindowCount; }
        }

        public void Click(string locatorString)
        {
            currentWindow.FindFirst(locatorString).Click();
        }

        public void Click(string locatorString, string clickPattern)
        {
            ClickPatternRecoganizer recoganizer = new ClickPatternRecoganizer(clickPattern);
            currentWindow.FindFirst(locatorString).Click(recoganizer.MouseButton, recoganizer.ModifierKeys, recoganizer.Times);
        }

        public void AssertTextPresent(string text)
        {
            throw new NotImplementedException();
        }

        public void AssertText(string locatorString, string pattern)
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer(pattern);
            string actualText = currentWindow.FindFirst(locatorString).Text;
            Assert.IsTrue(recoganizer.Match(actualText, recoganizer.Pattern),
                          string.Format("Actual Text: [{0}] does not match pattern: [{1}]", actualText, pattern));
        }

        public void AssertWindowTitle(string titlePattern)
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer(titlePattern);
            Assert.IsTrue(recoganizer.Match(currentWindow.Title, recoganizer.Pattern),
                          string.Format("Actual window title: [{0}] does not match pattern: [{1}]", currentWindow.Title,
                                        titlePattern));
        }
    }
}