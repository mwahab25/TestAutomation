using System;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Remote;
using Bravo.Automation.Execution;
using Bravo.Automation.Utilities;
using Bravo.Automation.Config;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium;

namespace Bravo.Automation.ActionKeywords
{ 
    public partial class Keywords
    {
        private static RemoteWebDriver Mobiledriver;
        private static IPerformsTouchActions touchdriver;
        private AppiumDriver<AndroidElement> appiumdriver;

        public void OpenApp(String obj, String data)
        {          
            Log.Info("Opening App ");
            try
            {
                if (data.Equals(MobileOS.Android.ToString()))
                {
                    //DesiredCapabilities caps = new DesiredCapabilities();
                    //caps.SetCapability(MobileCapabilityType.DeviceName, "PhoneOreo8.1");
                    //caps.SetCapability(MobileCapabilityType.Udid, "emulator-5554");
                    //caps.SetCapability(MobileCapabilityType.PlatformName, "Android");
                    //caps.SetCapability(MobileCapabilityType.PlatformVersion, "8.1");
                    ////caps.SetCapability(MobileCapabilityType.BrowserName, MobileBrowserType.Chrome);
                    //caps.SetCapability(MobileCapabilityType.App, Constants.Mobile_Appapk);
                    //caps.SetCapability(MobileCapabilityType.NoReset, true);

                    ////open mobile driver
                    //Mobiledriver = new RemoteWebDriver(new Uri("http://127.0.0.1:4723/wd/hub"), caps);

                    var driveroption = new AppiumOptions();
                    driveroption.AddAdditionalCapability(MobileCapabilityType.DeviceName, "PhoneOreo8.1");
                    driveroption.AddAdditionalCapability(MobileCapabilityType.Udid, "emulator-5554");
                    driveroption.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
                    driveroption.AddAdditionalCapability(MobileCapabilityType.PlatformVersion, "8.1");
                    driveroption.AddAdditionalCapability(MobileCapabilityType.App, Constants.Mobile_Appapk);
                    driveroption.AddAdditionalCapability(MobileCapabilityType.NoReset, true);
                    //driveroption.AddAdditionalCapability("chromedriverExecutable", Constants.Chrome_driver);

                    appiumdriver = new AndroidDriver<AndroidElement>(new Uri("http://127.0.0.1:4723/wd/hub"), driveroption);



                    var contexts = ((IContextAware)appiumdriver).Contexts;
                    string webviewcontext = null;
                    for (var i = 0; i < contexts.Count; i++)
                    {
                        Console.WriteLine(contexts[i]);
                        ExtentReporter.NodeInfo(contexts[i]);
                        if (contexts[i].Contains("WEBVIEW"))
                        {
                            webviewcontext = contexts[i];
                            break;
                        }
                    }
                    ((IContextAware)appiumdriver).Context = webviewcontext;

                    // appiumdriver.FindElementByClassName("android.widget.EditText").SendKeys("admin");

                    // IList<AppiumWebElement> els = Mobiledriver.FindElementsByClassName("android.widget.TextView");

                    //int number1 = els.Count;

                    //TouchAction action = new TouchAction(appiumdriver);
                    //action.Press(223, 1522);
                    //action.MoveTo(874, 1533);
                    //action.Release().;
                    //action.Perform();

                }
                else if (data.Equals(MobileOS.IOS.ToString()))
                {
                    
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to open App | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to open App | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }

        public void CloseApp(String obj, String data)
        {
            try
            {
                Log.Info("Closing App ");
               // Mobiledriver.Quit();
                appiumdriver.Quit();
            }
            catch (Exception e)
            {
                Log.Error("Not able to Close App | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to open App | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }
    }
}
