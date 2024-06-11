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
        public TaskRemove()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Press escape at any time to return to the Task Menu." +
                                 "\nIf the task you want to change isn't in the list below return to the Task Menu." +
                                 "\nOtherwise follow the steps to edit a task";
            if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a number corresponding to a task in the list."; }
            List<Task> allTasks = globalVal.SummonTasks();            
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            Console.WriteLine("Tasks for " + DateTime.Now.AddDays(GlobalVal.dayAdjust).ToString("D") + ":");
            menuPrinter.PrintTasks(GlobalVal.currentTasks);
            string input = null;
            string respone = globalVal.EscapeLoop(input);

            if (respone != null)
            { 
                
            }
        }
    }
}
