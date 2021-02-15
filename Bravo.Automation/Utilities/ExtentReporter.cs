using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
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

        public static void CreateTest(String sTestCaseID, String sTestCaseTitle)
        {
            testcase = extent.CreateTest("TestCase: "+sTestCaseID, "Title: "+sTestCaseTitle);         
        }

        public static void CreateNode(String sTestStepNo)
        {
            node = testcase.CreateNode(sTestStepNo);
        }

        public static void StartTestCase(String sTestCaseName)
        {
            testcase.Info("Start TestCase " + sTestCaseName);          
        }

        public static void EndTestCase(String sTestCaseName)
        {
            testcase.Info("End TestCase " + sTestCaseName);
        }

        public static void Info(String message)
        {
            testcase.Info(message);
        }

        public static void Pass(String message)
        {
            testcase.Pass(message);
        }

        public static void Fail(String message)
        {
            testcase.Fail(message);
        }

        public static void Error(String message)
        {
            testcase.Error(message);
        }

        public static void NodeInfo(String message)
        {
            node.Info(message);
        }

        public static void NodePass(String message)
        {
            node.Pass(message);
        }

        public static void NodeFail(String message)
        {
            node.Fail(message);
        }

        public static void NodeError(String message)
        {
            node.Error(message);
        }

        public static void NodeWarn(String message)
        {
            node.Warning(message);
        }
    }
}
