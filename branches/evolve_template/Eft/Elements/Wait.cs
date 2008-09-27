using System.Threading;

namespace Eft.Elements
{
    public class Wait
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 10;

        public delegate bool ConditionCheckerDelegate();

        public delegate object GetObjectValueDelegate();

        public static void Until(ConditionCheckerDelegate conditionCheckerDelegate, int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (!conditionCheckerDelegate())
            {
                if (elaspedTime > maximumWaitingTimeInSeconds*1000)
                {
                    return;
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public static void Until(ConditionCheckerDelegate d)
        {
            int elaspedTime = 0;
            while (!d())
            {
                if (elaspedTime > MAXIMUM_WAIT_TIME_IN_SEC*1000)
                {
                    return;
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public static void UntilChanged(GetObjectValueDelegate d, int maximumWaitingTimeInSeconds)
        {
            object oldValue = d();
            Until(delegate { return oldValue != d(); }, maximumWaitingTimeInSeconds);
        }

        public static void UntilChanged(GetObjectValueDelegate d)
        {
            UntilChanged(d, MAXIMUM_WAIT_TIME_IN_SEC);
        }
    }
}