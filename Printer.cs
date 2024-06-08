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
        public List<string> showTasks = new List<string>();
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
            if (GlobalVar.isLoggedIn == true)
            {
                Console.WriteLine("User: " + GlobalVar.displayName);
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
            Console.WriteLine();
            Console.WriteLine(menuText);
            Console.WriteLine();
        }
        public void PrintTasks()
        {
            GlobalVar globalVar = new GlobalVar();
            int columnWidth = globalVar.screenWidth;
            string output = null;
            int totalChars = 0;
            if (showTasks.Count() > 0)
            {
                for (int i = 0; i < showTasks.Count(); i++) 
                {
                    totalChars += showTasks[i].Length;
                    if (totalChars < columnWidth) 
                    {
                        output += (showTasks[i]);
                    }
                    else
                    {
                        Console.WriteLine(output);
                        totalChars = 0;
                        i--;
                        output = null;
                    }
                }
                Console.WriteLine(output);
                //string column = string.Join("; ", showTasks.ToArray());
                //while (true)
                //{
                //    string col = new string(column.Skip<char>(loopCount * columnWidth).Take<char>(columnWidth).ToArray()).PadRight(columnWidth); 
                //    if (String.IsNullOrWhiteSpace(col))
                //    {
                //        break;
                //    }
                //    output.AppendFormat("{0}\n", col);
                //    loopCount++;
                //}
                Console.WriteLine();
            }
        }
        public void PrintChoice()
        {
            Console.Write(optionChoice);
        }
    }
}
