using System;
using System.Collections.Generic;
using System.Linq;
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
        public Printer() 
        {
            title = "\n _____ ___   _____ ___________  ___  _____  _   __" +
                "\r\n|_   _/ _ \\ /  ___|_   _| ___ \\/ _ \\/  __ \\| | / /" +
                "\r\n  | |/ /_\\ \\\\ `--.  | | | |_/ / /_\\ \\ /  \\/| |/ / " +
                "\r\n  | ||  _  | `--. \\ | | |    /|  _  | |    |    \\ " +
                "\r\n  | || | | |/\\__/ / | | | |\\ \\| | | | \\__/\\| |\\  \\" +
                "\r\n  \\_/\\_| |_/\\____/  \\_/ \\_| \\_\\_| |_/\\____/\\_| \\_/";
            optionChoice = "Select one of the options above by entering its number: ";
            oopsyDesc = null;
            menuText = null;
        }
        public void Print()
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine("\n");
            if (Login.isLoggedIn == "y")
            {
                Console.WriteLine("User: " + Login.displayName);
            }
            if (oopsyDesc != null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(oopsyDesc);
                Console.ResetColor();
            }
            Console.WriteLine();
            Console.WriteLine(menuText);
            Console.WriteLine();
            Console.Write(optionChoice);
        }
    }
}
