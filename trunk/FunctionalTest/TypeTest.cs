using Eft;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class TypeTest
    {
        [Test]
        public void click_and_type_and_clear()
        {
            Application app = new Application("wordpad");
            app.Start();

            Element window = app.FindTopWindows()[0];
            Element editor = window.FindFirst(".RICHEDIT50W");
            editor.ClickAndType("hello, world");
            Assert.AreEqual("hello, world\r", editor.Text);
            editor.ClearText();
            Assert.AreEqual("\r", editor.Text);

            app.Stop();
        }
    }
}