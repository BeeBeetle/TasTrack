using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;
using Newtonsoft.Json;

namespace TasTrack
{
    public class Login
    {
        GlobalVal globalVar = new GlobalVal();
        Printer loginPrinter = new Printer();
        public Login()
        {
            loginPrinter.menuText = "1: Login to profile\n2: Create new profile\n3: Exit";
            if (GlobalVal.errorNumber == 0) { loginPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { loginPrinter.oopsyDesc = "I couldn't find that profile."; }
            if (GlobalVal.errorNumber == 2) { loginPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVal.errorNumber == 3) { loginPrinter.oopsyDesc = "Your passwords do not match. Please try again."; }
            if (GlobalVal.errorNumber == 4) { loginPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            string profiles = GlobalVal.profiles;
            loginPrinter.PrintTitle();
            loginPrinter.PrintMenu();
            loginPrinter.PrintChoice();
            try
            {
                GlobalVal.errorNumber = -1;
                int select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        Console.Write("Username: ");
                        string usernameInput = Console.ReadLine();// Our username which is part of the file name for easy finding!
                        GlobalVal.displayName = usernameInput;// Will be used later on other menus, just listing who is logged in
                        GlobalVal.filePath = profiles + usernameInput + "Profile.json";// See! right there in the file name! Plus now its here for later
                        string filename = GlobalVal.filePath.Split('\\').Last();
                        string passwordInput = null;
                        string storedHash = null;
                        string storedName = null;
                        if (File.Exists(GlobalVal.filePath))
                        {// To log in without creating an account we have to have a file already otherwise there is no account (duh!)
                            using (StreamReader reader = new StreamReader(GlobalVal.filePath))
                            {// Read the JSON file for the specified account and then deserialize it so we can access what we need
                                var json = reader.ReadToEnd();
                                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                                storedHash = desrlzdjson.UserInfo.HashPass;// Right now all we need is the hashed password for comparison
                                storedName = desrlzdjson.UserInfo.Username;
                            }
                            if (usernameInput != storedName)
                            {
                                GlobalVal.errorNumber = 1;
                                break;
                            }
                            Console.Write("Password: ");
                            while (true)
                            {
                                var key = Console.ReadKey(true);
                                if (key.Key == ConsoleKey.Enter)
                                {
                                    break;
                                }
                                if (key.Key == ConsoleKey.Backspace)
                                {// This whole mess lets us backspace when typing a password but also keeps it visually anonymous
                                    if (passwordInput == null || passwordInput.Length == 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        Console.Write("\b");
                                        Console.Write(' ');
                                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                                        string backspace = passwordInput.Remove(passwordInput.Length - 1);
                                        passwordInput = backspace;
                                    }
                                }
                                else
                                {
                                    Console.Write("*");
                                    passwordInput += key.KeyChar;
                                }
                            }
                            if (SecretHasher.Verify(passwordInput, storedHash) == true)
                            {// If the password's match then log us in baby!
                                GlobalVal.isLoggedIn = true;// Once we get to the bottom and break we will drop right out of our switch statement
                            }                               // Then we drop right out of this CLASS and Main() sees that we are logged in and gets the main menu
                            else
                            {
                                GlobalVal.errorNumber = 2;
                                break;
                            }
                            passwordInput = null;
                        }
                        else
                        {
                            GlobalVal.errorNumber = 1;
                            break;
                        }
                        break;
                    case 2:
                        Console.Write("\nWhat is your name? ");
                        string newUsername = Console.ReadLine();
                        GlobalVal.filePath = profiles + newUsername + "Profile.json";
                        if (File.Exists(GlobalVal.filePath))
                        {// Just a check to see if a profile already exists, since we check logging in based on username if a user already exists with the name entered
                            GlobalVal.errorNumber = 2;// we can't allow it because the program won't know how to distinguish between two identical file names
                            break;
                        }
                        else
                        {
                            GlobalVal.displayName = newUsername;// Once again will be used later
                        }
                        Console.Write("Please enter a good password: ");
                        string newPasswordTry = null;
                        while (true)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter)
                            {
                                break;
                            }
                            if (key.Key == ConsoleKey.Backspace)
                            {// See this instance of code in case 1
                                Console.Write("\b");
                                Console.Write(' ');
                                Console.SetCursorPosition(Console.CursorLeft-1, Console.CursorTop);
                            }
                            else
                            {
                                Console.Write("*");
                                newPasswordTry += key.KeyChar;
                            }
                        }
                        Console.Write("\nPlease reenter your password: ");
                        string newPasswordConfirm = null;
                        while (true)
                        {
                            var key = Console.ReadKey(true);
                            if (key.Key == ConsoleKey.Enter)
                            {
                                break;
                            }
                            if (key.Key == ConsoleKey.Backspace)
                            {// See the first instance of this code
                                Console.Write("\b");
                                Console.Write(' ');
                                Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            }
                            else
                            {
                                Console.Write("*");
                                newPasswordConfirm += key.KeyChar;
                            }
                        }
                        if (newPasswordTry == newPasswordConfirm)
                        {                              
                            using (StreamWriter writer = new StreamWriter(GlobalVal.filePath, true))
                            {// After making the file above we write to it with JSON.NET with the username and the hashed version of the passowrd
                                Userinfo userinfo = new Userinfo { Username = GlobalVal.displayName, HashPass = SecretHasher.Hash(newPasswordConfirm) };
                                JSONClass userToJson = new JSONClass { UserInfo = userinfo };
                                writer.WriteLine(JsonConvert.SerializeObject(userToJson, Formatting.Indented));
                            }                            
                            GlobalVal.isLoggedIn = true;
                        }
                        else
                        {
                            GlobalVal.errorNumber = 3;
                        }// Clear out our passwords after we are done with them just in case, maybe could be a more secure way to handle the passwords
                        newPasswordTry = null;// Maybe hash them right after they are entered and then do the compare? Not that important for this though
                        newPasswordConfirm = null;
                        break;
                    case 3:
                        Console.WriteLine("\nGoodbye! (:");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        GlobalVal.errorNumber = 0;
                        break;
                }
            }
            catch
            {// Anything that causes an exception will go here and we will get an error message, right now I don't really care what the exception is.
                GlobalVal.errorNumber = 4;// And obviously this program is small enough I don't think it matters two much either. Because I should be
            }// to find all of them through the course of coding and plan around them. So we really shouldn't get anything crazy. Maybe a non English
        }    // machine could throw us off here by giving us exceptions if specific work needs to be done to handle non-Latin characters or something
    }
}
