using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bravo.Automation.Utilities;
using Bravo.Automation.Execution;
using OpenQA.Selenium;
using Bravo.Automation.Config;

namespace Bravo.Automation.ActionKeywords
{
    public partial class Keywords
    {       
        private static void AssertElementExist(String obj, String data)
        {

        }

        private static void AssertUrlExist(Object obj, String data)
        {

        }

        private static void AssertUrlContains(Object obj, String data)
        {

        }

        private static void AssertNull(Object obj, String data)
        {
            try
            {
                Assert.IsNull(obj);
                Log.Info("Assertion Passed; actual value is null.");
            }
            catch (AssertFailedException e)
            {
                Log.Info("Assertion Failed; actual value is not null. | Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Log.Error("Assertion Failed; | Exception: " + e.Message);
            }
        }

        private static void AssertNotNull(Object obj, String data)
        {
            try
            {
                Assert.IsNotNull(obj);
                Log.Info("Assertion Passed; actual value is not null.");
            }
            catch (AssertFailedException e)
            {
                Log.Info("Assertion Failed; actual value is null. | Exception: " + e.Message);
            }
            catch (Exception e)
            {
                Log.Error("Assertion Failed; | Exception: " + e.Message);
            }
        }

        private static string AssertEqual(String Actual, String Expected)
        {
            bool result = (Actual).Equals(Expected);
            String status = result ? Outcome.Pass.ToString() : Outcome.Fail.ToString();
            return status;
        }

        private static string AssertNotEqual(String obj, String data)
        {
            bool result = (data).Equals(driver.Title);
            String status = result ? Outcome.Pass.ToString() : Outcome.Fail.ToString();
            return status;
        }

        private static void AssertTrue(String obj, String data)
        {
            
        }

        private static void AssertFalse(String obj, String data)
        {

        }

        private static void AssertSame(String obj, String data)
        {

        }

        private static void AssertNotSame(String obj, String data)
        {

        }

        public static void AssertElementContains(String obj, String data)
        {
            Log.Info("AssertElementContains .. " + obj);
            ExtentReporter.NodeInfo("AssertElementContains .. " + obj);
            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                //ScrollIntoView(by);
                WaitUntil(by);

                Assert.AreEqual(GetTextByDriver(by), data);
                WaitSeconds("", "2");
                DriverScript.bAssert = true;
                DriverScript.bOutcome = 1;
            }
            catch (AssertFailedException e)
            {
                Log.Error("AssertElementContains Assert Fail| Exception: " + e.Message);
                ExtentReporter.NodeFail("AssertElementContains Assert Fail| Exception: " + e.Message);
                DriverScript.bResult = true;
                DriverScript.bAssert = false;
                DriverScript.bOutcome = 2;
            }
            catch (Exception e)
            {
                Log.Error("Failed AssertElementContains | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed AssertElementContains | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }

    }
}
