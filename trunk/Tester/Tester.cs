using System;
using Eft.Elements;
using Eft.Tester.Exception;
using NUnit.Framework;

namespace Eft.Tester
{
    public class Tester
    {
        private Application app;
        private Window currentWindow;

        private Tester(string fileName)
        {
            app = Application.Run(fileName);
        }

        public static Tester Run(string fileName)
        {
            return new Tester(fileName);
        }

        public void AssertWindowTitle(string titlePattern)
        {
            if (currentWindow == null)
            {
                throw new WindowNotSpecifiedException("You have not select any window.");
            }
            PatternRecoganizer recoganizer = new PatternRecoganizer(titlePattern);
            Assert.IsTrue(recoganizer.Match(currentWindow.Title, recoganizer.Pattern),
                          string.Format("Actual window title: [{0}] does not match pattern: [{1}]", currentWindow.Title,
                                        titlePattern));
        }

        public void AssertTextPresent(string text)
        {
            throw new NotImplementedException();
        }

        public void SelectWindow(string titlePattern)
        {
            PatternRecoganizer recoganizer = new PatternRecoganizer(titlePattern);
            currentWindow = app.FindTopWindow(recoganizer.Pattern, recoganizer.Match);
        }

        public void Retire()
        {
            app.Stop();
        }
    }
}