using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace stub
{
    public partial class WaitForConditionTestWindow
    {
        private int invokeCount = 0;

        public WaitForConditionTestWindow()
        {
            InitializeComponent();
            trigger.Click += OnTriggerClick;
        }

        private void OnTriggerClick(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (invokeCount > 10) return;
            log.Text = invokeCount++.ToString();
            CommandManager.InvalidateRequerySuggested();
        }

    }
}