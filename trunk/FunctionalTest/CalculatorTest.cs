using eft;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class CalculatorTest
    {
        [Test]
        public void calc_1_plus_2()
        {
            Application app = new Application("calc");
            app.Start();

            app.FindFirst("Button[name='1']").Click();
            app.FindFirst("*[name='+']").Click();
            app.FindFirst("[name='2']").Click();
            app.FindFirst("[name='=']").Click();
            Element edit = app.FindFirst("Edit");
            edit.Type("3333");
            app.Stop();
        }
    }
}