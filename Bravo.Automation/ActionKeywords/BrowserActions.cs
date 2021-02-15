using System;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Bravo.Automation.Utilities;
using Bravo.Automation.Execution;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using Bravo.Automation.Config;

namespace Bravo.Automation.ActionKeywords
{
    public partial class Keywords
    {
        private static RemoteWebDriver driver;
        public static void OpenBrowser(String obj, String data)
        {
            Log.Info("Opening Browser ");
            ExtentReporter.NodeInfo("Opening Browser ");
            try
            {
                if (data.Equals(Browsers.Firefox.ToString()))
                {
                    driver = new FirefoxDriver();
                    Log.Info("Firefox browser started ");
                    ExtentReporter.NodeInfo("Firefox browser started ");
                    
                }
                else if (data.Equals(Browsers.IE.ToString()))
                {
                    driver = new InternetExplorerDriver();
                    Log.Info("IE browser started ");
                    ExtentReporter.NodeInfo("IE browser started ");
                    
                }
                else if (data.Equals(Browsers.Chrome.ToString()))
                {                   
                    //var options = new ChromeOptions();
                    var options = new ChromeOptionsWithPrefs();
                    options.AddArguments("--start-maximized");
                    options.AddArgument("--enable-automation");
                    options.AddArgument("--no-sandbox");
                    options.AddArgument("--disable-infobars");
                    options.AddArgument("--disable-dev-shm-usage");
                    options.AddArgument("--disable-browser-side-navigation");
                    options.AddArgument("--disable-gpu");

                    options.prefs = new Dictionary<string, object>
                    {
                        { "profile.default_content_settings.popups", 0 },
                        { "download.prompt_for_download","false" }
                    };

                    driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(),options,TimeSpan.FromSeconds(Constants.NavigationTimeout));

                    Log.Info("Chrome browser started ");
                    ExtentReporter.NodeInfo("Chrome browser started ");
                    
                }
                else if (data.Equals(Browsers.Edge.ToString()))
                {
                    driver = new EdgeDriver();
                    Log.Info("Edge browser started ");
                    ExtentReporter.NodeInfo("Edge browser started ");
                    
                }
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.Timeout);
            }
            catch (Exception e)
            {
                Log.Error("Not able to open Browser | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to open Browser | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        } 

        public static void RefreshBrowser(String obj, String data)
        {
            driver.Navigate().Refresh();
        }

        public static void Navigate(String obj, String data)
        {
            try
            {
                Log.Info("Navigating to URL " + data);
                ExtentReporter.NodeInfo("Navigating to URL " + data);
                ((IJavaScriptExecutor)driver).ExecuteScript("return window.stop;");

                string currentURL = driver.Url;
                if (!currentURL.Equals(data))
                {
                    //ReadOnlyCollection<Cookie> cookies = driver.Manage().Cookies.AllCookies;
                    //int count = cookies.Count;

                    //foreach (Cookie cookie in cookies)
                    //{
                    //    driver.Manage().Cookies.AddCookie(cookie);
                    //}

                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Constants.NavigationTimeout);
                    //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Constants.NavigationTimeout);
                    driver.Url = data;
                    //driver.Navigate().GoToUrl(data);
                }
                else
                {
                    driver.Navigate().Refresh();
                }
               
            }
            catch (Exception e)
            {
                Log.Error("Not able to navigate to URL | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Not able to navigate to URL | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }

        public static void CloseBrowser(String obj, String data)
        {
            try
            {
                Log.Info("Closing the browser ");
                ExtentReporter.NodeInfo("Closing the browser ");
                
                //driver.Close();
                driver.Quit();
            }
            catch (Exception e)
            {
                Log.Error("Not able to Close the Browser | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to Close the Browser | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }

        public class ChromeOptionsWithPrefs : ChromeOptions
        {
            public Dictionary<string, object> prefs { get; set; }
        }
    }
}
