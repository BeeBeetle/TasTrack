using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    public class GlobalVar
    {
        //Below are a list of variables I need for the login page
        //All of these can be used outside this class as well using the public var
        private static bool loginStatus = false;
        public static bool isLoggedIn
        {
            //Tracks when a user is logged in as y or n to determine if
            //they should move from the login page to the main menu
            get { return loginStatus; }
            set { loginStatus = value; }
        }

        private static string activeUser = null;
        public static string displayName
        {
            //Sets display name at login so that on the main menu
            //it will say User: <username> at the top of the page
            get { return activeUser; }
            set { activeUser = value; }
        }

        private static string tempPath = null;
        public static string filePath
        {
            //Determines the file path here at login either when creating account
            //or when logging into an account (nothing currently writes to any file)
            get { return tempPath; }
            set { tempPath = value; }
        }

        public static string dir = AppContext.BaseDirectory;
        public static string profiles = dir + @"profiles\";

        private static int userError = -1;
        public static int errorNumber
        {
            //This tracks the errors for the login page, there are only 3
            get { return userError; }
            set { userError = value; }
        }

        private static bool isCalendar = false;
        public static bool calView
        {
            //Lets us which menu we are on, this one is for the calendar menu
            get { return isCalendar; }
            set { isCalendar = value; }
        }

        public string date = DateTime.Now.ToString();//The date and time right NOW
        public int monthListNum = DateTime.Now.Month - 1;//Used to pull the month from an indexed list so 0 is January
        public string[] dateArray = DateTime.Now.ToString("D").Replace(", ", ",").Split(",");//Make the date an array to get just month, dd for display purposes
        public string[] dayNumber = DateTime.Now.ToString("D").Replace(", ", " ").Split(" ");

        public int screenWidth = Console.WindowWidth;//Listed as a number of columns, each column is the width of one constant width character

        private static bool isTaskList = false;
        public static bool taskList
        {
            //Lets us track what menu we are on, this one is for the Task Menu
            get { return isTaskList; }
            set { isTaskList = value; }
        }
    }
}