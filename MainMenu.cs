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
        GlobalVal globalVar = new GlobalVal();
        public MainMenu()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Calendar\n2: View Tasks\n3: Logout";
            if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "That profile doesn't exist."; }
            if (GlobalVal.errorNumber == 2) { menuPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVal.errorNumber == 3) { menuPrinter.oopsyDesc = "Your passwords do not match. Please try again."; }
            if (GlobalVal.errorNumber == 4) { menuPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            menuPrinter.PrintChoice();
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVal.errorNumber = -1;
                switch (select)
                {
                    case 1:
                        GlobalVal.calView = true;
                        break;
                    case 2:
                        GlobalVal.taskList = true;
                        break;
                    case 3:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.isLoggedIn = false;
                        GlobalVal.displayName = null;
                        var start = new MainLoop();
                        start.Main();
                        break;
                    default:
                        GlobalVal.errorNumber = 0;
                        break;
                }
            }
            catch
            {
                GlobalVal.errorNumber = 4;
            }
        }
    }
}
