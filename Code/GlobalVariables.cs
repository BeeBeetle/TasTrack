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
        private static string loginStatus = "n";
        public static string isLoggedIn
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
    }
}