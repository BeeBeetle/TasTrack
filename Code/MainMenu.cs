﻿using System;
using System.Collections.Generic;
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
            menuPrinter.menuText = "1: Calendar\n2: View Tasks\n3: Add A Task\n4: Remove A Task\n5: Logout"; 
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
            menuPrinter.Print();
            int select = Convert.ToInt32(Console.ReadLine());
            switch (select)
            {
<<<<<<< Updated upstream
                //case 1:


                case 5:
                    GlobalVar.isLoggedIn = "n";
                    GlobalVar.displayName = null;
                    var start = new TT();
                    start.Main();
                    break;
                default:
                    GlobalVar.errorNumber = 0;
                    break;
=======
                menuPrinter.oopsyDesc = "You gotta use a number, not a letter!";
            }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVar.errorNumber = -1;
                switch (select)
                {
                    case 1:
                        GlobalVar.calView = true;
                        break;
                    case 2:
                        GlobalVar.taskList = true;
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
>>>>>>> Stashed changes
            }
        }
    }
}
