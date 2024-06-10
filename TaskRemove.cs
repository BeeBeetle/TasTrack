using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class TaskRemove
    {
        GlobalVal globalVal = new GlobalVal();
        List<string> currentTasks = new List<string> { };
        int count;
        string addTask;
        public TaskRemove()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Follow the steps to add a task:";
            if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            Console.WriteLine("Tasks for " + DateTime.Now.AddDays(GlobalVal.dayAdjust).ToString("D") + ":");
            currentTasks = globalVal.SummonTasksByDate(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust));
            menuPrinter.PrintTasks(currentTasks);
            menuPrinter.PrintChoice();
            int currentYear = int.Parse(globalVal.dayNumber[2]);
            int today = int.Parse(globalVal.dayNumber[1]);
            //have a day switching mechanic
            //one to leaf through
            //one to pick a date by entering it
            //users can select a task at any time (by number with the display value of the task name)
            //2 choices: modify task, delete task
            //if delete confirm the choice (permanence message)
            //if modify 3 choices: change name, change due date, change desc.(when added)

        }
    }
}
