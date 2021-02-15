using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;

namespace Bravo.Automation.ActionKeywords
{
    public static class DriverActions
    {
        [ThreadStatic] static IWebDriver _driver;

        [ThreadStatic] public static Wait Wait;
        public static void Init(string type, string browser, int setWait)
        {
            _driver = DriverFactory.Build(type, browser);
            //Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(setWait));
            Wait = new Wait(setWait);
        }
        /// <summary>
        /// Gets the current instance of IWebDriver.
        /// </summary>
        public static IWebDriver Current =>
            _driver ?? throw new Exception("_driver is null. Call Driver.Init() first.");

        /// <summary>
        /// Gets the title of the current page.
        /// </summary>
        public static string Title => Current.Title;

        /// <summary>
        /// Takes a screenshot of the current page as a .png file.
        /// <para>Example: Driver.TakeScreenshot("~/pics/ss", "example")</para>
        /// <para>This saves the screenshot as ~/pics/ss/example.png</para>
        /// </summary>
        /// <param name="directory">Directory to save the file to.</param>
        /// <param name="imgName">Image name without .png extension.</param>
        public static void TakeScreenshot(string directory, string imgName)
        {
            var ss = ((ITakesScreenshot)Current).GetScreenshot();
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
            var ss = ((ITakesScreenshot)Current).GetScreenshot();
            var ssFileName = Path.Combine("", imgName);
            ss.SaveAsFile($"{ssFileName}.png", ScreenshotImageFormat.Png);
        }

        /// <summary>
        /// Executes the javascript against the current page and returns an object.
        /// Access the args by indexing into the "arguments" array.
        /// <para>Example: var five = Driver.ExecuteScript&lt;int&gt;("return 3 + arguments[0]", 2);</para>
        /// </summary>
        /// <returns>An object of type T.</returns>
        /// <param name="js">Javascript to execute.</param>
        /// <param name="args">Arguments to be passed to script.</param>
        /// <typeparam name="T">The return type of the script.</typeparam>
        public static T ExecuteScript<T>(string js, params object[] args)
        {
            return Current.ExecuteJavaScript<T>(js, args);
        }

        /// <summary>
        /// Executes the javascript against the current page.
        /// Access the args by indexing into the "arguments" array.
        /// <para>Example: Driver.ExecuteScript("console.log(arguments[0])", "foo");</para>
        /// </summary>
        /// <param name="js">Javascript to execute.</param>
        /// <param name="args">Arguments to be passed to script.</param>
        public static void ExecuteScript(string js, params object[] args)
        {
            Current.ExecuteJavaScript(js, args);
        }
    }
}
