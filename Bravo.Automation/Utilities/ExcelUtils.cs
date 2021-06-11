using System;
using Excel = Microsoft.Office.Interop.Excel;
using Bravo.Automation.Config;
using Bravo.Automation.Execution;

namespace Bravo.Automation.Utilities
{
    public class ExcelUtils
    {
        public static Excel.Application ExcelApp;
        public static Excel.Workbook ExcelWBook;
        private static Excel.Worksheet ExcelWSheet;

        /// <summary>
        /// Open specific Excel workbook
        /// </summary>
        public static void SetExcelFile(String path)
        {
            try
            {
                ExcelApp = new Excel.Application();
                ExcelApp.Visible = false;
                ExcelWBook = ExcelApp.Workbooks.Open(path);
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-SetExcelFile | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
        }

        /// <summary>
        /// Get cell data from specific Excel sheet
        /// </summary>
        public static string GetCellData(int rowNum, int colNum, String sheetName)
        {           
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                string cellValue = (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Text as string;
                return cellValue;
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-GetCellData | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
                return "";
            }
        }

        /// <summary>
        /// Get used rows count from specific Excel sheet
        /// </summary>
        public static int GetRowCount(String sheetName)
        {
            int number = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                number = ExcelWSheet.UsedRange.Rows.Count+1;
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-GetRowCount | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
            return number;
        }

        /// <summary>
        /// Get specific cell contains based on test case from specific Excel sheet
        /// </summary>
        public static int GetRowContains(String testCaseName, int colNum, String sheetName)
        {
            int rowNum = 0;
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                int rowCount = GetRowCount(sheetName);

                for (; rowNum < rowCount; rowNum++)
                {
                    if (GetCellData(rowNum, colNum, sheetName).Equals(testCaseName))
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-GetRowContains | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }
            return rowNum;
        }

        /// <summary>
        /// Get number of steps of test case from specific Excel sheet
        /// </summary>
        public static int GetTestStepsCount(String sheetName, String testCaseID, int testCaseStart)
        {
            try
            {
                for (int i = testCaseStart; i <= ExcelUtils.GetRowCount(sheetName); i++)
                {
                    if (!testCaseID.Equals(ExcelUtils.GetCellData(i, Constants.Col_TestCaseID, sheetName)))
                    {
                        int number = i;
                        return number;
                    }
                }
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                int number1 = ExcelWSheet.UsedRange.Rows.Count + 1;
                return number1;
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-GetRowContains | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
                return 0;
            }
        }

        /// <summary>
        /// Set data into cell in specific Excel sheet
        /// </summary>
        public static void SetCellData(String Result, int rowNum, int colNum, String sheetName)
        {
            try
            {
                ExcelWSheet = ExcelWBook.Sheets[sheetName] as Excel.Worksheet;
                (ExcelWSheet.Cells[rowNum + 1, colNum + 1] as Excel.Range).Value = Result;
            }
            catch (Exception e)
            {
                Log.Error("ExcelUtils-SetCellData | Exception: " + e.Message);
                DriverScript.iOutcome = 3;
            }

        }
    }
}
