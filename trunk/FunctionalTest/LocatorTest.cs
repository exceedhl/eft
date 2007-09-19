using System.Collections.Generic;
using Eft;
using Eft.Elements;
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

            Window window = app.FindTopWindows()[0];
            List<Element> els = window.Find("#'Item 2'");
            Assert.AreEqual(1, els.Count);
            Assert.AreEqual("Edit", els[0].Name);
            Assert.AreEqual("Item 2", els[0].Id);
            // Weird thing: found toolbar button on wordpad has no name, but if you use uispy, you can 
            // see the name.
            window.FindFirst("Button#'Item 32799'").Click();

            app.Stop();

            app = new Application("calc");
            app.Start();

            window = app.FindTopWindows()[0];
            List<Element> button1 = window.Find("[name='1']");
            Assert.AreEqual(1, button1.Count);
            Assert.AreEqual("1", button1[0].Name);

            Assert.IsTrue(window.Find("Button").Count > 0);
            Assert.IsTrue(window.Find("Edit").Count == 1);

            app.Stop();
        }

        [Test]
        public void and_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Element window = app.FindTopWindows()[0];
            Assert.AreEqual("0", window.FindFirst("[name='0']").Name);
            Assert.AreEqual("0", window.FindFirst("#124").Name);
            Assert.AreEqual("0", window.FindFirst("Button[name='0']").Name);
            Assert.AreEqual("0", window.FindFirst("Button#124.Button").Name);
            app.Stop();
        }

        [Test]
        public void positional_pseudo_selector()
        {
            Application app = new Application("calc");
            app.Start();
            Element window = app.FindTopWindows()[0];
            Assert.AreEqual("Hex", window.FindFirst("RadioButton:first-of-type").Name);
            Assert.AreEqual("Hex", window.FindFirst("RadioButton:nth-of-type(0)").Name);
            Assert.AreEqual("Grads", window.FindFirst("RadioButton:last-of-type").Name);
            Assert.AreEqual("Degrees", window.FindFirst("RadioButton:nth-of-type(4)").Name);
            app.Stop();
        }

        [Test]
        public void combination_selector()
        {
            Application app = new Application("wordpad");
            app.Start();
            Element window = app.FindTopWindows()[0];
            Assert.AreEqual("1001", window.FindFirst("ToolBar[name='Formatting'] Edit:first-of-type").Id);
            Assert.AreEqual("Item 57600", window.FindFirst("ToolBar[name='Standard'] Button").Id);
            Element fontSizeDropDownButton = window.FindFirst("Button[name='Drop Down Button']:nth-of-type(1)");
            Assert.AreEqual("Drop Down Button", fontSizeDropDownButton.Name);
            fontSizeDropDownButton.Click();
            Assert.AreEqual("12", window.FindFirst("ListItem[name='12']").Name);
            window.FindFirst("ListItem[name='12']").Click();
            Assert.AreEqual("", window.FindFirst(".RICHEDIT50W").Name);
            app.Stop();
        }
    }
}