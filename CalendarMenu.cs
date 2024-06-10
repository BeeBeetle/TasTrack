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
        List<string> currentTasks = new List<string> { };
        public CalendarMenu()
        {
            var calendarMenu = new CalendarBuilder();
            Printer calendarPrinter = new Printer();
            string input = null; 
            string[] monthDay = new DateTime(     // This will set us up to make sure that we pull the current
                int.Parse(globalVal.dayNumber[2]),// day as "January 1" or "August 15" based on the current values
                int.Parse(globalVal.dayNumber[0]),// for the adjustment variables. This will automatically roll over
                int.Parse(globalVal.dayNumber[1]) // so March 30th would turn into Feb 29th which turns into Jan 30th. 
                ).AddDays(GlobalVal.dayAdjust).AddMonths(GlobalVal.monthAdjust).ToString("D").Replace(", ", ",").Split(",");
            currentTasks = globalVal.SummonTasksByDate(globalVal.selectedDay.AddDays(GlobalVal.dayAdjust).AddMonths(GlobalVal.monthAdjust).AddYears(GlobalVal.yearAdjust));
            int year = int.Parse(globalVal.dayNumber[2]);// Pulls the year from DateTime            The line above just sets our JSON reader to get the tasks for the current date
            int month = int.Parse(globalVal.dayNumber[0]);// Pulls the month from DateTime          based on the current adjustment values
            int today = int.Parse(globalVal.dayNumber[1]);// Pulls the day number from DateTime
            int daysInMonth = DateTime.DaysInMonth(year, month);
            calendarPrinter.menuText = "1: Select a day" +
                                     "\n2: Previous Month" +
                                     "\n3: Next Month" +
                                     "\n4: Tasks Menu" +
                                     "\n5: Return to Main Menu";
            if (GlobalVal.errorNumber == 0) { calendarPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVal.errorNumber == 1) { calendarPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVal.errorNumber == 2) { calendarPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVal.errorNumber == 3) { calendarPrinter.oopsyDesc = "Your passwords do not match. Please try again."; }
            if (GlobalVal.errorNumber == 4) { calendarPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            if (GlobalVal.errorNumber == 5) { calendarPrinter.oopsyDesc = "That day is outside of this month silly!"; }
            calendarPrinter.PrintTitle();
            Console.WriteLine(calendarMenu.Format(calendarMenu.Create(), monthDay[1] + ": " + String.Join(',', currentTasks.ToArray())));
            calendarPrinter.PrintMenu();
            calendarPrinter.PrintChoice();
            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                GlobalVal.errorNumber = -1;
                switch (select)
                {
                    case 1:
                        int output = 0;
                        Console.Write("Select a day by number: ");
                        try { output = int.Parse(globalVal.EscapeLoop(input)); }
                        catch { GlobalVal.errorNumber = 2; }
                        if (output > daysInMonth || output < daysInMonth)
                        {
                            GlobalVal.errorNumber = 5;
                            break;
                        }
                        GlobalVal.dayAdjust = output - int.Parse(globalVal.dayNumber[1]);
                        break;
                    case 2:
                        GlobalVal.monthAdjust -= 1;
                        break;
                    case 3:
                        GlobalVal.monthAdjust += 1;
                        break;
                    case 4:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.calView = false;
                        GlobalVal.taskList = true;
                        break;
                    case 5:
                        GlobalVal.dayAdjust = 0;
                        GlobalVal.monthAdjust = 0;
                        GlobalVal.yearAdjust = 0;
                        GlobalVal.calView = false;
                        var start = new MainLoop();
                        start.Main();
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
