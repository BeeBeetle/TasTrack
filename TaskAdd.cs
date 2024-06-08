using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class TaskAdd
    {
        GlobalVar globalVar = new GlobalVar();
        public TaskAdd()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Follow the steps to add a task:";
            Task addTask = new Task { };// Initialize the class we use for tasks
            int year = 0;
            int month = 0;
            int day = 0;
            int hour = 0;
            int minute = 0;
            int[] thirtyDay = { 4, 6, 9, 11 };
            int[] thirtyoneDay = { 1, 3, 5, 7, 8, 10, 12 };
            int breakpoint = 0;
            bool addingTask = true;
            while (addingTask)
            {
                if (GlobalVar.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
                if (GlobalVar.errorNumber == 2) { menuPrinter.oopsyDesc = "Oops! I don't recognize that date! Did you include the slashes?"; }
                if (GlobalVar.errorNumber == 3) { menuPrinter.oopsyDesc = "You entered a day that doesn't exist in the month you chose."; }
                if (GlobalVar.errorNumber == 4) { menuPrinter.oopsyDesc = "Oops! I didn't recognize that as a 24 hour time! Did you include the colon?"; }
                if (GlobalVar.errorNumber == 5) { menuPrinter.oopsyDesc = "That year is outside of my scope."; }
                if (GlobalVar.errorNumber == 6) { menuPrinter.oopsyDesc = "I don't think thats a month . . ."; }
                if (GlobalVar.errorNumber == 7) { menuPrinter.oopsyDesc = "There are only 24 hours in a day, 0 - 23."; }
                if (GlobalVar.errorNumber == 8) { menuPrinter.oopsyDesc = "That's the wrong amount of minutes!"; }
                for (int i = breakpoint; i < 3; i++)
                {
                    if (i == 0)
                    {// We first need the task name
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        menuPrinter.PrintChoice();
                        GlobalVar.errorNumber = -1;
                        Console.Write("What do you want to name this task?: ");
                        addTask.TaskName = Console.ReadLine();
                        Console.Write("\nDo you want to name your task ");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(addTask.TaskName);
                        Console.ResetColor();
                        Console.Write("? Y/N: ");
                        string response = Console.ReadLine().ToLower(); 
                        if (response == "y" || response == "yes")
                        {
                            continue;
                        }
                        if (response == "n" || response == "no")
                        {
                            breakpoint = 0;
                            break;
                        }
                        else
                        {
                            GlobalVar.errorNumber = 1;
                            breakpoint = 0;
                            break;
                        }
                    }
                    if (i == 1)
                    {// We also need a date the task is due
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        GlobalVar.errorNumber = -1;
                        Console.WriteLine("What date should this be completed by?");
                        Console.Write("Please enter a date as MM/DD/YYYY: ");
                        string dateInput = Console.ReadLine();
                        string[] dateOutput = dateInput.Split('/'); ;// We split the date because we need the parts as numbers
                        if (dateInput != null)
                        {// Lets make sure that the date is theoretically valid and has a month day and year entered
                            try
                            {
                                year = int.Parse(dateOutput[2]);
                                month = int.Parse(dateOutput[0]);
                                day = int.Parse(dateOutput[1]);
                            }
                            catch
                            {
                                GlobalVar.errorNumber = 2;
                                breakpoint = 1;
                                break;
                            }
                        }
                        year = int.Parse(dateOutput[2]);
                        month = int.Parse(dateOutput[0]);
                        day = int.Parse(dateOutput[1]);
                        if (year > 9999 || year < 0)
                        {
                            GlobalVar.errorNumber = 5;
                            breakpoint = 1;
                            break;
                        }
                        if (month > 13 || month < 0)
                        {// I split the month/year validity checks because I want two different error messages
                            GlobalVar.errorNumber = 6;
                            breakpoint = 1;
                            break;
                        }
                        else
                        {// If the year is good and the month is good then we can move on to checking if day entered is valid
                            if (thirtyDay.Contains(month) && day > 30)
                            {// Thirty days has september, april, june, and november
                                GlobalVar.errorNumber = 3;
                                breakpoint = 1;
                                break;
                            }
                            if (thirtyoneDay.Contains(month) && day > 31)
                            {// All the rest have 31
                                GlobalVar.errorNumber = 3;
                                breakpoint = 1;
                                break;
                            }
                            if (month == 2)
                            {// 'cept february, but leap years give it one!
                                bool leapYear = DateTime.IsLeapYear(year);// Easy leap year check for a leap year
                                if (!leapYear && day > 28)
                                {
                                    GlobalVar.errorNumber = 3;
                                    breakpoint = 1;
                                    break;
                                }
                                if (leapYear && day > 29)
                                {
                                    GlobalVar.errorNumber = 3;
                                    breakpoint = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (i == 2)
                    {
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        GlobalVar.errorNumber = -1;
                        Console.Write("\nWould you like to specify a time? Y/N: ");
                        string response = Console.ReadLine().ToLower();
                        if (response == "y" || response == "yes")
                        {
                            Console.Write("Using the 24 clock (11pm is 23/12am is 0) please enter a time as HH:MM?: ");
                            string hourInput = Console.ReadLine();
                            string[] hourOutput = hourInput.Split(':');
                            try
                            {
                                hour = int.Parse(hourOutput[0]);
                                minute = int.Parse(hourOutput[1]);
                            }
                            catch
                            {
                                GlobalVar.errorNumber = 4;
                                breakpoint = 2;
                                break;
                            }
                            hour = int.Parse(hourOutput[0]);
                            minute = int.Parse(hourOutput[1]);
                            if (hour < 0 || hour > 23)
                            {
                                GlobalVar.errorNumber = 7;
                                breakpoint = 2;
                            }    
                            if (minute < 0 || minute > 59)
                            {
                                GlobalVar.errorNumber = 8;
                                breakpoint = 2;
                            }
                            else
                            {
                                addingTask = false;
                                break;
                            }
                        }
                        if (response == "n" || response == "no")
                        {
                            hour = 00;
                            minute = 00;
                            addingTask = false;
                        }
                        else
                        {
                            GlobalVar.errorNumber = 1;
                            breakpoint = 2;
                            break;
                        }
                    }
                }
            }
            addTask.TaskDue = new DateTime(year, month, day, hour, minute, 0);
            addTask.TaskCreate = DateTime.Now;

            List<Task> existListAdd = new List<Task> { };
            Userinfo existInfoAdd = new Userinfo { };

            using (StreamReader sr = new StreamReader(GlobalVar.filePath))
            {
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    int count = desrlzdjson.TaskList.Count;
                    for (int i = 0; i < count; i++)
                    {
                        existListAdd.Add(desrlzdjson.TaskList[i]);
                    }
                }
                existInfoAdd = desrlzdjson.UserInfo;
            }
            existListAdd.Add(addTask);

            JSONClass addList = new JSONClass
            {
                UserInfo = existInfoAdd,
                TaskList = existListAdd
            };

            var updateJson = JsonConvert.SerializeObject(addList, Formatting.Indented);
            using (StreamWriter writer = new StreamWriter(GlobalVar.filePath))
            {
                writer.WriteLine(updateJson);
            }
            if (!addingTask);
            {
                menuPrinter.PrintTitle();
                menuPrinter.PrintMenu();
                GlobalVar.errorNumber = -1;
                Console.Write("Would you like to add another task? Y/N: ");
                string addMore = Console.ReadLine().ToLower();
                if (addMore == "y" || addMore == "yes")
                {
                    addingTask = true;
                }
                if (addMore == "n" || addMore == "no")
                {
                    GlobalVar.isAddTask = false;
                    GlobalVar.taskList = true;
                    var start = new MainLoop();
                    start.Main();
                }
                else
                {
                    GlobalVar.errorNumber = 1;
                }
            }
        }
    }
}
