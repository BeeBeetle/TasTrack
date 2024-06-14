using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    public class GlobalVal
    {
        public List<Task> SummonTasks()
        {// Here we are just getting all tasks for any instance in which we may need to search them ALL with code
            List<Task> allTasks = new List<Task> { };
            int count;
            using (StreamReader sr = new StreamReader(filePath))
            {// Pull all the data from the JSON for the logged in user
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    allTasks = desrlzdjson.TaskList;
                }
                return allTasks;
            }
        }
        public List<Task> SummonTasksByDate(DateTime selectedDay)
        {// The default way to populate a list of tasks that need to be displayed, this gets them by date and sorts them by time
            List<Task> currentTasks = new List<Task> { };
            int count;
            using (StreamReader sr = new StreamReader(filePath))
            {// Pull all the data from the JSON for the logged in user
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    count = desrlzdjson.TaskList.Count;
                    for (int i = 0; i < count; i++)
                    {// Get all our tasks and loop through all of them selecting the ones according to the currently chosen date (should be todays date by default)
                        if (desrlzdjson.TaskList[i].TaskDue.Date == selectedDay)
                        {// Add them to a list
                            currentTasks.Add(desrlzdjson.TaskList[i]);
                        }
                    }
                }
            }// Next we sort our tasks by date and time (but really just by time because we know they are all due on the same day from above)
            currentTasks.Sort(delegate (Task x, Task y) { return x.TaskDue.Date.CompareTo(y.TaskDue); });            
            return currentTasks;
        }
        public List<Task> SummonTasksByName(string taskName)
        {// Pull all the data from the JSON for the logged in user
            int count;
            List<string> taskSummon = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {// Get all our tasks and loop through all of them selecting the ones according to the name entered by the user using EscapeLoop()
                var json = sr.ReadToEnd();
                JSONClass desrlzdjson = JsonConvert.DeserializeObject<JSONClass>(json);
                if (desrlzdjson.TaskList != null)
                {
                    count = desrlzdjson.TaskList.Count;
                    for (int i = 0; i < count; i++)
                    {// Get every task from the JSON that matches and put them all in a list
                        if (desrlzdjson.TaskList[i].TaskName == taskName)
                        {
                            currentTasks.Add(desrlzdjson.TaskList[i]);
                        }
                    }
                }
            }// Just in case we still sort our tasks by date and time in case there are more than one with the same name
            currentTasks.Sort(delegate (Task x, Task y) { return x.TaskDue.Date.CompareTo(y.TaskDue); });
            return currentTasks;
        }
        public List<string> Task2String(List<Task> tasks)
        {
            int count;
            string addTask;
            List<string> taskSummon = new List<string>();
            count = tasks.Count;
            for (int i = 0; i < count; i++)
            {// Loop through our list and send our tasks to the return value               
                addTask = tasks[i].TaskName + " - " + tasks[i].TaskDue.ToString("g") + "; ";
                taskSummon.Add(addTask);
            }
            return taskSummon;
        }
        public string EscapeLoop(string input)
        {
            var start = new MainLoop();
            while (true) 
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                if (key.Key == ConsoleKey.Backspace)
                {// This whole mess lets us backspace when typing with this method
                    if (input == null || input.Length == 0)
                    {
                        continue;
                    }
                    else
                    {
                        Console.Write("\b");
                        Console.Write(' ');
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        string backspace = input.Remove(input.Length - 1);
                        input = backspace;
                        continue;
                    }
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    if (!isLoggedIn)
                    {
                        start.Main();
                        break;
                    }
                    if (calView)
                    {
                        start.Main();
                        break;
                    }
                    else
                    {
                        GlobalVal.remvTask = false;
                        GlobalVal.addTask = false;
                        GlobalVal.taskList = true;
                        start.Main();
                        break;
                    }
                }
                else//if (key.Key != ConsoleKey.Escape || key.Key == ConsoleKey.Enter || key.Key != ConsoleKey.Backspace)
                {
                    Console.Write(key.KeyChar);
                    input += key.KeyChar;
                }
            }
            return input;
        }
        public void DateRollover()
        {
            if (int.Parse(dayNumber[0]) + GlobalVal.monthAdjust > 12)
            {
                GlobalVal.yearAdjust += 1;
                GlobalVal.monthAdjust = (int.Parse(dayNumber[0]) - (int.Parse(dayNumber[0]) * 2)) + 1;
            }
            if (int.Parse(dayNumber[0]) + GlobalVal.monthAdjust < 1)
            {
                GlobalVal.yearAdjust -= 1;
                GlobalVal.monthAdjust = 12 - int.Parse(dayNumber[0]);
            }
        }

        //Below are a list of variables I need for the login page
        //All of these can be used outside this class as well using the public var
        private static bool loginStatus = false;
        public static bool isLoggedIn
        {
            //Tracks when a user is logged in as y or n to determine if
            //they should move from the login page to the main menu
            get { return loginStatus; }
            set { loginStatus = value; }
        }

        private static string activeUser = null;
        public static string displayName
        {
            //Sets display name at login so that on the main menu
            //it will say User: <username> at the top of the page
            get { return activeUser; }
            set { activeUser = value; }
        }

        private static string tempPath = null;
        public static string filePath
        {
            //Determines the file path here at login either when creating account
            //or when logging into an account
            get { return tempPath; }
            set { tempPath = value; }
        }

        private static bool isCalendar = false;
        public static bool calView
        {
            //Lets us which menu we are on, this one is for the calendar menu
            get { return isCalendar; }
            set { isCalendar = value; }
        }

        private static bool isTaskList = false;
        public static bool taskList
        {
            //Lets us track what menu we are on, this one is for the Task Menu
            get { return isTaskList; }
            set { isTaskList = value; }
        }
        private static bool isAddTask = false;
        public static bool addTask
        {
            //Lets us track what menu we are on, this one is for the adding a task
            get { return isAddTask; }
            set { isAddTask = value; }
        }
        private static bool isRemvTask = false;
        public static bool remvTask
        {
            //Lets us track what menu we are on, this one is for the removing a task
            get { return isRemvTask; }
            set { isRemvTask = value; }
        }

        public static string dir = AppContext.BaseDirectory;
        public static string profiles = dir + @"profiles\";

        private static int userError = -1;
        public static int errorNumber
        {
            //This tracks the errors for the login page, there are only 3
            get { return userError; }
            set { userError = value; }
        }

        private static bool inputBool = false;
        public static bool Input
        {
            get { return inputBool; }
            set { inputBool = value; }
        }

        private static string isTaskName = null;
        public static string taskName
        {
            get { return isTaskName; }
            set { isTaskName = value; }
        }

        public string date = DateTime.Now.ToString("D");//The date and time right NOW
        public int monthListNum = DateTime.Now.Month - 1;//Used to pull the month from an indexed list so 0 is January
        public string[] dayNumber = DateTime.Now.ToShortDateString().Split('/');//index 0 = month, index 1 = day, index 2 = year
        public string[] dateArray = DateTime.Now.ToString("D").Replace(", ", ",").Split(",");//Make the date an array to get just month, dd for display purposes
        public DateTime selectedDay = DateTime.Now.Date;

        private static int dayAdjustment = 0;
        public static int dayAdjust
        {
            get { return dayAdjustment; }
            set { dayAdjustment = value; }
        }

        private static int monthAdjustment = 0;
        public static int monthAdjust
        {
            get { return monthAdjustment; }
            set { monthAdjustment = value; }
        }

        private static int yearAdjustment = 0;
        public static int yearAdjust
        {
            get { return yearAdjustment; }
            set { yearAdjustment = value; }
        }

        private static List<Task> chosenTasks; 
        public static List<Task> currentTasks
        {
            get { return chosenTasks; }
            set { chosenTasks = value; }
        }

        public int screenWidth = Console.WindowWidth - 1;//Listed as a number of columns, each column is the width of one constant width character
    }
}