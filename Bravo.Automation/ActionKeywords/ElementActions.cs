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
                Log.Error("No such element found | Exception: " + e.Message);
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
                Log.Error("Failed ScrollIntoView | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ScrollIntoView | Exception: " + e.Message);
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
                Log.Error("Failed ClickByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ClickByDriver | Exception: " + e.Message);
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
                Log.Error("Failed ClickByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ClickByDriver | Exception: " + e.Message);
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
                Log.Error("Failed ClickByJavascript | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ClickByJavascript | Exception: " + e.Message);
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
                Log.Error("Failed ClickByJavascript | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ClickByJavascript | Exception: " + e.Message);
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
                Log.Error("Failed ClearByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed ClearByDriver | Exception: " + e.Message);
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
                Log.Error("Failed InputByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed InputByDriver | Exception: " + e.Message);
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
                Log.Error("Failed InputByJavascript | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed InputByJavascript | Exception: " + e.Message);
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
                Log.Error("Failed SelectTextByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed SelectTextByDriver | Exception: " + e.Message);
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
                Log.Error("Failed SelectValueByDriver | Exception: " + e.Message);
                ExtentReporter.NodeInfo("Failed SelectValueByDriver | Exception: " + e.Message);
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

        public static void Click(String obj, String data)
        {
            Log.Info("Click on Webelement .. " + obj);
            ExtentReporter.NodeInfo("Click on Webelement .. " + obj);
            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                //ScrollIntoView(by);
                //WaitUntil(by);
                WaitUntilClickable(by);
                WaitSeconds("","2");
                if (!ClickByDriver(by))
                {
                    if (!ClickByJavascript(by))
                    {
                        Log.Error("Not able to click ..");
                        ExtentReporter.NodeError("Not able to click ..");
                        DriverScript.bResult = false;
                        DriverScript.bOutcome = 3;
                    }
                }
            }
            catch(Exception e)
            {
                Log.Error("Failed Click on Webelement | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed Click on Webelement | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }      

        public static void Input(String obj, String data)
        {
            Log.Info("Input in Webelement .. " + obj);
            ExtentReporter.NodeInfo("Input in Webelement .. " + obj);
            try
            {
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                //ScrollIntoView(by);
                WaitUntilClickable(by);
                WaitSeconds("", "2");
                if (!InputByDriver(by, data))
                {
                    if (!InputByJavascript(by, data))
                    {
                        Log.Error("Not able to input ..");
                        ExtentReporter.NodeError("Not able to input ..");
                        DriverScript.bResult = false;
                        DriverScript.bOutcome = 3;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed Input in Webelement | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed Input in Webelement | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
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

                WaitUntilExists(byDragElement);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue);
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
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;

               
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

                WaitUntilExists(byDragElement);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue);
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
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;

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

                WaitUntilExists(byDragElement);
                if (IsElementPresent(byDragElement))
                {
                    source = driver.FindElement(byDragElement);
                }
                else
                {
                    WaitForElement(byDragElement);
                    source = driver.FindElement(byDragElement);
                }

                WaitUntilExists(byDropValue);
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
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;


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

                WaitForInvisibilityLoading(out double loadtime);
                Log.Info("Loadtime: " + loadtime);

                ScrollIntoView(by);
                WaitUntilClickable(by);

                IWebElement chkbx = driver.FindElement(by);
                if (!chkbx.Selected)
                {
                    if (!ClickByDriver(chkbx))
                    {
                        if (!ClickByJavascript(chkbx))
                        {
                            Log.Error("Not able to CheckCheckbox ..");
                            ExtentReporter.NodeError("Not able to CheckCheckbox ..");
                            DriverScript.bResult = false;
                            DriverScript.bOutcome = 3;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("Failed CheckCheckbox | Exception: " + e.Message);
                ExtentReporter.NodeError("Failed CheckCheckbox | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }        

        public static void Select(String obj, String data)
        {
            try
            {
                Log.Info("Selecting from drop down list " + obj);
                ExtentReporter.NodeInfo("Selecting from drop down list " + obj);
                string[] locator = obj.Split('_');
                By by = LocateValue(locator[1], GetKey(obj));

                //WaitForInvisibilityLoading(out double loadtime);
                //Log.Info("Loadtime: " + loadtime);

                //ScrollIntoView(by);
                WaitUntil(by);
                if(!SelectValueByDriver(by, data))
                {
                    SelectTextByDriver(by, data);
                }
            }
            catch (Exception e)
            {
                Log.Error("Not able to select from drop down list | Exception: " + e.Message);
                ExtentReporter.NodeError("Not able to select from drop down list | Exception: " + e.Message);
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
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
                DriverScript.bResult = false;
                DriverScript.bOutcome = 3;
            }
        }
    }
}
