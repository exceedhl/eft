using System.Collections.Generic;
using Eft;
using NUnit.Framework;

namespace FunctionalTest
{
    [TestFixture]
    public class LocatorTest
    {
        [Test]
        public void simple_selector()
        {
            Application app = new Application("wordpad");
            app.Start();

            List<Element> els = app.MainWindow.Find("#'Item 2'");
            Assert.AreEqual(1, els.Count);
            Assert.AreEqual("Edit", els[0].Name);
            Assert.AreEqual("Item 2", els[0].Id);
            // Weird thing: found toolbar button on wordpad has no name, but if you use uispy, you can 
            // see the name.
            app.MainWindow.FindFirst("Button#'Item 32799'").Click();

            app.Stop();

            app = new Application("calc");
            app.Start();

            List<Element> button1 = app.MainWindow.Find("[name='1']");
            Assert.AreEqual(1, button1.Count);
            Assert.AreEqual("1", button1[0].Name);

            Assert.IsTrue(app.MainWindow.Find("Button").Count > 0);
            Assert.IsTrue(app.MainWindow.Find("Edit").Count == 1);

            app.Stop();
        }

        [Test]
        public void and_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Assert.AreEqual("0", app.MainWindow.FindFirst("[name='0']").Name);
            Assert.AreEqual("0", app.MainWindow.FindFirst("#124").Name);
            Assert.AreEqual("0", app.MainWindow.FindFirst("Button[name='0']").Name);
            Assert.AreEqual("0", app.MainWindow.FindFirst("Button#124.Button").Name);
            app.Stop();
        }

        [Test]
        public void positional_pseudo_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Assert.AreEqual("Hex", app.MainWindow.FindFirst("RadioButton:first-of-type").Name);
            Assert.AreEqual("Hex", app.MainWindow.FindFirst("RadioButton:nth-of-type(0)").Name);
            Assert.AreEqual("Grads", app.MainWindow.FindFirst("RadioButton:last-of-type").Name);
            Assert.AreEqual("Degrees", app.MainWindow.FindFirst("RadioButton:nth-of-type(4)").Name);
            app.Stop();
        }

        [Test]
        public void combination_selector()
        {
            Application app = new Application("wordpad");
            app.Start();
            Assert.AreEqual("1001", app.MainWindow.FindFirst("ToolBar[name='Formatting'] Edit:first-of-type").Id);
            Assert.AreEqual("Item 57600", app.MainWindow.FindFirst("ToolBar[name='Standard'] Button").Id);
            Element fontSizeDropDownButton = app.MainWindow.FindFirst("Button[name='Drop Down Button']:nth-of-type(1)");
            Assert.AreEqual("Drop Down Button", fontSizeDropDownButton.Name);
            fontSizeDropDownButton.Click();
            Assert.AreEqual("12", app.MainWindow.FindFirst("ListItem[name='12']").Name);
            app.MainWindow.FindFirst("ListItem[name='12']").Click();
            Assert.AreEqual("", app.MainWindow.FindFirst(".RICHEDIT50W").Name);
            app.Stop();
        }
    }
}