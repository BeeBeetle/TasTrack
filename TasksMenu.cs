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
        GlobalVar globalVar = new GlobalVar();
        List<Task> currentTasks = new List<Task> { };
        int count;
        string addTask;
        public TasksMenu()
        {
            var start = new MainLoop();
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Add Task\n2: Remove Task\n3: Next Day\n4: Previous Day\n5: Return to Calendar Menu\n6: Return to Main Menu";
            using (StreamReader sr = new StreamReader(GlobalVar.filePath))
            {
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    count = desrlzdjson.TaskList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (desrlzdjson.TaskList[i].TaskDue.Date == DateTime.Now.Date)
                        {
                            currentTasks.Add(desrlzdjson.TaskList[i]);
                        }
                    }
                }
            }
            currentTasks.Sort(delegate (Task x, Task y) { return x.TaskDue.Date.ToShortDateString().CompareTo(y.TaskDue.ToShortDateString()); });
            count = currentTasks.Count;
            for (int i = 0; i < count; i++)
            {
                addTask = currentTasks[i].TaskName + ", " + currentTasks[i].TaskDue.ToString("D");
                menuPrinter.showTasks.Add(addTask);
            }
            if (GlobalVar.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVar.errorNumber == 1) { menuPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVar.errorNumber == 2) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            if (GlobalVar.errorNumber == 3) { menuPrinter.oopsyDesc = ""; }
            if (GlobalVar.errorNumber == 4) { menuPrinter.oopsyDesc = "You gotta use a number, not a letter!"; }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            menuPrinter.PrintTasks();
            menuPrinter.PrintChoice();
            int currentYear = int.Parse(globalVar.dayNumber[3]);
            int today = int.Parse(globalVar.dayNumber[2]);

            try
            {
                int select = int.Parse(Console.ReadLine());
                GlobalVar.errorNumber = -1;
                switch (select)
                {
                    case 1:// Here we will add a taks to our list
                        GlobalVar.isAddTask = true;
                        GlobalVar.taskList = false;
                        break;
                    case 2:// If we go here we can edit tasks
                        GlobalVar.taskList = false;
                        GlobalVar.isRemvTask = true;
                        break;
                    case 3:
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        menuPrinter.PrintTasks();
                        break;
                    case 4:
                        GlobalVar.errorNumber = 1;
                        break;
                    case 5:
                        GlobalVar.taskList = false;
                        GlobalVar.calView = true;
                        start.Main();
                        break;
                    case 6:
                        GlobalVar.taskList = false;
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
