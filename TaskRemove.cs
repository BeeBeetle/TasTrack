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
        GlobalVal globalVar = new GlobalVal();
        List<Task> currentTasks = new List<Task> { };
        int count;
        string addTask;
        public TaskRemove()
        {
            //Printer menuPrinter = new Printer();
            //menuPrinter.menuText = "Follow the steps to add a task:";
            //if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            //if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            //using (StreamReader sr = new StreamReader(GlobalVal.filePath))
            //{
            //    var json = sr.ReadToEnd();
            //    JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
            //    if (desrlzdjson.TaskList != null)
            //    {
            //        count = desrlzdjson.TaskList.Count;
            //        for (int i = 0; i < count; i++)
            //        {
            //            if (desrlzdjson.TaskList[i].TaskDue.Date == DateTime.Now.Date)
            //            {
            //                currentTasks.Add(desrlzdjson.TaskList[i]);
            //            }
            //        }
            //    }
            //}
            //currentTasks.Sort(delegate (Task x, Task y) { return x.TaskDue.Date.ToShortDateString().CompareTo(y.TaskDue.ToShortDateString()); });
            //count = currentTasks.Count;
            //for (int i = 0; i < count; i++)
            //{
            //    addTask = currentTasks[i].TaskName + ", " + currentTasks[i].TaskDue.ToString("D");
            //    menuPrinter.showTasks.Add(addTask);
            //}
            //menuPrinter.PrintTitle();
            //menuPrinter.PrintMenu();
            //menuPrinter.PrintTasks();
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
