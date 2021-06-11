using System;
using System.IO;

namespace Bravo.Automation.Config
{
    public class Constants
    {
        //System Variables
        public static string assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string projectDir = Path.GetDirectoryName(Path.GetDirectoryName(assemblyDir));
        public static string Path_E2ETestData = Path.Combine(projectDir, "Creation\\BravoE2E.xlsx");      
        public static string Path_Report = Path.Combine(projectDir, "TestResults\\Report" + DateTime.Now.ToString("_MMddyyyy_hhmmtt") + ".html");
        public static string Drag_Drop = Path.Combine(projectDir, "Utilities\\dragdrop.js");

        //Data Excel sheets
        public static string Sheet_PagesObjects = "Pages Objects";
        public static string Sheet_TestCases = "Test Cases";
        public static string Sheet_TestSteps = "Test Steps";

        //Data Test Steps Sheet Column Numbers
        public static int Col_TestCaseID = 0;
        public static int Col_TestStepID = 1;
        public static int Col_TestStepDesc = 2;
        public static int Col_PageObject = 4;
        public static int Col_ActionKeyword = 5;
        public static int Col_DataSet = 6;
        public static int Col_TestStepResult = 7;

        //Data Test Cases Sheet Column Numbers 
        public static int Col_ID = 0;
        public static int Col_Title = 1;
        public static int Col_Description = 2;
        public static int Col_RunMode = 3;
        public static int Col_Result = 4;

        //Driver config
        public static string DriverType = "local";
        public static double Timeout = 60;
        public static double NavigationTimeout = 200;
        public static bool Headless = false;

        //Android Emulator config
        public static string DeviceName = "PhoneOreo8.1";
        public static string Udid = "emulator-5554";
        public static string PlatformVersion = "8.1";
        public static string AndroidAppapk = Path.Combine(projectDir, "Resources\\Bravo-Product-v5.1.4.apk");

    }
}
