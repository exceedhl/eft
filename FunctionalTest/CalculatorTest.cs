using Eft;
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

            app.MainWindow.FindFirst("Button[name='1']").Click();
            app.MainWindow.FindFirst("*[name='+']").Click();
            app.MainWindow.FindFirst("[name='2']").Click();
            app.MainWindow.FindFirst("[name='=']").Click();
            Element edit = app.MainWindow.FindFirst("Edit");
            edit.Type("3333");
            app.Stop();
        }
    }
}