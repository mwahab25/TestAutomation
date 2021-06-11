using System;
using System.IO;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using Bravo.Automation.Execution;
using Bravo.Automation.Utilities;
using Bravo.Automation.Creation;
using System.Collections;
using System.Collections.Generic;
using Bravo.Automation.Config;
using OpenQA.Selenium.Interactions;

namespace Bravo.Automation.ActionKeywords
{
    public partial class Keywords
    {
        private static By LocateValue(string locatortype,string value)
        {
            By by;
            switch (locatortype)
            {
                case "xpath":
                    by = By.XPath(value);
                    break;
                case "id":
                    by = By.Id(value);
                    break;
                case "csslocator":
                    by = By.CssSelector(value);
                    break;
                case "classname":
                    by = By.ClassName(value);
                    break;
                case "linktext":
                    by = By.LinkText(value);
                    break;
                case "name":
                    by = By.Name(value);
                    break;
                case "partiallinktext":
                    by = By.PartialLinkText(value);
                    break;
                default:
                    by = null;
                    break;
            }
            return by;
        }
       
        private static string GetKey(String obj)
        {
            return Locators.Default.Properties[obj].DefaultValue as string;
        }

        private static bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException e)
            {
                Log.Info($"No able to find element by IsElementPresent | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"No able to find element by IsElementPresent | Exception: {e.Message}");
                return false;
            }
        }

        private static bool CheckElement(By by)
        {
            bool CH1 = false;
            try
            {
                CH1 = IsElementPresent(By.CssSelector("svg"));
            }
            catch { }
            bool CH2;
            if (driver.FindElement(By.CssSelector("svg")).Displayed)
            {
                CH2 = true;
            }
            else
            {
                CH2 = false;
            }
            bool CH3;
            try
            {
                CH3 = driver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                CH3 = false;
            }

            if (!CH1 && !CH2 && CH3)
                return true;
            else
                return false;
        }       

        private static void ScrollIntoView(By by)
        {
            try
            {
                Log.Info("ScrollIntoView ..");
                ExtentReporter.NodeInfo("ScrollIntoView ..");

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", driver.FindElement(by));
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ScrollIntoView | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ScrollIntoView | Exception: {e.Message}");
            }
        }

        private static bool ClickByDriver(By by)
        {
            try
            {
                Log.Info("ClickByDriver ..");
                ExtentReporter.NodeInfo("ClickByDriver ..");

                driver.FindElement(by).Click();
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClickByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClickByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool ClickByDriver(IWebElement element)
        {
            try
            {
                Log.Info("ClickByDriver ..");
                ExtentReporter.NodeInfo("ClickByDriver ..");

                element.Click();
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClickByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClickByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool ClickByJavascript(By by)
        {
            try
            {
                Log.Info("ClickByJavascript ..");
                ExtentReporter.NodeInfo("ClickByJavascript ..");

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[arguments.length - 1].click();", driver.FindElement(by));
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClickByJavascript | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClickByJavascript | Exception: {e.Message}");
                return false;
            }
        }

        private static bool ClickByJavascript(IWebElement element)
        {
            try
            {
                Log.Info("ClickByJavascript ..");
                ExtentReporter.NodeInfo("ClickByJavascript ..");
                
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[arguments.length - 1].click();", element);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClickByJavascript | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClickByJavascript | Exception: {e.Message}");
                return false;
            }
        }

        private static bool ClearByDriver(By by, String data)
        {
            try
            {
                Log.Info("ClearByDriver ..");
                ExtentReporter.NodeInfo("ClearByDriver ..");

                driver.FindElement(by).Clear();
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClearByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClearByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool InputByDriver(By by, String data)
        {
            try
            {
                Log.Info("InputByDriver ..");
                ExtentReporter.NodeInfo("InputByDriver ..");
               
                driver.FindElement(by).SendKeys(data);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to InputByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to InputByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool InputByJavascript(By by, String data)
        {
            try
            {
                Log.Info("InputByJavascript ..");
                ExtentReporter.NodeInfo("InputByJavascript ..");
                
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("arguments[0].value='" + data + "';",driver.FindElement(by));
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to InputByJavascript | Exception: { e.Message}");
                ExtentReporter.NodeInfo($"Not able to InputByJavascript | Exception: {e.Message}");
                return false;
            }
        }

        private static bool SelectTextByDriver(By by,string data)
        {
            try
            {
                Log.Info("SelectTextByDriver ..");
                ExtentReporter.NodeInfo("SelectTextByDriver ..");
                
                new SelectElement(driver.FindElement(by)).SelectByText(data);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to SelectTextByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to SelectTextByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool SelectValueByDriver(By by, string data)
        {
            try
            {
                Log.Info("SelectValueByDriver ..");
                ExtentReporter.NodeInfo("SelectValueByDriver ..");
                
                new SelectElement(driver.FindElement(by)).SelectByValue(data);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to SelectValueByDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to SelectValueByDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static string GetElementTextByType(By by, String type)
        {
            String txt = "";
            switch (type)
            {
                case "text":
                    txt = driver.FindElement(by).Text;
                    break;
                case "textContent":
                    txt = driver.FindElement(by).GetAttribute("textContent");
                    break;
                case "value":
                    txt = driver.FindElement(by).GetAttribute("value");
                    break;
                default:
                    break;
            }
            return txt;
        }

        private static string GetTextByDriver(By by)
        {
            string elementTxt = "";
            string[] types = { "text", "textContent", "value" };
            for (int i = 0; i < types.Length; i++)
            {
                if (!GetElementTextByType(by, types[i]).Trim().Equals(""))
                {
                    elementTxt = GetElementTextByType(by, types[i]).Trim();
                    break;
                }
            }
            Console.WriteLine(elementTxt);
            return elementTxt;
        }

        private static string GetTagNameByDriver(By by)
        {
            string elementTagName = driver.FindElement(by).TagName;
            return elementTagName;
        }

        private static String GetSizeByDriver(By by)
        {
            String elementSize = driver.FindElement(by).Size.ToString();
            return elementSize;
        }

        private static String GetAttributeByDriver(By by, String attributeName)
        {
            String elementAttribute = driver.FindElement(by).GetAttribute(attributeName);
            return elementAttribute;
        }

        private static bool InputByappiumDriver(By by, String data)
        {
            try
            {
                Log.Info("InputByappiumDriver ..");
                ExtentReporter.NodeInfo("InputByappiumDriver ..");
                           
                appiumdriver.FindElement(by).SendKeys(data);
                return true;

            }
            catch (Exception e)
            {
                Log.Info($"Not able to InputByappiumDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to InputByappiumDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool ClickByappiumDriver(By by)
        {
            try
            {
                Log.Info("ClickByappiumDriver ..");
                ExtentReporter.NodeInfo("ClickByappiumDriver ..");

                appiumdriver.FindElement(by).Click();
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to ClickByappiumDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to ClickByappiumDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool SelectTextByappiumDriver(By by, string data)
        {
            try
            {
                Log.Info("SelectTextByappiumDriver ..");
                ExtentReporter.NodeInfo("SelectTextByappiumDriver ..");

                new SelectElement(appiumdriver.FindElement(by)).SelectByText(data);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to SelectTextByappiumDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to SelectTextByappiumDriver | Exception: {e.Message}");
                return false;
            }
        }

        private static bool SelectValueByappiumDriver(By by, string data)
        {
            try
            {
                Log.Info("SelectValueByappiumDriver ..");
                ExtentReporter.NodeInfo("SelectValueByappiumDriver ..");

                new SelectElement(appiumdriver.FindElement(by)).SelectByValue(data);
                return true;
            }
            catch (Exception e)
            {
                Log.Info($"Not able to SelectValueByappiumDriver | Exception: {e.Message}");
                ExtentReporter.NodeInfo($"Not able to SelectValueByappiumDriver | Exception: {e.Message}");
                return false;
            }
        }

        #region Public methods
        public static void Click(String obj, String data)
        {
            Log.Info($"Clicking on Element {obj}");
            ExtentReporter.NodeInfo($"Clicking on Element {obj}");

            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                if (locator[0] == "Mobile")
                {
                    WaitUntilVisible(by, appiumdriver);
                    WaitSeconds("", "2");

                   if(!ClickByappiumDriver(by))
                   {
                        Log.Error("Failed ClickByappiumDriver");
                        ExtentReporter.NodeError("Failed ClickByappiumDriver");
                        DriverScript.iOutcome = 3;
                    }
                }
                else
                {
                    WaitUntilClickable(by, driver);
                    WaitSeconds("", "2");

                    if (!ClickByDriver(by))
                    {
                        if (!ClickByJavascript(by))
                        {
                            Log.Error("Failed ClickByDriver and ClickByJavascript");
                            ExtentReporter.NodeError("Failed ClickByDriver and ClickByJavascript");
                            DriverScript.iOutcome = 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed Click | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed Click | Exception: {e.Message}");
                ExtentReporter.AddScreenShot("");
                DriverScript.iOutcome = 3;
            }
        }      

        public static void Input(String obj, String data)
        {
            Log.Info("Typing in Element .. " + obj);
            ExtentReporter.NodeInfo("Typing in Element .. " + obj);
            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                if (locator[0] == "Mobile")
                {
                    WaitUntilVisible(by, appiumdriver);
                    WaitSeconds("", "2");

                    if (!InputByappiumDriver(by, data)) 
                    {
                        Log.Error("Failed InputByappiumDriver");
                        ExtentReporter.NodeError("Failed InputByappiumDriver");
                        DriverScript.iOutcome = 3;
                    }
                }
                else
                {
                    WaitUntilClickable(by,driver);
                    WaitSeconds("", "2");

                    if (!InputByDriver(by, data))
                    {
                        if (!InputByJavascript(by, data))
                        {
                            Log.Error("Failed InputByDriver and InputByJavascript");
                            ExtentReporter.NodeError("Failed InputByDriver and InputByJavascript");
                            DriverScript.iOutcome = 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed Input | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed Input | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        public static void Select(String obj, String data)
        {
            Log.Info($"Selecting from dropdown Element {obj}");
            ExtentReporter.NodeInfo($"Selecting from dropdown Element {obj}");

            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                if (locator[0] == "Mobile")
                {
                    WaitUntil(by, appiumdriver);
                    if (!SelectTextByappiumDriver(by, data))
                    {
                        if (!SelectValueByappiumDriver(by, data))
                        {
                            Log.Error("Failed SelectTextByappiumDriver and SelectValueByappiumDriver");
                            ExtentReporter.NodeError("Failed SelectTextByappiumDriver and SelectValueByappiumDriver");
                            DriverScript.iOutcome = 3;
                        }
                    }
                }
                else
                {
                    WaitUntil(by, driver);
                    if (!SelectTextByDriver(by, data))
                    {
                        if (!SelectValueByDriver(by, data))
                        {
                            Log.Error("Failed SelectTextByDriver and SelectValueByDriver");
                            ExtentReporter.NodeError("Failed SelectTextByDriver and SelectValueByDriver");
                            DriverScript.iOutcome = 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error($"Failed Select | Exception: {e.Message}");
                ExtentReporter.NodeError($"Failed Select | Exception: {e.Message}");
                DriverScript.iOutcome = 3;
            }
        }

        public static void DragDropTrad(String obj, String data)
        {
            Log.Info("Draging Webelement " + obj);
            ExtentReporter.NodeInfo("Draging Webelement " + obj);
            try
            {
                string[] locator1 = obj.Split('_');
                string[] locator2value = data.Split('_');

                By byDragElement = LocateValue(locator1[1], GetKey(obj));
                By byDropValue = LocateValue(locator2value[1], GetKey(data));

                IWebElement source;
                IWebElement target;

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                WaitUntilExists(byDragElement,driver);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue,driver);
                if (IsElementPresent(byDragElement))
                {
                    target = driver.FindElement(byDropValue);
                }
                else
                {
                    WaitForElement(byDragElement);
                    target = driver.FindElement(byDropValue);
                }

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                if (File.Exists(Constants.Drag_Drop))
                {
                    WaitSeconds("", "3");
                    string dragAndDropScript = File.ReadAllText(Constants.Drag_Drop);
                    jse.ExecuteScript(dragAndDropScript, source, target);
                    WaitSeconds("", "3");
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to drag and drop | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to drag and drop | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }

        public static void DragDropDist(String obj, String data)
        {
            Log.Info("Draging Webelement " + obj);
            ExtentReporter.NodeInfo("Draging Webelement " + obj);
            try
            {
                string[] locator1 = obj.Split('_');
                string[] locator2value = data.Split('_');

                By byDragElement = LocateValue(locator1[1], GetKey(obj));

                string newgetdata;
                if(GetKey(data).Contains("#"))
                {
                    newgetdata=GetKey(data).Replace("#", DateTime.Today.Date.ToString("dd/MM/yyyy"));
                }
                else
                {
                    newgetdata = GetKey(data);
                }

                By byDropValue = LocateValue(locator2value[1], newgetdata);

                IWebElement source;
                IWebElement target;

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                WaitUntilExists(byDragElement,driver);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue,driver);
                if (IsElementPresent(byDragElement))
                {
                    target = driver.FindElement(byDropValue);
                }
                else
                {
                    WaitForElement(byDragElement);
                    target = driver.FindElement(byDropValue);
                }

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                if (File.Exists(Constants.Drag_Drop))
                {
                    WaitSeconds("", "3");
                    string dragAndDropScript = File.ReadAllText(Constants.Drag_Drop);
                    jse.ExecuteScript(dragAndDropScript, source, target);
                    WaitSeconds("", "3");
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to drag and drop | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to drag and drop | Exception: " + e.Message);
                DriverScript.iOutcome = 3;

            }
        }

        public static void DragDropAng(String obj, String data)
        {
            Log.Info("Draging Ang Webelement " + obj);
            ExtentReporter.NodeInfo("Draging Ang Webelement " + obj);
            try
            {
                string[] locator1 = obj.Split('_');
                string[] locator2value = data.Split('_');

                By byDragElement = LocateValue(locator1[1], GetKey(obj));
                By byDropValue = LocateValue(locator2value[1], GetKey(data));

                IWebElement source;
                IWebElement target;

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                WaitUntilExists(byDragElement,driver);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    //WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue,driver);
                if (IsElementPresent(byDragElement))
                {
                    target = driver.FindElement(byDropValue);
                }
                else
                {
                    //WaitForElement(byDragElement);
                    target = driver.FindElement(byDropValue);
                }

                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                if (File.Exists(Constants.Drag_Drop))
                {
                    WaitSeconds("", "3");
                    //string dragAndDropScript = File.ReadAllText(Constants.Drag_Drop);
                    //jse.ExecuteScript(dragAndDropScript, source, target);

                    Actions actions = new Actions(driver);
                    actions.ClickAndHold(source).Perform();
                    actions.MoveByOffset(target.Location.X - source.Location.X, target.Location.Y - source.Location.Y).Perform();
                    actions.Release(target).Perform();

                    WaitSeconds("", "3");
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to drag and drop Ang | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to drag and drop Ang | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }

        public static void CheckCheckbox(String obj, String data)
        {
            Log.Info("CheckCheckbox .. " + obj);
            ExtentReporter.NodeInfo("CheckCheckbox .. " + obj);
            try
            {                            
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                ScrollIntoView(by);
                WaitUntilClickable(by,driver);

                IWebElement chkbx = driver.FindElement(by);
                if (!chkbx.Selected)
                {
                    if (!ClickByDriver(chkbx))
                    {
                        if (!ClickByJavascript(chkbx))
                        {
                            Log.Error("Not able to CheckCheckbox ..");
                            ExtentReporter.NodeError("Not able to CheckCheckbox ..");
                            DriverScript.iOutcome = 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed CheckCheckbox | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed CheckCheckbox | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }    

        public static void KeyPress(String obj, String data)
        {
            Log.Info("KeyPress "+ data +" on .. "+ obj);
            ExtentReporter.NodeInfo("KeyPress " + data + " on .. " + obj);
            try
            {
                string[] locator = obj.Split('_');
                switch (data.ToLower().Trim())
                {
                    case "enter":
                        driver.FindElement(LocateValue(locator[1], GetKey(obj))).SendKeys(Keys.Enter);
                        break;
                    case "return":
                        driver.FindElement(LocateValue(locator[1], GetKey(obj))).SendKeys(Keys.Return);
                        break;
                    case "tab":
                        driver.FindElement(LocateValue(locator[1], GetKey(obj))).SendKeys(Keys.Tab);
                        break;
                    default:
                        Log.Error("Not a key");
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to KeyPress "+ data +" | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to KeyPress " + data + " | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }     
        #endregion
    }
}
