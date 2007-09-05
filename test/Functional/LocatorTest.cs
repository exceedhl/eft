using System.Collections.Generic;
using eft;
using NUnit.Framework;

namespace test.Functional
{
    [TestFixture]
    public class LocatorTest
    {
        [Test]
        public void simple_selector()
        {
            Application app = new Application("wordpad");
            app.Start();

            List<Element> els = app.Find("#'Item 2'");
            Assert.AreEqual(1, els.Count);
            Assert.AreEqual("Edit", els[0].Name);
            Assert.AreEqual("Item 2", els[0].Id);
            // Weird thing: found toolbar button on wordpad has no name, but if you use uispy, you can 
            // see the name.
            app.FindFirst("Button#'Item 32799'").Click();

            app.Stop();

            app = new Application("calc");
            app.Start();

            List<Element> button1 = app.Find("[name='1']");
            Assert.AreEqual(1, button1.Count);
            Assert.AreEqual("1", button1[0].Name);

            Assert.IsTrue(app.Find("Button").Count > 0);
            Assert.IsTrue(app.Find("Edit").Count == 1);

            app.Stop();
        }

        [Test]
        public void and_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Assert.AreEqual("0", app.FindFirst("[name='0']").Name);
            Assert.AreEqual("0", app.FindFirst("#124").Name);
            Assert.AreEqual("0", app.FindFirst("Button[name='0']").Name);
            Assert.AreEqual("0", app.FindFirst("Button#124.Button").Name);
            app.Stop();
        }

        [Test]
        public void positional_pseudo_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Assert.AreEqual("Hex", app.FindFirst("RadioButton:first-of-type").Name);
            Assert.AreEqual("Hex", app.FindFirst("RadioButton:nth-of-type(0)").Name);
            Assert.AreEqual("Grads", app.FindFirst("RadioButton:last-of-type").Name);
            Assert.AreEqual("Degrees", app.FindFirst("RadioButton:nth-of-type(4)").Name);
            app.Stop();
        }

        [Test]
        public void combination_selector()
        {
            Application app = new Application("wordpad");
            app.Start();
            Assert.AreEqual("1001", app.FindFirst("ToolBar[name='Formatting'] Edit:first-of-type").Id);
            Assert.AreEqual("Item 57600", app.FindFirst("ToolBar[name='Standard'] Button").Id);
            Element fontSizeDropDownButton = app.FindFirst("Button[name='Drop Down Button']:nth-of-type(1)");
            Assert.AreEqual("Drop Down Button", fontSizeDropDownButton.Name);
            fontSizeDropDownButton.Click();
            Assert.AreEqual("12", app.FindFirst("ListItem[name='12']").Name);
            app.FindFirst("ListItem[name='12']").Click();
            Assert.AreEqual("", app.FindFirst(".RICHEDIT50W").Name);
            app.Stop();
        }
    }
}