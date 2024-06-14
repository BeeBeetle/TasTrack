using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TasTrack
{
    internal class TaskRemove
    {
        GlobalVal globalVal = new GlobalVal();
        MainLoop start = new MainLoop();
        public TaskRemove()
        {
            Printer menuPrinter = new Printer();
            menuPrinter.menuText = "Press escape at any time to return to the Task Menu." +
                                 "\nIf the task you want to change isn't in the list below return to the Task Menu." +
                                 "\nOtherwise follow the steps to edit a task";
            Task editTask = new Task();
            int taskChoice;
            int editChoice;
            int extStep = 0;
            bool editingTask = true;
            bool delete = false;
            string response;
            string input = null;
            while (editingTask)
            {
                if (GlobalVal.errorNumber == 0) { menuPrinter.oopsyDesc = "Oops! Please select a valid option."; }
                if (GlobalVal.errorNumber == 1) { menuPrinter.oopsyDesc = "Your repsponse needs to be a number corresponding to a task in the list."; }
                if (GlobalVal.errorNumber == 2) { menuPrinter.oopsyDesc = "That number is outside the range of the tasks displayed."; }
                if (GlobalVal.errorNumber == 3) { menuPrinter.oopsyDesc = "You entered a day that doesn't exist in the month you chose."; }
                if (GlobalVal.errorNumber == 4) { menuPrinter.oopsyDesc = "Oops! I didn't recognize that as a 24 hour time! Did you include the colon?"; }
                if (GlobalVal.errorNumber == 5) { menuPrinter.oopsyDesc = "That year is outside of my scope."; }
                if (GlobalVal.errorNumber == 6) { menuPrinter.oopsyDesc = "I don't think thats a month . . ."; }
                if (GlobalVal.errorNumber == 7) { menuPrinter.oopsyDesc = "There are only 24 hours in a day, 0 - 23."; }
                if (GlobalVal.errorNumber == 8) { menuPrinter.oopsyDesc = "That's the wrong amount of minutes!"; }                
                if (extStep == 0)
                {
                    menuPrinter.PrintTitle();
                    menuPrinter.PrintMenu();
                    menuPrinter.PrintTasks(GlobalVal.currentTasks);
                    Console.Write("\nChoose a task from the list: ");
                    response = globalVal.EscapeLoop(input);
                    try { int.Parse(response); }
                    catch { GlobalVal.errorNumber = 1; start.Main(); }                       
                    taskChoice = int.Parse(response);
                    int count = GlobalVal.currentTasks.Count;
                    if (taskChoice > count || 0 > taskChoice)
                    {
                        GlobalVal.errorNumber = 2;
                        break;
                    }
                    else
                    {
                        editTask = GlobalVal.currentTasks[taskChoice - 1];
                        extStep = 1;
                    }
                }
                if (extStep == 1)
                {
                    menuPrinter.PrintTitle();
                    menuPrinter.PrintMenu();
                    Console.WriteLine("1: Name- " + editTask.TaskName + "\n2: Due Date- " + editTask.TaskDue.ToString("g") + "\n3: DELETE" + "\n4: Done Editing");
                    Console.Write("\nCoose something to edit, or delete the task: ");
                    input = null;
                    response = globalVal.EscapeLoop(input);
                    try { int.Parse(response); }
                    catch { GlobalVal.errorNumber = 1; start.Main(); }
                    editChoice = int.Parse(response);
                    switch (editChoice)
                    {
                        case 1:// Overwrite the name in editTask
                            menuPrinter.PrintTitle();
                            menuPrinter.PrintMenu();
                            Console.WriteLine("Current name: " + editTask.TaskName);
                            Console.Write("\nNew name: ");
                            editTask.TaskName = globalVal.EscapeLoop(input);
                            break;
                        case 2:// Make a new due date and change that value in editTask
                            int year = 0;
                            int month = 0;
                            int day = 0;
                            int hour = 0;
                            int minute = 0;
                            int[] thirtyDay = { 4, 6, 9, 11 };
                            int[] thirtyoneDay = { 1, 3, 5, 7, 8, 10, 12 };
                            int intStep = 0;
                            while (true)
                            {
                                input = null;
                                if (intStep == 0)
                                {
                                    menuPrinter.PrintTitle();
                                    menuPrinter.PrintMenu();
                                    Console.WriteLine("Instead of " + editTask.TaskDue.ToString("g") + ", what new date should this be completed by?");
                                    Console.Write("Please enter a date as MM/DD/YYYY: ");
                                    string dateInput = globalVal.EscapeLoop(input);
                                    string[] dateOutput = dateInput.Split('/'); ;// We split the date because we need the parts as numbers
                                    if (dateInput != null)
                                    {// Lets make sure that the date is theoretically valid and has a month day and year entered
                                        try { int.Parse(dateOutput[2]); int.Parse(dateOutput[0]); int.Parse(dateOutput[1]); }
                                        catch { GlobalVal.errorNumber = 2; break; }
                                    }
                                    year = int.Parse(dateOutput[2]);
                                    month = int.Parse(dateOutput[0]);
                                    day = int.Parse(dateOutput[1]);
                                    if (year > 9999 || year < 0)
                                    {
                                        GlobalVal.errorNumber = 5;
                                        break;
                                    }
                                    if (month > 13 || month < 0)
                                    {// I split the month/year validity checks because I want two different error messages
                                        GlobalVal.errorNumber = 6;
                                        break;
                                    }
                                    else
                                    {// If the year is good and the month is good then we can move on to checking if day entered is valid
                                        if (thirtyDay.Contains(month) && day > 30 || thirtyoneDay.Contains(month) && day > 31)
                                        {// Thirty days has September, April, June, and November. All the rest have 31,
                                            GlobalVal.errorNumber = 3;
                                            break;
                                        }
                                        if (month == 2)
                                        {// 'cept february, but leap years give it one!
                                            bool leapYear = DateTime.IsLeapYear(year);// Easy leap year check
                                            if (!leapYear && day > 28 || leapYear && day > 29)
                                            {
                                                GlobalVal.errorNumber = 3;
                                                break;
                                            }
                                        }
                                        intStep = 1;
                                    }
                                }
                                if (intStep == 1)
                                {
                                    menuPrinter.PrintTitle();
                                    menuPrinter.PrintMenu();
                                    Console.Write("\nWould you like to specify a time? Y/N: ");
                                    response = globalVal.EscapeLoop(input).ToLower();
                                    if (response == "y" || response == "yes")
                                    {
                                        menuPrinter.PrintTitle();
                                        menuPrinter.PrintMenu();
                                        Console.Write("Using the 24 clock (11pm is 23/12am is 0) please enter a time as HH:MM?: ");
                                        string hourInput = globalVal.EscapeLoop(input);
                                        string[] hourOutput = hourInput.Split(':');
                                        try { int.Parse(hourOutput[0]); int.Parse(hourOutput[1]); }
                                        catch { GlobalVal.errorNumber = 4; break; }
                                        hour = int.Parse(hourOutput[0]);
                                        minute = int.Parse(hourOutput[1]);
                                        if (hour < 0 || hour > 23)
                                        {
                                            GlobalVal.errorNumber = 7;
                                            break;
                                        }
                                        if (minute < 0 || minute > 59)
                                        {
                                            GlobalVal.errorNumber = 8;
                                            break;
                                        }
                                        else
                                        {
                                            intStep = 2;
                                            break;
                                        }
                                    }
                                    if (response == "n" || response == "no")
                                    {
                                        hour = 00;
                                        minute = 00;
                                        intStep = 2;
                                        break;
                                    }
                                    else
                                    {
                                        GlobalVal.errorNumber = 1;
                                        break;
                                    }
                                }
                            }// Overwrite the value of TaskDue in editTask
                            editTask.TaskDue = new DateTime(year, month, day, hour, minute, 0);
                            extStep = 1;
                            break;
                        case 3:
                            extStep = 2;
                            editingTask = false;
                            delete = true;
                            break;
                        case 4:
                            extStep = 2;
                            editingTask = false;
                            break;
                        default:
                            GlobalVal.errorNumber = 0;
                            break;
                    }
                }
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
                        if (desrlzdjson.TaskList[i].ID == editTask.ID)
                        {
                            continue;
                        }
                        else
                        {
                            existListAdd.Add(desrlzdjson.TaskList[i]);
                        }
                    }
                }
                existInfoAdd = desrlzdjson.UserInfo;
            }
            if (!delete)
            {
                existListAdd.Add(editTask);
            }

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
            GlobalVal.remvTask = false;
            GlobalVal.taskList = true;
            start.Main();
        }
    }
}
