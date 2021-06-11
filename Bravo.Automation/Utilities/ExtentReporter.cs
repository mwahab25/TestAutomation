using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Bravo.Automation.Config;

namespace Bravo.Automation.Utilities
{
    public class ExtentReporter
    {
        public static ExtentReports extent;
        public static ExtentTest testcase;
        public static ExtentTest node;

        static ExtentReporter()
        {
            extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(Constants.Path_Report);
            htmlReporter.Config.Theme = Theme.Standard;       
            extent.AttachReporter(htmlReporter);
        }

        /// <summary>
        /// Create testcase details
        /// </summary>
        public static void CreateTest(String sTestCaseID, String sTestCaseTitle)
        {
            testcase = extent.CreateTest("TestCase: "+sTestCaseID, "Title: "+sTestCaseTitle);   
        }

        /// <summary>
        /// Create test step node
        /// </summary>
        public static void CreateNode(String sTestStepNo)
        {
            node = testcase.CreateNode(sTestStepNo);
        }

        /// <summary>
        /// Start testcase note
        /// </summary>
        public static void StartTestCase(String sTestCaseName)
        {
            testcase.Info("Start TestCase " + sTestCaseName);          
        }

        /// <summary>
        /// End testcase note
        /// </summary>
        public static void EndTestCase(String sTestCaseName)
        {
            testcase.Info("End TestCase " + sTestCaseName);
        }

        /// <summary>
        /// Info testcase step
        /// </summary>
        public static void Info(String message)
        {
            testcase.Info(message);
        }

        /// <summary>
        /// Pass test step
        /// </summary>
        public static void Pass(String message)
        {
            testcase.Pass(message);
        }

        /// <summary>
        /// Fail test step
        /// </summary>
        public static void Fail(String message)
        {
            testcase.Fail(message);
        }

        /// <summary>
        /// Error test step
        /// </summary>
        public static void Error(String message)
        {
            testcase.Error(message);
        }

        /// <summary>
        /// Warning test step
        /// </summary>
        public static void Warn(String message)
        {
            testcase.Warning(message);
        }


        public static void AddScreenShot(String path)
        {
            testcase.AddScreenCaptureFromPath(path);
        }

        /// <summary>
        /// Info test step node
        /// </summary>
        public static void NodeInfo(String message)
        {
            node.Info(message);
        }

        /// <summary>
        /// Pass testcase step node
        /// </summary>
        public static void NodePass(String message)
        {
            node.Pass(message);
        }

        /// <summary>
        /// Fail test step node
        /// </summary>
        public static void NodeFail(String message)
        {
            node.Fail(message);
        }

        /// <summary>
        /// Error test step node
        /// </summary>
        public static void NodeError(String message)
        {
            node.Error(message);
        }

        /// <summary>
        /// Warning test step node
        /// </summary>
        public static void NodeWarn(String message)
        {
            node.Warning(message);
        }

        public static void NodeAddScreenShot(String path)
        {
            node.AddScreenCaptureFromPath(path);
        }
    }
}
