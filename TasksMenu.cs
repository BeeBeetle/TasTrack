using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TasTrack
{
    internal class TasksMenu
    {
        GlobalVal globalVal = new GlobalVal();
        List<string> currentTasks = new List<string> { };
        public TasksMenu()
        {
            var start = new MainLoop();
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Add Task\n2: Remove Task\n3: Previous Day\n4: Next Day\n5: Calendar Menu\n6: Return to Main Menu";
            if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVal.errorNumber == 2) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            if (GlobalVal.errorNumber == 3) { menuPrinter.oopsyDesc = ""; }
            if (GlobalVal.errorNumber == 4) { menuPrinter.oopsyDesc = "You gotta use a number, not a letter!"; }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            Console.WriteLine("Tasks for " + DateTime.Now.AddDays(GlobalVal.dayAdjust).ToString("D") + ":");
            currentTasks = globalVal.SummonTasks(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust));
            menuPrinter.PrintTasks(currentTasks);
            menuPrinter.PrintChoice();
            int currentYear = int.Parse(globalVal.dayNumber[2]);
            int today = int.Parse(globalVal.dayNumber[1]);

            try
            {
                int select = int.Parse(Console.ReadLine());
                GlobalVal.errorNumber = -1;
                switch (select)
                {
                    case 1:// Here we will add a taks to our list
                        GlobalVal.addTask = true;
                        GlobalVal.taskList = false;
                        break;
                    case 2:// If we go here we can edit tasks
                        GlobalVal.taskList = false;
                        GlobalVal.remvTask = true;
                        break;
                    case 3:
                        GlobalVal.dayAdjust -= 1;
                        break;
                    case 4:
                        GlobalVal.dayAdjust += 1;
                        break;
                    case 5:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.taskList = false;
                        GlobalVal.calView = true;
                        start.Main();
                        break;
                    case 6:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.taskList = false;
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
