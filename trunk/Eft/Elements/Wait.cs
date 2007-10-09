using System.Threading;

namespace Eft.Elements
{
    public class Wait
    {
        private const int MAXIMUM_WAIT_TIME_IN_SEC = 30;
        private const int WAIT_INTERVAL_IN_MILLIS = 10;

        public delegate bool ConditionCheckerDelegate();

        public static void Until(ConditionCheckerDelegate conditionCheckerDelegate, int maximumWaitingTimeInSeconds)
        {
            int elaspedTime = 0;
            while (!conditionCheckerDelegate())
            {
                if (elaspedTime > maximumWaitingTimeInSeconds * 1000)
                {
                    return;
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }

        public static void Until(ConditionCheckerDelegate conditionCheckerDelegate)
        {
            int elaspedTime = 0;
            while (!conditionCheckerDelegate())
            {
                if (elaspedTime > MAXIMUM_WAIT_TIME_IN_SEC * 1000)
                {
                    return;
                }
                Thread.Sleep(WAIT_INTERVAL_IN_MILLIS);
                elaspedTime += WAIT_INTERVAL_IN_MILLIS;
            }
        }
    }
}