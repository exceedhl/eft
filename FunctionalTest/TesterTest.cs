using Eft;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class TesterTest
    {
        [Test]
        public void tester_creation()
        {
            Tester i = Tester.Run("calc");
//            i.AssertWindowTitle("Calculator");
//            i.AssertTextPresent("Calculator");
            i.Retire();
        }
    }
}