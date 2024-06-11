using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    public class Printer
    {
        public string title;
        public string oopsyDesc;
        public string menuText;
        public string optionChoice;
        public string mainMenuSum;
        public Printer()
        {
            title = "\n   ______  ______  _____  ___________  ___  _____  _   __" +
                "\r\n  /\\__  _\\/\\  _  \\/\\  _ `\\_   _| ___ \\/ _ \\/  __ \\| | / /" +
                "\r\n  \\/_/\\ \\/\\ \\ \\L\\ \\ \\,\\L\\_\\| | | |_/ / /_\\ \\ /  \\/| |/ / " +
                "\r\n     \\ \\ \\ \\ \\  __ \\/_\\__ \\| | |    /|  _  | |    |    \\ " +
                "\r\n      \\ \\ \\ \\ \\ \\/\\ \\/\\ \\L\\ \\| | |\\ \\| | | | \\__/\\| |\\  \\" +
                "\r\n       \\ \\_\\ \\ \\_\\ \\_\\ `\\____/ \\_| \\_\\_| |_/\\____/\\_| \\_/" +
                "\r\n        \\/_/  \\/_/\\/_/\\/_____/";
            //title = "\n _____ ___   _____ ___________  ___  _____  _   __" +
            //    "\r\n|_   _/ _ \\ /  ___|_   _| ___ \\/ _ \\/  __ \\| | / /" +
            //    "\r\n  | |/ /_\\ \\\\ `--.  | | | |_/ / /_\\ \\ /  \\/| |/ / " +
            //    "\r\n  | ||  _  | `--. \\ | | |    /|  _  | |    |    \\ " +
            //    "\r\n  | || | | |/\\__/ / | | | |\\ \\| | | | \\__/\\| |\\  \\" +
            //    "\r\n  \\_/\\_| |_/\\____/  \\_/ \\_| \\_\\_| |_/\\____/\\_| \\_/";
            optionChoice = "Select one of the options above by entering its number: ";
            oopsyDesc = null;
            menuText = null;
            mainMenuSum = null;
        }
        public void PrintTitle()
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("\n");
            if (GlobalVal.isLoggedIn == true)
            {
                Console.WriteLine("User: " + GlobalVal.displayName);
            }
            if (oopsyDesc != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(oopsyDesc);
                Console.ResetColor();
            }
        }
        public void PrintMenu()
        {
            Console.WriteLine(menuText);
            Console.WriteLine();
        }
        public void PrintTasks(List<Task> input)
        {
            GlobalVal globalVar = new GlobalVal();
            int columnWidth = globalVar.screenWidth;
            string output = null;
            int totalChars = 0;
            input.ToList();
            if (input.Count() > 0)
            {
                for (int i = 0; i < input.Count(); i++)
                {
                    string task = input[i].TaskName + " - " + input[i].TaskDue.ToString("D") + "; ";
                    if (!GlobalVal.remvTask)
                    { 
                        totalChars += task.Length;
                        if (totalChars < columnWidth)
                        {
                            output += task;
                        }
                        else
                        {
                            Console.WriteLine(output);
                            totalChars = 0;
                            i--;
                            output = null;
                        }
                    }
                    if (GlobalVal.remvTask)
                    {
                        totalChars += task.Length;
                        if (totalChars < columnWidth)
                        {
                            output += task;
                        }
                        else
                        {
                            Console.WriteLine(output);
                            totalChars = 0;
                            i--;
                            output = null;
                        }

                    }
                }
                Console.WriteLine(output);
                Console.WriteLine();
            }
        }
        public void PrintChoice()
        {
            Console.Write(optionChoice);
        }
    }
}
