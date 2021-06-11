using System;
using System.IO;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using Bravo.Automation.Config;
using Bravo.Automation.Utilities;
using Bravo.Automation.Execution;

namespace Bravo.Automation.ActionKeywords
{
    public partial class Keywords
    {
        private static IWebDriver driver;
        
        private static AndroidDriver<AndroidElement> appiumdriver;

        private static FirefoxDriver BuildFirefoxDriver()
        {
            var options = new FirefoxOptions();
            if (Constants.Headless)
            {
                options.AddArgument("-headless");
            }
            options.AddArgument("-foreground");
            return new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(Constants.NavigationTimeout));
        }

        private static ChromeDriver BuildChromeDriver()
        {
            var options = new ChromeOptionsWithPrefs();
            options.AddArguments("--start-maximized");
            options.AddArgument("--enable-automation");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-infobars");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-browser-side-navigation");
            options.AddArgument("--disable-gpu");
            
            if (Constants.Headless)
            {
                options.AddArguments("--headless");
            }

            options.prefs = new Dictionary<string, object>
                    {
                        { "profile.default_content_settings.popups", 0 },
                        { "download.prompt_for_download","false" }
                    };

            return new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromSeconds(Constants.NavigationTimeout));
        }
 
        private static RemoteWebDriver BuildRemoteDriver(string browser)
        {
            var DOCKER_GRID_HUB_URI = new Uri("http://localhost:4444/wd/hub");

            RemoteWebDriver driver;

            switch (browser)
            {
                case "chrome":
                    var chromeOptions = new ChromeOptions
                    {
                        BrowserVersion = "",
                        PlatformName = "LINUX",
                    };

                    chromeOptions.AddArgument("--start-maximized");

                    driver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, chromeOptions.ToCapabilities());
                    break;

                case "firefox":
                    var firefoxOptions = new FirefoxOptions
                    {
                        BrowserVersion = "",
                        PlatformName = "LINUX",
                    };

                    driver = new RemoteWebDriver(DOCKER_GRID_HUB_URI, firefoxOptions.ToCapabilities());
                    break;

                default:
                    throw new ArgumentException($"{browser} is not supported remotely.");
            }

            return driver;
        }

        private static IWebDriver BuildDriver(string type,string browser)
        {
            if (type == "local")
            {
                switch (browser)
                {
                    case "Chrome":
                        return BuildChromeDriver();
                    case "Firefox":
                        return BuildFirefoxDriver();
                    default:
                        throw new ArgumentException($"{browser} is not supported locally.");
                }
            }
            else if (type == "remote")
            {
                return BuildRemoteDriver(browser);
            }
            else
            {
                throw new ArgumentException($"{Constants.DriverType} is invalid. Choose 'local' or 'remote'.");
            }
            
        }    

        private class ChromeOptionsWithPrefs : ChromeOptions
        {
            public Dictionary<string, object> prefs { get; set; }
        }

        /// <summary>
        /// appium android capabilities
        /// </summary>
        private static void OpenAndroidDriver(string devicename, string udid, string platformversion, string apppath)
        {
            var appiumoption = new AppiumOptions();
            appiumoption.AddAdditionalCapability(MobileCapabilityType.DeviceName, devicename);
            appiumoption.AddAdditionalCapability(MobileCapabilityType.Udid, udid);
            appiumoption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
            appiumoption.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, platformversion);
            appiumoption.AddAdditionalCapability(MobileCapabilityType.App, apppath);
            appiumoption.AddAdditionalCapability(MobileCapabilityType.NoReset, true);

            appiumdriver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), appiumoption);
        }

        /// <summary>
        /// Takes a screenshot of the current page as a .png file.
        /// <para>Example: Driver.TakeScreenshot("~/pics/ss", "example")</para>
        /// <para>This saves the screenshot as ~/pics/ss/example.png</para>
        /// </summary>
        /// <param name="directory">Directory to save the file to.</param>
        /// <param name="imgName">Image name without .png extension.</param>
        public static void TakeScreenshot(string directory, string imgName)
        {
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            var ssFileName = Path.Combine(directory, imgName);
            ss.SaveAsFile($"{ssFileName}.png", ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Takes a screenshot of the current page as a .png file and
        /// saves it in the current test's auto-generated directory.
        /// </summary>
        /// <param name="imgName">Image name without .png extension.</param>
        public static void TakeScreenshot(string imgName)
        {
            var ss = ((ITakesScreenshot)driver).GetScreenshot();
            var ssFileName = Path.Combine("", imgName);
            ss.SaveAsFile($"{ssFileName}.png", ScreenshotImageFormat.Png);
        }

        #region Public methods
        public static void OpenBrowser(String obj, String data)
        {
            Log.Info($"Opening Browser {data}");
            ExtentReporter.NodeInfo($"Opening Browser {data}");
            try
            {
                driver = BuildDriver(Constants.DriverType, data);

                Log.Info($"Browser {data} Opened");
                ExtentReporter.NodeInfo($"Browser {data} Opened");
            }
            catch (Exception e)
            {
                Log.Error($"Failed OpenBrowser | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed OpenBrowser | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        public static void CloseBrowser(String obj, String data)
        {
            try
            {
                if (driver != null)
                {
                    Log.Info("Closing Browser ");
                    ExtentReporter.NodeInfo("Closing Browser ");
                    //driver.Quit();
                    driver.Close();

                    Log.Info("Browser Closed");
                    ExtentReporter.NodeInfo("Browser Closed");
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to Close the Browser | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to Close the Browser | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }
        
        public static void RefreshBrowser(String obj, String data)
        {
            try
            {
                Log.Info($"Refreshing Browser");
                ExtentReporter.NodeInfo($"Refreshing Browser");

                driver.Navigate().Refresh();
            }
            catch (Exception e)
            {
                Log.Error($"Failed RefreshBrowser | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Failed RefreshBrowser | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        public static void NavigateToURL(String obj, String data)
        {
            try
            {
                Log.Info($"Navigating to URL {data}");
                ExtentReporter.NodeInfo($"Navigating to URL {data}");

                ((IJavaScriptExecutor)driver).ExecuteScript("return window.stop;");

                string currentURL = driver.Url;
                if (!currentURL.Equals(data))
                {
                    driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(Constants.NavigationTimeout);
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
                DriverScript.iOutcome = 3;
            }
        }
        
        public static void OpenMobileApp(String obj, String data)
        {
            Log.Info("Opening App");
            ExtentReporter.NodeInfo("Opening App");

            try
            {
                if (data.Equals(OS.Android.ToString()))
                {
                    OpenAndroidDriver(Constants.DeviceName, Constants.Udid, Constants.PlatformVersion, Constants.AndroidAppapk);
                }
                else if (data.Equals(OS.IOS.ToString()))
                {
                    //IOS
                }

                Log.Info("App Opened");
                ExtentReporter.NodeInfo("App Opened");
            }
            catch (Exception e)
            {
                Log.Error($"Failed OpenApp | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed OpenApp | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        public static void CloseMobileApp(String obj, String data)
        {
            try
            {
                Log.Info("Closing App");
                ExtentReporter.NodeInfo("Closing App");

                appiumdriver.Quit();

                Log.Info("App Closed");
                ExtentReporter.NodeInfo("App Closed");
            }
            catch (Exception e)
            {
                Log.Error($"Failed CloseApp | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed CloseApp | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        #endregion
    }
}
