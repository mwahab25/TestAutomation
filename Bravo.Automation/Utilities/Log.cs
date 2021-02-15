using System;
using log4net;

namespace Bravo.Automation.Utilities
{
    public class Log
    {
        private static readonly ILog log =log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void StartTestCase(String sTestCaseName)
        {     
            log.Info("Start TestCase " + sTestCaseName);
        }

        public static void EndTestCase(String sTestCaseName)
        {
            log.Info("End TestCase " + sTestCaseName);
            log.Info("..................................................................................");
        }

        public static void Info(String message)
        {
            log.Info(message);
        }

        public static void Warn(String message)
        {
            log.Warn(message);
        }

        public static void Error(String message)
        {
            log.Error(message);
        }

        public static void Fatal(String message)
        {
            log.Fatal(message);
        }

        public static void Debug(String message)
        {
            log.Debug(message);
        }
    }
}
