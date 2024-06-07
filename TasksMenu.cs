using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace TasTrack
{
    internal class TasksMenu
    {
        GlobalVar globalVar = new GlobalVar();
        public TasksMenu()
        {
            var start = new MainLoop();
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "1: Add Task\n2: Remove Task\n3: Next Day\n4: Previous Day\n5: Return to Calendar Menu\n6: Return to Main Menu";
            if (GlobalVar.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVar.errorNumber == 1) { menuPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVar.errorNumber == 2) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
            if (GlobalVar.errorNumber == 3) { menuPrinter.oopsyDesc = ""; }
            if (GlobalVar.errorNumber == 4) { menuPrinter.oopsyDesc = "You gotta use a number, not a letter!"; }
            menuPrinter.PrintTitle();
            menuPrinter.PrintMenu();
            int currentYear = int.Parse(globalVar.dayNumber[3]); 
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVar.errorNumber = -1;
                switch (select)
                {
                    case 1:// Here we will add a taks to our list
                        GlobalVar.isAddTask = true;
                        GlobalVar.taskList = false;
                        break;
                    case 2:
                        //GlobalVar.isRemvTask = true;
                        //GlobalVar.taskList = false;
                        //List<Task> existListRemv = new List<Task> { };
                        //Userinfo existInfoRemv = new Userinfo { };

                        //using (StreamReader sr = new StreamReader(GlobalVar.filePath))
                        //{
                        //    var json = sr.ReadToEnd();
                        //    JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                        //    int count = desrlzdjson.TaskList.Count;
                        //    //existInfoAdd = desrlzdjson.UserInfo;
                        //    for (int i = 0; i < count; i++)
                        //    {
                        //        existListRemv.Add(desrlzdjson.TaskList[i]);
                        //    }
                        //}
                        GlobalVar.errorNumber = 1;
                        break;
                    case 3:
                        GlobalVar.errorNumber = 1;
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
