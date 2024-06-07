using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class TaskRemove
    {
        GlobalVar globalVar = new GlobalVar();
        public TaskRemove()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Follow the steps to add a task:";
            Task addTask = new Task { };// Initialize the class we use for tasks
            bool addingTask = true; 
            if (GlobalVar.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVar.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            //load the "activeDay" tasks
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
