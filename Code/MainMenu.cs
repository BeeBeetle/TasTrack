using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasTrack;

namespace TasTrack
{
    internal class MainMenu
    {
        public MainMenu()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Calendar\n2: View Tasks\n3: Logout";
            menuPrinter.mainMenuSum = "Upcomming tasks\n";
            if (GlobalVar.errorNumber == 0)
            {
                menuPrinter.oopsyDesc = "Oops! Please select a valid option.";
            }
            if (GlobalVar.errorNumber == 1)
            {
                menuPrinter.oopsyDesc = "That profile doesn't exist.";
            }
            if (GlobalVar.errorNumber == 2)
            {
                menuPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid.";
            }
            if (GlobalVar.errorNumber == 3)
            {
                menuPrinter.oopsyDesc = "Your passwords do not match. Please try again.";
            }
            if (GlobalVar.errorNumber == 4)
            {
                menuPrinter.oopsyDesc = "Shit! Something broke, bad!";
            }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            int select = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (select)
                {
                    case 1:
                        GlobalVar.calView = true;
                        break;
                    case 2:
                        //TaskList;
                        break;
                    case 3:
                        GlobalVar.isLoggedIn = false;
                        GlobalVar.displayName = null;
                        var start = new MainLoop();
                        start.Main();
                        break;
                    default:
                        GlobalVar.errorNumber = 0;
                        break;
                }
            }
            catch
            {
                GlobalVar.errorNumber = 4;
            }
        }
    }
}
