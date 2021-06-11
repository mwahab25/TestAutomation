using System;
using System.Reflection;
using NUnit.Framework;
using Bravo.Automation.Config;
using Bravo.Automation.ActionKeywords;
using Bravo.Automation.Utilities;

namespace Bravo.Automation.Execution
{
    [TestFixture]
    public class DriverScript
    {
        public static Keywords actionKeywords;
        public static String sActionKeyword;
        public static String sPageObject;
        public static MethodInfo[] method;
        public static int iTestStep;
        public static int iTestLastStep;
        public static String sTestCaseID;
        public static String sTestCaseTitle;
        public static String sTestCaseDesc;
        public static String sTestStepDesc;
        public static String sRunMode;
        public static String sData;
        public static int iOutcome; // 1-Pass 2-Fail 3-Error

        public DriverScript()
        {
            actionKeywords = new Keywords();
            method = actionKeywords.GetType().GetMethods();
        }

        [SetUp]
        public void TestSetUp()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [Test]
        [Category("Bravo Tests")]
        public void End2End_TestScenarios()
        {           
            ExcelUtils.SetExcelFile(Constants.Path_E2ETestData);
            DriverScript startEngine = new DriverScript();
            startEngine.Execute_TestCase();
        }

        private void Execute_TestCase()
        {
            int iTotalTestCases = ExcelUtils.GetRowCount(Constants.Sheet_TestCases);
            for (int iTestcase = 1; iTestcase < iTotalTestCases; iTestcase++) {
                iOutcome = 1;
                sTestCaseID = ExcelUtils.GetCellData(iTestcase, Constants.Col_ID, Constants.Sheet_TestCases);
                sTestCaseTitle = ExcelUtils.GetCellData(iTestcase, Constants.Col_Title, Constants.Sheet_TestCases);
                sTestCaseDesc = ExcelUtils.GetCellData(iTestcase, Constants.Col_Description, Constants.Sheet_TestCases);
                sRunMode = ExcelUtils.GetCellData(iTestcase, Constants.Col_RunMode, Constants.Sheet_TestCases);              
                
                if (sRunMode.Equals("Yes")) {
                    Log.StartTestCase(sTestCaseID);
                    ExtentReporter.CreateTest(sTestCaseID + "_" + sTestCaseTitle, sTestCaseDesc);
                    ExtentReporter.StartTestCase(sTestCaseID+"_"+ sTestCaseTitle);
                    iTestStep = ExcelUtils.GetRowContains(sTestCaseID, Constants.Col_TestCaseID, Constants.Sheet_TestSteps);
                    iTestLastStep = ExcelUtils.GetTestStepsCount(Constants.Sheet_TestSteps, sTestCaseID, iTestStep);
                    iOutcome = 1;
                    for (; iTestStep < iTestLastStep; iTestStep++) {
                        sActionKeyword = ExcelUtils.GetCellData(iTestStep, Constants.Col_ActionKeyword, Constants.Sheet_TestSteps);
                        sPageObject = ExcelUtils.GetCellData(iTestStep, Constants.Col_PageObject, Constants.Sheet_TestSteps);
                        sData = ExcelUtils.GetCellData(iTestStep, Constants.Col_DataSet, Constants.Sheet_TestSteps);
                        sTestStepDesc = ExcelUtils.GetCellData(iTestStep, Constants.Col_TestStepDesc, Constants.Sheet_TestSteps);
                        ExtentReporter.CreateNode(sTestStepDesc);
                        Execute_Actions();

                        if (iOutcome == 3)
                        {
                            ExcelUtils.SetCellData(Outcome.Error.ToString(), iTestcase, Constants.Col_Result, Constants.Sheet_TestCases);
                            Log.EndTestCase(sTestCaseID);
                            ExtentReporter.Error("TestCase " + sTestCaseID + "_" + sTestCaseTitle + " Error");
                            ExtentReporter.EndTestCase(sTestCaseID + "_" + sTestCaseTitle);
                            Assert.Fail();
                            break;
                        }
                    }

                    if (iOutcome == 1)
                    {
                        ExcelUtils.SetCellData(Outcome.Pass.ToString(), iTestcase, Constants.Col_Result, Constants.Sheet_TestCases);
                        Log.EndTestCase(sTestCaseID);
                        ExtentReporter.Pass("TestCase " + sTestCaseID + "_" + sTestCaseTitle + " Passed");
                        ExtentReporter.EndTestCase(sTestCaseID + "_" + sTestCaseTitle);
                    }
                    else if (iOutcome == 2)
                    {
                        ExcelUtils.SetCellData(Outcome.Fail.ToString(), iTestcase, Constants.Col_Result, Constants.Sheet_TestCases);
                        Log.EndTestCase(sTestCaseID);
                        ExtentReporter.Fail("TestCase " + sTestCaseID + "_" + sTestCaseTitle + " Failed");
                        ExtentReporter.EndTestCase(sTestCaseID + "_" + sTestCaseTitle);                        
                    }
                }
            }
        }

        private static void Execute_Actions()
        {
            for (int i = 0; i < method.Length; i++) {

                if (method[i].Name.Equals(sActionKeyword)) {
                    method[i].Invoke(actionKeywords, new object[] { sPageObject, sData });

                    if (iOutcome == 1)
                    {
                        ExcelUtils.SetCellData(Outcome.Pass.ToString(), iTestStep, Constants.Col_TestStepResult, Constants.Sheet_TestSteps);
                        ExtentReporter.Pass(sTestStepDesc);
                        break;
                    }
                    else if (iOutcome == 2)
                    {
                        ExcelUtils.SetCellData(Outcome.Fail.ToString(), iTestStep, Constants.Col_TestStepResult, Constants.Sheet_TestSteps);
                        ExtentReporter.Fail(sTestStepDesc);
                        //ActionKeywords.CloseBrowser("", "");
                        break;
                    }
                    else if (iOutcome == 3)
                    {
                        ExcelUtils.SetCellData(Outcome.Error.ToString(), iTestStep, Constants.Col_TestStepResult, Constants.Sheet_TestSteps);
                        ExtentReporter.Error(sTestStepDesc);
                        //ActionKeywords.CloseBrowser("", "");
                        break;
                    }
                }
            }
        }

        [TearDown]
        public void TestCloseApp()
        {
            ExtentReporter.extent.Flush();
            ExcelUtils.ExcelWBook.Save();
            ExcelUtils.ExcelWBook.Close(0);
            ExcelUtils.ExcelApp.Quit();
        }
    }
}