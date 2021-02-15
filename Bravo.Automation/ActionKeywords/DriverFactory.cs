using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;
using Bravo.Automation.Config;

namespace Bravo.Automation.ActionKeywords
{
    public static class DriverFactory
    {
        public static IWebDriver Build(string type, string browser)
        {
            if (type == "local")
            {
                switch (browser)
                {
                    case "chrome":
                        return BuildChromeDriver();

                    case "firefox":
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
                throw new ArgumentException($"{type} is invalid. Choose 'local' or 'remote'.");
            }
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

        private static ChromeDriver BuildChromeDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            return new ChromeDriver(Constants.WORKSPACE_DIRECTORY + "Files", options);
        }

        private static FirefoxDriver BuildFirefoxDriver()
        {
            return new FirefoxDriver(Constants.WORKSPACE_DIRECTORY + "Files");
        }

        private static SafariDriver BuildSafariDriver()
        {
            return new SafariDriver(Constants.WORKSPACE_DIRECTORY + "Files");
        }
        
        private static EdgeDriver BuildEdgeDriver()
        {
            return new EdgeDriver(Constants.WORKSPACE_DIRECTORY + "Files");
        }
    }
}
