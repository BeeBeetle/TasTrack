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
        GlobalVal globalVal = new GlobalVal();
        List<Task> currentTasks = new List<Task> { };
        public CalendarMenu()
        {
            var calendarMenu = new CalendarBuilder();
            Printer calendarPrinter = new Printer();
            string input = null; 
            string[] monthDay = new DateTime(     // This will set us up to make sure that we pull the current
                int.Parse(globalVal.dayNumber[2]),// day as "January 1" or "August 15" based on the current values
                int.Parse(globalVal.dayNumber[0]),// for the adjustment variables. This will automatically roll over
                int.Parse(globalVal.dayNumber[1]) // so March 30th would turn into Feb 29th which turns into Jan 30th. 
                ).AddDays(GlobalVal.dayAdjust).AddMonths(GlobalVal.monthAdjust).ToString("D").Split(", ");
            GlobalVal.currentTasks = globalVal.SummonTasksByDate(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust).AddMonths(GlobalVal.monthAdjust).AddYears(GlobalVal.yearAdjust));
            string printableTasks =String.Join("", globalVal.Task2String(GlobalVal.currentTasks).ToArray());
            int year = int.Parse(globalVal.dayNumber[2]);// Pulls the year from DateTime            The line above just sets our JSON reader to get the tasks for the current date
            int month = int.Parse(globalVal.dayNumber[0]);// Pulls the month from DateTime          based on the current adjustment values
            int today = int.Parse(globalVal.dayNumber[1]);// Pulls the day number from DateTime
            int daysInMonth = DateTime.DaysInMonth(year, month);
            calendarPrinter.menuText = "1: Return to Main Menu\n2: Tasks Menu\n3: Pick a Day\n4: Enter a Date\n5: Previous Month\n6: Next Month";
            if (GlobalVal.errorNumber == 0) { calendarPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { calendarPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVal.errorNumber == 2) { calendarPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVal.errorNumber == 3) { calendarPrinter.oopsyDesc = "That wasn't a number! I need the day as a number."; }
            if (GlobalVal.errorNumber == 4) { calendarPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            if (GlobalVal.errorNumber == 5) { calendarPrinter.oopsyDesc = "Oops! I don't recognize that date! Did you include the slashes?"; }
            if (GlobalVal.errorNumber == 6) { calendarPrinter.oopsyDesc = "That year is outside of my scope."; }
            if (GlobalVal.errorNumber == 7) { calendarPrinter.oopsyDesc = "I don't think thats a month . . ."; }
            if (GlobalVal.errorNumber == 8) { calendarPrinter.oopsyDesc = "You entered a day that doesn't exist in the month you chose."; }
            calendarPrinter.PrintTitle();
            currentTasks.ToArray();
            Console.WriteLine(calendarMenu.Format(calendarMenu.Create(), monthDay[1] + ": " + printableTasks));
            calendarPrinter.PrintMenu();
            calendarPrinter.PrintChoice();
            try
            {
                int[] thirtyDay = { 4, 6, 9, 11 };
                int[] thirtyoneDay = { 1, 3, 5, 7, 8, 10, 12 };
                int y;
                int m;
                int d;
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVal.errorNumber = -1;
                switch (select)
                {
                    case 1:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.calView = false;
                        var start = new MainLoop();
                        start.Main();
                        break;
                    case 2:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.calView = false;
                        GlobalVal.taskList = true;
                        break;
                    case 3:
                        Console.Write("Please enter a day as DD: ");
                        string dayInput = globalVal.EscapeLoop(input);
                        if (dayInput != null)
                        {
                            try { int.Parse(dayInput); }
                            catch { GlobalVal.errorNumber = 3; break; }
                        }
                        d = int.Parse(dayInput);
                        GlobalVal.dayAdjust = d - today;
                        break;
                    case 4:
                        Console.Write("Please enter a date as MM/DD/YYYY: ");
                        string dateInput = globalVal.EscapeLoop(input);
                        string[] dateOutput = dateInput.Split('/'); ;// We split the date because we need the parts as numbers
                        if (dateInput != null)
                        {// Lets make sure that the date is theoretically valid and has a month day and year entered
                            try { int.Parse(dateOutput[2]); int.Parse(dateOutput[0]); int.Parse(dateOutput[1]); }
                            catch { GlobalVal.errorNumber = 5; break; }
                        }
                        y = int.Parse(dateOutput[2]);
                        m = int.Parse(dateOutput[0]);
                        d = int.Parse(dateOutput[1]);
                        if (y > 9999 || y < 0)
                        {
                            GlobalVal.errorNumber = 6;
                            break;
                        }
                        if (m > 13 || m < 0)
                        {// I split the month/year validity checks because I want two different error messages
                            GlobalVal.errorNumber = 7;
                            break;
                        }
                        else
                        {// If the year is good and the month is good then we can move on to checking if day entered is valid
                            if (thirtyDay.Contains(m) && d > 30 || thirtyoneDay.Contains(m) && d > 31)
                            {
                                GlobalVal.errorNumber = 8;
                                break;
                            }
                            if (m == 2)
                            {
                                bool leapYear = DateTime.IsLeapYear(y);// Easy leap year check
                                if (!leapYear && d > 28 || leapYear && d > 29)
                                {
                                    GlobalVal.errorNumber = 8;
                                    break;
                                }
                            }
                        }
                        GlobalVal.yearAdjust = y - year;
                        GlobalVal.monthAdjust = m - month;
                        GlobalVal.dayAdjust = d - today;
                        break;
                    case 5:
                        GlobalVal.monthAdjust -= 1;
                        break;
                    case 6:
                        GlobalVal.monthAdjust += 1;
                        break;
                    default:
                        GlobalVal.errorNumber = 0;
                        break;
                }
            }
            catch
            {
                GlobalVal.errorNumber = 4;
            }
        }
    }
}
