using System;
using System.Threading;
using Eft;
using Eft.Elements;
using Eft.Exception;
using NUnit.Framework;

namespace FunctionalTest.Core
{
    [TestFixture]
    public class ToggleFeatureTest
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
        public void should_return_correct_toggle_state_for_elements_supporting_toggle_pattern()
        {
            Element checkBox = window.FindFirst("#checkBox");
            Assert.IsFalse(checkBox.IsChecked);
            Assert.IsFalse(checkBox.IsSelected);
            checkBox.Click();
            Assert.IsTrue(checkBox.IsChecked);
            Assert.IsTrue(checkBox.IsSelected);
        }

        [Test]
        public void should_return_correct_selection_state_for_elements_supporting_selection_item_pattern()
        {
            Element radioButton1 = window.FindFirst("#radioButton");
            Assert.IsFalse(radioButton1.IsChecked);
            Assert.IsFalse(radioButton1.IsSelected);
            radioButton1.Click();
            Assert.IsTrue(radioButton1.IsChecked);
            Assert.IsTrue(radioButton1.IsSelected);

            Element comboBoxItem = window.FindFirst("#comboBoxItem");
            Assert.IsFalse(comboBoxItem.IsSelected);
            //todo: wait for menu showing or using selectionItem.select
            window.FindFirst("#comboBox").Click();
            Thread.Sleep(1000);
            comboBoxItem.Click();
            Assert.IsTrue(comboBoxItem.IsSelected);

            Element listBoxItem = window.FindFirst("#listBoxItem");
            Assert.IsFalse(listBoxItem.IsSelected);
            listBoxItem.Click();
            Assert.IsTrue(listBoxItem.IsSelected);

            Element tab2 = window.FindFirst("#tab2");
            Assert.IsFalse(tab2.IsSelected);
            tab2.Click();
            Assert.IsTrue(tab2.IsSelected);

            Element listViewRow0 = window.FindFirst("#listView DataItem:first-of-type");
            Assert.IsFalse(listViewRow0.IsSelected);
            listViewRow0.Click();
            Assert.IsTrue(listViewRow0.IsSelected);
        }

        [Test]
        [ExpectedException(typeof (PropertyNotSupportedException),
            ExpectedMessage = "Current element does not support this property")]
        public void should_throw_exception_if_not_supporting_toggle_pattern()
        {
            bool isChecked = window.FindFirst("#textBox").IsChecked;
        }

        [TearDown]
        public void teardown()
        {
            app.Stop();
        }
    }
}