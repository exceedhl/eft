using System;

namespace Eft
{
    public class Tester
    {
        private Application app;

        private Tester(string fileName)
        {
            app = Application.Run(fileName);
        }

        public static Tester Run(string fileName)
        {
            return new Tester(fileName);
        }

        public void AssertWindowTitle(string title)
        {
            throw new NotImplementedException();
        }

        public void AssertTextPresent(string text)
        {
            throw new NotImplementedException();
        }

        public void Retire()
        {
            app.Stop();
        }
    }
}