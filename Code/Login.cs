using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace TasTrack
{
    public class Login
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
        private static int userLoginError = -1;
        public static int errorNumber
        {
        //This tracks the errors for the login page, there are only 3
            get { return userLoginError; }
            set { userLoginError = value; }
        }
        Printer loginPrinter = new Printer();
        public void GetLogin()
        {
            string basePath = @"D:\programs\TasTrack\profiles\";
            loginPrinter.menuText = "1: Login to profile\n2: Create new profile\n3: Exit";
            if (errorNumber == 0) 
            {
                loginPrinter.oopsyDesc = "Oops! Please select a valid option.";
            }
            if (errorNumber == 1) 
            { 
                loginPrinter.oopsyDesc = "That profile doesn't exist.";
            }
            if (errorNumber == 2)
            {
                loginPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid.";
            }
            if (errorNumber == 3)
            {
                loginPrinter.oopsyDesc = "Your passwords do not match. Please try again.";
            }
            loginPrinter.Print();
            int select = Convert.ToInt32(Console.ReadLine());
            switch (select)
            {
                case 1:
                    Console.Write("Name: ");
                    string usernameInput = Console.ReadLine();
                    activeUser = usernameInput;
                    tempPath = basePath + usernameInput + "Profile.txt";
                    string passwordInput = null;
                    string storedHash = null;
                    if (File.Exists(tempPath))
                    {
                        using(StreamReader reader = new StreamReader(tempPath))
                        {
                            string line;
                            char[] delimChars = {'/'};
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] findPass = line.Split(delimChars);
                                if (findPass[0] == "P|")
                                {
                                    storedHash = findPass[1];
                                }
                            }
                        }
                        Console.Write("Password: ");
                        while (true)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter)
                                break;
                            else
                                Console.Write("*");
                            passwordInput += key.KeyChar;
                        }
                        if (SecretHasher.Verify(passwordInput, storedHash) == true)
                        {
                            loginStatus = "y";
                        }
                        else
                        {
                            userLoginError = 2;
                            break;
                        }
                        passwordInput = null;
                    }
                    else
                    {
                        userLoginError = 1;
                        break;
                    }
                    break;
                case 2:
                    Console.Write("\nWhat is your name? ");
                    string newUsername = Console.ReadLine();
                    tempPath = basePath + newUsername + "Profile.txt";
                    if (File.Exists(tempPath))
                    {
                        userLoginError = 2;
                        break;
                    }
                    else
                    {
                        activeUser = newUsername;
                    }
                    Console.Write("Please enter a good password: ");
                    string newPasswordTry = null;
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        else
                            Console.Write("*");
                        newPasswordTry += key.KeyChar;
                    }
                    Console.Write("\nPlease reenter your password: ");
                    string newPasswordConfirm = null;
                    while (true)
                    {
                        var key = Console.ReadKey(true);
                        if (key.Key == ConsoleKey.Enter)
                            break;
                        else
                            Console.Write("*");
                        newPasswordConfirm += key.KeyChar;
                    }
                    if (newPasswordTry == newPasswordConfirm)
                    {
                        using (var userFile = File.Create(tempPath)) ;
                        {
                            using (StreamWriter writer = new StreamWriter(tempPath, true))
                            {
                                writer.WriteLine("U|/" + activeUser);
                                Console.WriteLine(activeUser);
                                writer.WriteLine("P|/" + SecretHasher.Hash(newPasswordConfirm));
                                Console.WriteLine(newPasswordConfirm);
                            }
                        }
                        loginStatus = "y";
                    }
                    else
                    {
                        userLoginError = 3;
                    }
                    break;
                case 3:
                    Console.WriteLine("\nGoodbye! (:");
                    Thread.Sleep(2000);
                    Environment.Exit(0);
                    break;
                default:
                    userLoginError = 0;
                    break;
            }
        }
    }
}
