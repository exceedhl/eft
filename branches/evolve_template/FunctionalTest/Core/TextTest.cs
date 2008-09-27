using System;
using Eft;
using Eft.Elements;
using Eft.Win32;
using NUnit.Framework;

namespace FunctionalTest.Core
{
    [TestFixture]
    public class TextTest
    {
        private Application app;
        private Window window;

        [SetUp]
        public void setup()
        {
            string fileName = AppDomain.CurrentDomain.BaseDirectory + @"\Stub.exe";
            app = Application.Run(fileName);
            Window mainWindow = app.FindTopWindow("Stub*");
            mainWindow.FindFirst("#openTextTestWindow").Click();
            window = app.FindTopWindow("text test window");
        }

        [Test]
        public void type_text()
        {
            Application wordpad = Application.Run("wordpad");

            Element mainwindow = wordpad.FindTopWindows()[0];
            Element editor = mainwindow.FindFirst(".RICHEDIT50W");
            editor.ClickAndType("hello, world");
            Assert.AreEqual("hello, world\r", editor.Text);
            editor.ClearText();
            Assert.AreEqual("\r", editor.Text);
            editor.Type("hello, world");
            Assert.AreEqual("hello, world\r", editor.Text);
            wordpad.Stop();
        }

        [Test]
        public void should_get_text_of_element_if_it_supports_text_property()
        {
            Assert.AreEqual("text block", window.FindFirst("#textBlock").Text);
            Assert.AreEqual("text box", window.FindFirst("#textBox").Text);
            Assert.AreEqual("label value", window.FindFirst("#label").Text);
            Assert.AreEqual("group box", window.FindFirst("#groupBox").Text);
            Assert.AreEqual("ok button", window.FindFirst("#button").Text);
            Assert.AreEqual("check box", window.FindFirst("#checkBox").Text);
            Assert.AreEqual("radio button", window.FindFirst("#radioButton").Text);
            Mouse.MoveTo(window.FindFirst("#button").ClickablePoint);
            Assert.AreEqual("tool tip", window.FindFirst("#tooltip").Text);
            Assert.AreEqual("expander header", window.FindFirst("#expander").Text);
            Assert.AreEqual("item 1", window.FindFirst("#comboBoxItem").Text);
            Assert.AreEqual("list box item", window.FindFirst("#listBoxItem").Text);
        }

        [Test]
        public void should_return_title_of_window_as_text()
        {
            Assert.AreEqual("text test window", window.Title);
        }

        [Test]
        public void clear_text_should_be_able_to_focus_and_clear_text()
        {
            window.FindFirst("#button").Focus();
            Element element = window.FindFirst("#textBox");
            element.ClearText();
            Assert.AreEqual("", element.Text);
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}