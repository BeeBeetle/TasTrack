using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class CalendarMenu
    {
        GlobalVar globalVar = new GlobalVar();
        public CalendarMenu()
        {
            var calendarMenu = new CalendarBuilder();
            Printer calendarPrinter = new Printer();
            string displayTasks = null;
            calendarPrinter.menuText = "1: Select a day\n2: Next Month\n3: Previous Month\n4: Tasks Menu\n5: Return to Main Menu";
            using (StreamReader sr = new StreamReader(GlobalVar.filePath))
            {
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    int count = desrlzdjson.TaskList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (desrlzdjson.TaskList[i].TaskDue.Date == DateTime.Now.Date)
                        {
                            displayTasks += desrlzdjson.TaskList[i].TaskName + ", " + desrlzdjson.TaskList[i].TaskDue.ToString("D") + "; ";
                        }
                    }
                }
            }
            if (GlobalVar.errorNumber == 0) { calendarPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVar.errorNumber == 1) { calendarPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVar.errorNumber == 2) { calendarPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVar.errorNumber == 3) { calendarPrinter.oopsyDesc = "Your passwords do not match. Please try again."; }
            if (GlobalVar.errorNumber == 4) { calendarPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            calendarPrinter.PrintTitle();
            Console.WriteLine(calendarMenu.Format(calendarMenu.Create(),
            globalVar.dateArray[1] + ": " + displayTasks));
            calendarPrinter.PrintMenu();
            calendarPrinter.PrintChoice();
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVar.errorNumber = -1;
                switch (select)
                {
                    case 1:
                        GlobalVar.errorNumber = 1;
                        break;
                    case 2:
                        GlobalVar.errorNumber = 1;
                        break;
                    case 3:
                        GlobalVar.errorNumber = 1;
                        break;
                    case 4:
                        GlobalVar.calView = false;
                        GlobalVar.taskList = true;
                        break;
                    case 5:
                        GlobalVar.calView = false;
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
            }
        }
    }
}
