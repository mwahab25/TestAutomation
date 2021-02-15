using System;
using System.IO;

namespace Bravo.Automation.Config
{
    public class Constants
    {
        public static string assemblyDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        public static string projectDir = Path.GetDirectoryName(Path.GetDirectoryName(assemblyDir));

        //System Variables
        public static string WORKSPACE_DIRECTORY => Path.GetFullPath(@"../../");

        public static string Path_E2ETestData = Path.Combine(projectDir, "Creation\\BravoE2E.xlsx");
        public static string Drag_Drop = Path.Combine(projectDir, "Files\\dragdrop.js");
        public static string Mobile_Appapk = Path.Combine(projectDir, "Files\\Bravo-Product-v5.1.4.apk");
        public static string Chrome_driver = Path.Combine(projectDir, "Files\\chromedriver.exe");
        public static string Path_Report = Path.Combine(projectDir + "\\Files");

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

        public static string DriverType = "local";
        public static double Timeout = 10;
        public static double NavigationTimeout = 150;
    }
}
