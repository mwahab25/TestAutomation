using System;
using log4net;

namespace Bravo.Automation.Utilities
{
    public class Log
    {
        private static readonly ILog log =log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        /// <summary>
        /// Start testcase note
        /// </summary>
        public static void StartTestCase(String sTestCaseName)
        {     
            log.Info("Start TestCase " + sTestCaseName);
        }

        /// <summary>
        /// End testcase note
        /// </summary>
        public static void EndTestCase(String sTestCaseName)
        {
            log.Info("End TestCase " + sTestCaseName);
            log.Info("..................................................................................");
        }

        /// <summary>
        /// Info test step
        /// </summary>
        public static void Info(String message)
        {
            log.Info(message);
        }

        /// <summary>
        /// Warning test step
        /// </summary>
        public static void Warn(String message)
        {
            log.Warn(message);
        }

        /// <summary>
        /// Error test step
        /// </summary>
        public static void Error(String message)
        {
            log.Error(message);
        }

        /// <summary>
        /// Fatal test step
        /// </summary>
        public static void Fatal(String message)
        {
            log.Fatal(message);
        }

        /// <summary>
        /// Debug test step
        /// </summary>
        public static void Debug(String message)
        {
            log.Debug(message);
        }
    }
}
