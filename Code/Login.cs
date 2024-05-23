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
        Printer loginPrinter = new Printer();
        public void GetLogin()
        {
            loginPrinter.menuText = "1: Login to profile\n2: Create new profile\n3: Exit";
            if (GlobalVar.errorNumber == 0) 
            {
                loginPrinter.oopsyDesc = "Oops! Please select a valid option.";
            }
            if (GlobalVar.errorNumber == 1) 
            { 
                loginPrinter.oopsyDesc = "That profile doesn't exist.";
            }
            if (GlobalVar.errorNumber == 2)
            {
                loginPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid.";
            }
            if (GlobalVar.errorNumber == 3)
            {
                loginPrinter.oopsyDesc = "Your passwords do not match. Please try again.";
            }
            if (GlobalVar.errorNumber == 4)
            {
                loginPrinter.oopsyDesc = "Shit! Something broke, bad!";
            }
            string profiles = GlobalVar.profiles;
            loginPrinter.Print();
            try
            {
                GlobalVar.errorNumber = -1;
                int select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        Console.Write("Name: ");
                        string usernameInput = Console.ReadLine();
                        GlobalVar.displayName = usernameInput;
                        GlobalVar.filePath = profiles + usernameInput + "Profile.txt";
                        string passwordInput = null;
                        string storedHash = null;
                        if (File.Exists(GlobalVar.filePath))
                        {
                            using (StreamReader reader = new StreamReader(GlobalVar.filePath))
                            {
                                string line;
                                char[] delimChars = { '/' };
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
                                GlobalVar.isLoggedIn = "y";
                            }
                            else
                            {
                                GlobalVar.errorNumber = 2;
                                break;
                            }
                            passwordInput = null;
                        }
                        else
                        {
                            GlobalVar.errorNumber = 1;
                            break;
                        }
                        break;
                    case 2:
                        Console.Write("\nWhat is your name? ");
                        string newUsername = Console.ReadLine();
                        GlobalVar.filePath = profiles + newUsername + "Profile.txt";
                        if (File.Exists(GlobalVar.filePath))
                        {
                            GlobalVar.errorNumber = 2;
                            break;
                        }
                        else
                        {
                            GlobalVar.displayName = newUsername;
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
                            using (var userFile = File.Create(GlobalVar.filePath)) ;
                            {
                                using (StreamWriter writer = new StreamWriter(GlobalVar.filePath, true))
                                {
                                    writer.WriteLine("U|/" + GlobalVar.displayName);
                                    writer.WriteLine("P|/" + SecretHasher.Hash(newPasswordConfirm));
                                }
                            }
                            GlobalVar.isLoggedIn = "y";
                        }
                        else
                        {
                            GlobalVar.errorNumber = 3;
                        }
                        break;
                    case 3:
                        Console.WriteLine("\nGoodbye! (:");
                        Thread.Sleep(2000);
                        Environment.Exit(0);
                        break;
                    default:
                        GlobalVar.errorNumber = 0;
                        break;
                }
            }
            catch
            {
                GlobalVar.errorNumber = 0;
            }
        }
    }
}
