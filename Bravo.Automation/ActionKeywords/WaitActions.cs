using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Bravo.Automation.Execution;
using Bravo.Automation.Utilities;
using Bravo.Automation.Config;

namespace Bravo.Automation.ActionKeywords
{
    public partial class Keywords
    {
        private static void WaitUntil(By by, IWebDriver driver)
        {
            try
            {
                Log.Info("WaitUntil ..");
                ExtentReporter.NodeInfo("WaitUntil ..");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.Timeout));
                wait.Until(d => d.FindElement(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntil | Exception: " + e.Message);
            }
        }

        private static void WaitUntilClickable(By by, IWebDriver driver)
        {
            try
            {
                Log.Info("WaitUntilClickable ..");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.Timeout));
                wait.Until(ExpectedConditions.ElementToBeClickable(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntilClickable | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed WaitUntil | Exception: " + e.Message);
            }
        }

        private static void WaitUntilExists(By by, IWebDriver driver)
        {
            try
            {
                Log.Info("WaitUntilExists ..");
                ExtentReporter.NodeInfo("WaitUntilExists ..");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.Timeout));
                wait.Until(ExpectedConditions.ElementExists(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntilExists | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed WaitUntilExists | Exception: " + e.Message);
            }
        }
      
        private static void WaitUntilVisible(By by,IWebDriver driver)
        {
            try
            {
                Log.Info("WaitUntilVisible ..");
                ExtentReporter.NodeInfo("WaitUntilVisible ..");
                
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.Timeout));
                wait.Until(ExpectedConditions.ElementIsVisible(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntilVisible | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed WaitUntilVisible | Exception: " + e.Message);
            }
        }

        private static void WaitFluentUntil(By by)
        {
            try
            {
                Log.Info("WaitFluentUntil ..");
                ExtentReporter.NodeInfo("WaitFluentUntil ..");

                DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
                fluentWait.Timeout = TimeSpan.FromSeconds(Constants.Timeout);
                fluentWait.PollingInterval = TimeSpan.FromSeconds(1);
                fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                IWebElement searchResult = fluentWait.Until(x => x.FindElement(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitFluentUntil | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed WaitFluentUntil | Exception: " + e.Message);
            }
        }

        private static void WaitUntilUrlContains(String data)
        {
            try
            {
                Log.Info("WaitUntilUrlContains .." + data);
                ExtentReporter.NodeInfo("WaitUntilUrlContains ..");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.NavigationTimeout));
                wait.Until(ExpectedConditions.UrlContains(data));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntilUrlContains | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed WaitUntilUrlContains | Exception: " + e.Message);
            }
        }

        private static void WaitUntilInvisibilityElement(By by)
        {
            try
            {
                Log.Info("WaitUntilInvisibilityElement ..");
                ExtentReporter.NodeInfo("WaitUntilInvisibilityElement ..");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Constants.NavigationTimeout));
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(by));
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitUntilInvisibilityElement | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed WaitUntilInvisibilityElement | Exception: " + e.Message);
            }
        }

        private static void WaitForInvisibilityLoading(out double loadtime)
        {
            Log.Info("WaitForInvisibilityLoading ..");
            loadtime = 0;
            WaitSeconds("","1");

            try
            {
                By by = By.CssSelector("svg");
                if (driver.FindElement(by).Displayed == false)
                {
                    WaitSeconds("", "1");

                    if (driver.FindElement(by).Displayed == false)
                        WaitSeconds("", "1");

                    int iteration = 1;
                    Boolean load = driver.FindElement(by).Displayed;
                    while (load)
                    {
                        load = driver.FindElement(by).Displayed;
                        WaitSeconds("", "1");
                        iteration += 1;
                        if ((!load) || (iteration == 2000))
                            break;

                    }
                    if (iteration == 2000)
                    {
                        Log.Error("WaitForInvisibilityLoading | time out ..");
                    }
                }
                else
                {
                    int iteration = 1;
                    Boolean load = driver.FindElement(by).Displayed;
                    while (load)
                    {
                        load = driver.FindElement(by).Displayed;
                        WaitSeconds("", "1");
                        iteration += 1;
                        if ((!load) || (iteration == 2000))
                            break;

                    }
                    if (iteration == 2000)
                    {
                        Log.Error("WaitForInvisibilityLoading | time out ..");
                    }
                }
            }
            catch(Exception e)
            {
                Log.Error("Failed WaitForInvisibilityLoading | Exception: " + e.Message);
            }
        }

        private static void WaitForElement(By by)
        {
            Log.Info("WaitForElement ..");
            WaitSeconds("", "1");
            try
            {
                int iteration = 0;
                bool item = CheckElement(by);
                while (!item)
                {
                    iteration += 1;
                    WaitSeconds("", "1");
                    item = CheckElement(by);
                    if ((item) || (iteration == 2000))
                        break;
                }
                if (iteration == 2000)
                {
                    Log.Error("WaitForElement | time out ..");
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed WaitForElement | Exception: " + e.Message);
            }
        }

        #region Public methods
        public static void WaitSeconds(String obj, String data)
        {
            try
            {                
                int millisec =Convert.ToInt32(data) * 1000;
                Log.Info($"Waiting {data} seconds");
                ExtentReporter.NodeInfo($"Waiting {data} seconds");
                
                Thread.Sleep(millisec);               
            }
            catch (Exception e)
            {
                Log.Error($"Failed WaitSeconds | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Failed WaitSeconds | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }
        #endregion
    }
}
