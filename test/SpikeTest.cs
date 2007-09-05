using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using eft;
using eft.Provider;
using eft.Win32;
using NUnit.Framework;
using Application=eft.Application;

namespace test
{
    [TestFixture]
    public class SpikeTest
    {
        [Test]
        public void spike()
        {
            Process clientProcess = Process.Start("wordpad");
            Thread.Sleep(1000);
            AutomationElement window = AutomationElement.FromHandle(clientProcess.MainWindowHandle);

            AutomationElement openButton = window.FindFirst(TreeScope.Descendants,
                                                            new PropertyCondition(
                                                                AutomationElement.AutomationIdProperty, "Item 57601"));
            Console.WriteLine(
                APIWrapper.FindWindowEx(clientProcess.MainWindowHandle, IntPtr.Zero, "ToolbarWindow32", "Standard"));
            Console.WriteLine(clientProcess.MainWindowHandle);
//            Point point = openButton.GetClickablePoint();
//            Console.WriteLine(point);
//            InputHelper.MoveTo(clientProcess.MainWindowHandle, point);
//            InputHelper.Click(clientProcess.MainWindowHandle, openButton.GetClickablePoint());
//            window.FindFirst(TreeScope.Children, new PropertyCondition(AutomationElement.));

            Keyboard.Command(clientProcess.MainWindowHandle);
            SendKeys.SendWait("shit.txt");
            AutomationElement open = window.FindFirst(TreeScope.Descendants,
                                                      new PropertyCondition(AutomationElement.NameProperty,
                                                                            "Open"));
            Console.WriteLine(open.Current.Name);
            AutomationElement cancel =
                open.FindFirst(TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, "2"));
            InvokePattern pattern = (InvokePattern) cancel.GetCurrentPattern(InvokePattern.Pattern);
            pattern.Invoke();
            Console.WriteLine(APIWrapper.FindWindowEx(clientProcess.MainWindowHandle, IntPtr.Zero, "#32770(Dialog)", ""));
            WindowPattern windowPattern = (WindowPattern) window.GetCurrentPattern(WindowPattern.Pattern);
//            windowPattern.Close();
            Keyboard.Command(clientProcess.MainWindowHandle);
            clientProcess.Kill();
        }

        [Test]
        public void spike_find_window()
        {
            Console.WriteLine(APIWrapper.FindWindow("", "Document - WordPad"));
            int window = APIWrapper.FindWindow("WordPadClass", "Document - WordPad");
            Console.WriteLine(window);
            Console.WriteLine(APIWrapper.FindWindowEx(new IntPtr(window), IntPtr.Zero, "ToolbarWindow32", ""));
        }

        [Test]
        public void start_process()
        {
            Process p = Process.Start("wordpad");
            Console.WriteLine(p.MachineName);
            Console.WriteLine(p.Id);
            Console.WriteLine(p.ProcessName);
            Console.WriteLine(p.StartTime);
            Console.WriteLine(p.MainWindowTitle);

            p.Kill();
        }

        [Test]
        public void automation()
        {
            Console.WriteLine(Automation.PropertyName(AutomationElement.ControlTypeProperty));
        }

        [Test]
        public void regex()
        {
            string TYPE = @"([a-zA-Z]+|\*)?";
            string ATTRIBUTE = @"(\[(\w+)='([^\[]+)'\]|#((\w+)|'(.+)')|\.((\w+)|'(.+)'))*";
            string PSEUDO = @"(?<pseudo>(?<=\S+):[a-z\-\(\)0-9]+)?";
            string COMBINATOR = @"\s*(?<combinator>[>~\+\s])\s*(?=\S+)";

            Regex css =
                new Regex(
                    "(" + COMBINATOR + ")?" + @"((?<type>" + TYPE + ")(?<attr>" + ATTRIBUTE + ")" + PSEUDO + ")$",
                    RegexOptions.RightToLeft | RegexOptions.ExplicitCapture);

            string input = "Button + Type[name='hel #.+~\'lo'].'class name':first > [name='shit']#'some id':sec";
            string input2 = "type > t";
//
            Match m = css.Match(input2);
            while (m.Success)
            {
//                while (m.Success == true)
//                {
                //                Console.WriteLine(m.Groups[1]);
                foreach (Group group in m.Groups)
                {
                    Console.WriteLine(group.Value);
                }
                //                m = m.NextMatch();
//                }
                input2 = input2.Substring(0, m.Index);
                if (input2.Length == 0) break;
                m = css.Match(input2);
            }
        }

        [Test]
        public void wordpad()
        {
            Application app = new Application("wordpad");
            app.Start();
            app.FindFirst("MenuItem[name='Help']").Click();
            app.FindFirst("MenuItem[name='Help'] MenuItem:last-of-type").Click();
            Thread.Sleep(1000);
            app.FindFirst("Window[name='About WordPad'] Button").Click();
            app.Stop();
        }

        [Ignore]
        [Test]
        public void cmd()
        {
            Application app = new Application("cmd");
            app.Start();
            Thread.Sleep(3000);
            SendKeys.SendWait("dir{ENTER}");
            SendKeys.SendWait("cd ..{ENTER}");
            SendKeys.SendWait("dir  {ENTER}");
            app.Stop();
        }

        [Ignore]
        [Test]
        public void from_existing_window()
        {
            Process process = Process.GetProcessesByName("calc")[0];
            new Element(AutomationProviderFactory.FromHandle(process.MainWindowHandle)).Find("Edit")[0].Type("3333");
        }

        [Ignore]
        [Test]
        public void desktop()
        {
            AutomationElement desktop = AutomationElement.RootElement;
            Console.WriteLine(desktop.Current.ClassName);
            AutomationElementCollection all = desktop.FindAll(TreeScope.Children, Condition.TrueCondition);
        }
    }
}