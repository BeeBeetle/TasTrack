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
        GlobalVal globalVal = new GlobalVal();
        public TaskAdd()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Press escape at any time to return to the Task Menu.\nOtherwise, follow the steps to add a task:";
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
            string input = null;
            string response = null;
            while (addingTask)
            {
                if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a yes or no, a Y or N"; }
                if (GlobalVal.errorNumber == 2) { menuPrinter.oopsyDesc = "Oops! I don't recognize that date! Did you include the slashes?"; }
                if (GlobalVal.errorNumber == 3) { menuPrinter.oopsyDesc = "You entered a day that doesn't exist in the month you chose."; }
                if (GlobalVal.errorNumber == 4) { menuPrinter.oopsyDesc = "Oops! I didn't recognize that as a 24 hour time! Did you include the colon?"; }
                if (GlobalVal.errorNumber == 5) { menuPrinter.oopsyDesc = "That year is outside of my scope."; }
                if (GlobalVal.errorNumber == 6) { menuPrinter.oopsyDesc = "I don't think thats a month . . ."; }
                if (GlobalVal.errorNumber == 7) { menuPrinter.oopsyDesc = "There are only 24 hours in a day, 0 - 23."; }
                if (GlobalVal.errorNumber == 8) { menuPrinter.oopsyDesc = "That's the wrong amount of minutes!"; }
                for (int b = breakpoint; b < 4; b++)
                {
                    if (b == 0)
                    {// We first need the task name
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        if (GlobalVal.errorNumber == 1)
                        {
                            GlobalVal.errorNumber = -1;
                            Console.Write("\nDo you want to name your task ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(addTask.TaskName);
                            Console.ResetColor();
                            Console.Write("? Y/N: ");
                            GlobalVal.errorNumber = -1;
                            response = globalVal.EscapeLoop(input).ToLower();
                        }
                        else
                        {
                            GlobalVal.errorNumber = -1;
                            Console.Write("What do you want to name this task?: ");
                            addTask.TaskName = globalVal.EscapeLoop(input);
                            Console.Write("\nDo you want to name your task ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(addTask.TaskName);
                            Console.ResetColor();
                            Console.Write("? Y/N: ");
                            response = globalVal.EscapeLoop(input).ToLower();
                        }
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
                            GlobalVal.errorNumber = 1;
                            breakpoint = 0;
                            break;
                        }
                    }
                    if (b == 1)
                    {// We also need a date the task is due
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        GlobalVal.errorNumber = -1;
                        Console.WriteLine("What date should this be completed by?");
                        Console.Write("Please enter a date as MM/DD/YYYY: ");
                        string dateInput = globalVal.EscapeLoop(input);
                        string[] dateOutput = dateInput.Split('/'); ;// We split the date because we need the parts as numbers
                        if (dateInput != null)
                        {// Lets make sure that the date is theoretically valid and has a month day and year entered
                            try
                            {
                                int.Parse(dateOutput[2]);
                                int.Parse(dateOutput[0]);
                                int.Parse(dateOutput[1]);
                            }
                            catch
                            {
                                GlobalVal.errorNumber = 2;
                                breakpoint = 1;
                                break;
                            }
                        }
                        year = int.Parse(dateOutput[2]);
                        month = int.Parse(dateOutput[0]);
                        day = int.Parse(dateOutput[1]);
                        if (year > 9999 || year < 0)
                        {
                            GlobalVal.errorNumber = 5;
                            breakpoint = 1;
                            break;
                        }
                        if (month > 13 || month < 0)
                        {// I split the month/year validity checks because I want two different error messages
                            GlobalVal.errorNumber = 6;
                            breakpoint = 1;
                            break;
                        }
                        else
                        {// If the year is good and the month is good then we can move on to checking if day entered is valid
                            if (thirtyDay.Contains(month) && day > 30 || thirtyoneDay.Contains(month) && day > 31)
                            {// Thirty days has September, April, June, and November. All the rest have 31,
                                GlobalVal.errorNumber = 3;
                                breakpoint = 1;
                                break;
                            }
                            if (month == 2)
                            {// 'cept february, but leap years give it one!
                                bool leapYear = DateTime.IsLeapYear(year);// Easy leap year check
                                if (!leapYear && day > 28 || leapYear && day > 29)
                                {
                                    GlobalVal.errorNumber = 3;
                                    breakpoint = 1;
                                    break;
                                }
                            }
                        }
                    }
                    if (b == 2)
                    {
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        if (GlobalVal.errorNumber == 4 || GlobalVal.errorNumber == 7 || GlobalVal.errorNumber == 8)
                        {
                            GlobalVal.errorNumber = -1;
                            response = "y";
                        }
                        else
                        {
                            GlobalVal.errorNumber = -1;
                            Console.Write("Would you like to specify a time? Y/N: ");
                            response = globalVal.EscapeLoop(input).ToLower();
                            Console.WriteLine();
                        }
                        if (response == "y" || response == "yes")
                        {
                            Console.Write("Using the 24 clock (11pm is 23/12am is 0) please enter a time as HH:MM?: ");
                            string hourInput = globalVal.EscapeLoop(input);
                            string[] hourOutput = hourInput.Split(':');
                            try
                            {
                                int.Parse(hourOutput[0]);
                                int.Parse(hourOutput[1]);
                            }
                            catch
                            {
                                GlobalVal.errorNumber = 4;
                                breakpoint = 2;
                                break;
                            }
                            hour = int.Parse(hourOutput[0]);
                            minute = int.Parse(hourOutput[1]);
                            if (hour < 0 || hour > 23)
                            {
                                GlobalVal.errorNumber = 7;
                                breakpoint = 2;
                                break;
                            }
                            if (minute < 0 || minute > 59)
                            {
                                GlobalVal.errorNumber = 8;
                                breakpoint = 2;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                        if (response == "n" || response == "no")
                        {
                            hour = 00;
                            minute = 00;
                        }
                        else
                        {
                            GlobalVal.errorNumber = 1;
                            breakpoint = 2;
                            break;
                        }
                    }
                    addTask.TaskDue = new DateTime(year, month, day, hour, minute, 0);
                    addTask.TaskCreate = DateTime.Now;
                    if (b == 3)
                    {
                        menuPrinter.PrintTitle();
                        menuPrinter.PrintMenu();
                        Console.WriteLine(addTask.TaskName + ", " + addTask.TaskDue.ToString("g"));
                        Console.WriteLine("Would you like to add this task? Y/N: ");
                        response = globalVal.EscapeLoop(input).ToLower();
                        if (response == "y" || response == "yes")
                        {
                            addingTask = false;
                            break;
                        }
                        if (response == "n" || response == "no")
                        {
                            breakpoint = 0;
                            GlobalVal.errorNumber = -1;
                            continue;
                        }
                        else
                        {
                            GlobalVal.errorNumber = 1;
                            breakpoint = 3;
                            break;
                        }
                    }                    
                }
                addTask.ID = IDMaker.Hash(addTask.TaskName);
            }
            List<Task> existListAdd = new List<Task> { };
            Userinfo existInfoAdd = new Userinfo { };

            using (StreamReader sr = new StreamReader(GlobalVal.filePath))
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
            using (StreamWriter writer = new StreamWriter(GlobalVal.filePath))
            {
                writer.WriteLine(updateJson);
            }
            if (!addingTask)
            {
                menuPrinter.PrintTitle();
                menuPrinter.PrintMenu();
                GlobalVal.errorNumber = -1;
                Console.Write("Would you like to add another task? Y/N: ");
                string addMore = Console.ReadLine().ToLower();
                while (true)
                {
                    if (addMore == "y" || addMore == "yes")
                    {
                        addingTask = true;
                        break;
                    }
                    if (addMore == "n" || addMore == "no")
                    {
                        GlobalVal.addTask = false;
                        GlobalVal.taskList = true;
                        var start = new MainLoop();
                        start.Main();
                        break;
                    }
                    else
                    {
                        GlobalVal.errorNumber = 1;
                        break;
                    }
                }
            }
        }
    }
}
