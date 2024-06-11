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
        public TasksMenu()
        {
            var start = new MainLoop();
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Add Task\n2: Modify a Listed Task\n3: Previous Day\n4: Next Day\n5: Find Task\n6: Calendar Menu\n7: Return to Main Menu";
            if (!GlobalVal.noInput)
            {// This catches if there was input in the search by name option
                if (GlobalVal.currentTasks.Count == null)
                {// If no input was entered then we want to return an error saying you gotta do a name
                    GlobalVal.errorNumber = 3;
                    GlobalVal.noInput = true;
                    GlobalVal.currentTasks = globalVal.SummonTasksByDate(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust));
                }
                else
                {// If we entered some text and it is found then return all matches
                    GlobalVal.currentTasks = globalVal.SummonTasksByName(GlobalVal.taskName);
                    GlobalVal.noInput = true;
                }// If there is no match then the output will be empty and trigger the if statment
            }
            else
            {// If there is no input we just do the days (for instance the first time we come to the page)
                GlobalVal.currentTasks = globalVal.SummonTasksByDate(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust));
            }// Right now once you load the find days and get a hit it's stuck like that and you have to do next day --> previous day to return to the original menu
            if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVal.errorNumber == 2) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            if (GlobalVal.errorNumber == 3) { menuPrinter.oopsyDesc = "I couldn't find that task anywhere. The name must be an exact match."; }
            if (GlobalVal.errorNumber == 4) { menuPrinter.oopsyDesc = "You gotta use a number, not a letter!"; }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            Console.WriteLine("Tasks for " + DateTime.Now.AddDays(GlobalVal.dayAdjust).ToString("D") + ":");
            menuPrinter.PrintTasks(GlobalVal.currentTasks);
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
                    case 3:// Simple enough to page through days, this one is for the previous day
                        GlobalVal.dayAdjust -= 1;
                        break;
                    case 4:// And this one takes us to the next day
                        GlobalVal.dayAdjust += 1;
                        break;
                    case 5:// We want to try and find a task by name with this plus the if statement at the start
                        GlobalVal.noInput = false;
                        string input = null;
                        Console.Write("Enter a tasks name: ");
                        GlobalVal.taskName = globalVal.EscapeLoop(input);
                        break;
                    case 6:// Reset our bools and bring us back to the calendar screen
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.taskList = false;
                        GlobalVal.calView = true;
                        GlobalVal.noInput = true;
                        start.Main();
                        break;
                    case 7:// This will negate the task menu and take us all the way back to the main menu
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.taskList = false;
                        GlobalVal.noInput = true;
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
