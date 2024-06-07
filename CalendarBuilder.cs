using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class CalendarBuilder
    {
        GlobalVar globalVar = new GlobalVar();
        public string Create()
        {
            StringBuilder output = new StringBuilder();
            DateTime now = DateTime.Now;// To use the method DaysInMonth we have to initialize DateTime in this file
            var startDate = new DateTime(1980, 2, 1).ToString("D").Replace(", ", " ").Split(" "); //0 = the day of the week the 1st was on
            int daysInMonth = DateTime.DaysInMonth(1980, 2);
            string year = globalVar.dayNumber[3];// Pulls the year from an external use of DateTime
            int today = Convert.ToInt32(globalVar.dayNumber[2]);// Pulls the day number from an external use of DateTime
            string numTasks = Convert.ToString(00); // Takes the number of tasks for each day and puts it on the calendar
            string[] calHeader = 
                {// An indexed list of headers with the month from a use of DateTime that displays the month as a number and I subtract 1 to get the appropriate index
                "░░░░░░ January ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ February ░░░░░░░░░░░░ " + year + " ░░░░░░░░", 
                "░░░░░░░ March ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ April ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", 
                "░░░░░░░░ May ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ June ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", 
                "░░░░░░░ July ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░ August ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", 
                "░░░░░ September ░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░ October ░░░░░░░░░░░░ " + year + " ░░░░░░░░", 
                "░░░░░ November ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ December ░░░░░░░░░░░░ " + year + " ░░░░░░░░" 
                };
            string days = "░ S ░░░ M ░░░ T ░░░ W ░░░ T ░░░ F ░░░ S ░";
            // These are all charcter variables so that they can be changed if wanted to something else, maybe they will move to their own document so I can have different "styles" set up
            char extWall = '║';
            char intWall = '│';
            char extCornerTL = '╔';
            char extCornerBL = '╚';
            char extCornerTR = '╗';
            char extCornerBR = '╝';
            char intConnector = '┼';
            char extTBeamU = '╧';
            char intTBeamD = '┬';
            char extRowBar = '═';
            char intRowBar = '─';
            char extTBeamSL = '╟';
            char extTBeamSR = '╢';
            char extTBeamLL = '╠';
            char extTBeamLR = '╣';
            char spacer = '░';
            int rows = 0; // Just how many rows have been completed, 16 are needed in total
            bool initDay = false;
            int dayCount = 1;
            int waitTime = -1;

            while (true)
            {// Here we are going to build are calender row by row and then return it as a single long string for the format method
                for (int i = 0; i < 43; i++)
                {// This builds the top of the calendar
                    if (rows == 0)
                    {
                        if (i == 0)
                        {
                            output.Append(extCornerTL);
                        }
                        if (i == 41)
                        {
                            output.Append(extCornerTR);
                            break;
                        }
                        else
                        {
                            output.Append(extRowBar);
                        }
                    }
                    if (rows == 1)
                    {// This attactches the month name and the year to the header based off an indexed list 
                        if (i == 0)
                        {
                            output.Append(extWall);
                        }
                        if (i == 42)
                        {
                            output.Append(extWall);
                            break;
                        }
                        if (i == 1)
                        {// Because we put more than one character and we only need to do that once we can have i = 1 and then let i iterate with no effect
                            output.Append(calHeader[globalVar.monthListNum]);
                        }    
                    }
                    if (rows == 2)
                    {// This separates the month and year from the day labels
                        if (i == 0)
                        {
                            output.Append(extTBeamLL);
                        }
                        if (i == 41)
                        {
                            output.Append(extTBeamLR);
                            break;
                        }
                        else
                        {
                            output.Append(extRowBar);
                        }
                    }
                    if (rows == 3)
                    {// Gives us the day labels (S/M/T etc.)
                        if (i == 0)
                        {
                            output.Append(extWall);
                        }
                        if (i == 41)
                        {
                            output.Append(extWall);
                            break;
                        }
                        if (i == 1)
                        {// Because we put more than one character and we only need to do that once we can have i = 1 and then let i iterate with no effect
                            output.Append(days);
                        }
                    }
                    if (rows == 4)
                    {// This builds the thin line for the top of the days grid
                        if (i == 0)
                        {
                            output.Append(extTBeamSL);
                        }
                        if (i == 42)
                        {
                            output.Append(extTBeamSR);
                            break;
                        }
                        if (i % 6 == 0 && i != 0)
                        {
                            output.Append(intTBeamD);
                        }
                        if (i % 6 != 0 && i < 42)
                        {
                            output.Append(intRowBar);
                        }
                    }
                    if (rows % 2 == 0 && rows > 4 && rows != 16)
                    {// Thin interior lines separating the days in the main grid
                        if (i == 0)
                        {
                            output.Append(extTBeamSL);
                        }
                        if (i == 42)
                        {
                            output.Append(extTBeamSR);
                            break;
                        }
                        if (i % 6 == 0 && i != 0)
                        {
                            output.Append(intConnector);
                        }
                        if (i % 6 != 0 && i < 42)
                        {
                            output.Append(intRowBar);
                        }
                    }
                    if (rows % 2 != 0 && rows > 4)
                    {// For the internals of the day grid (days/task volume)
                        if (i == 0)
                        {
                            output.Append(extWall);
                        }
                        if (i == 42)
                        {
                            output.Append(extWall);
                            break;
                        }
                        if (i % 6 == 0 && i != 0)
                        {
                            output.Append(intWall);
                        }
                        if (i % 6 != 0 && i < 42)
                        {// This one might be a bit much
                            if (initDay == false)
                            {// As long as we haven't set the first day of the monthe we switch through the days to find our day
                                switch (startDate[0])
                                {// startDate[0] is a string value from DateTime for the day the 1st fell on
                                    case "Sunday":
                                        waitTime = 0;
                                        break;
                                    case "Monday":
                                        waitTime = 5; // These are multiplied by 5 here because in every instance other than filling the days
                                        break;        // we build the strings one character at a time, but for the date/task we do it as a chunk
                                    case "Tuesday":   // so we need to have these by x5 instead of 0/1/2 etc. because if say Wednesday was 3
                                        waitTime = 10;// then you would put three spacers then immediately 5 characters for the day/task and that
                                        break;        // would throw off the entire rest of the calendar.
                                    case "Wednesday":
                                        waitTime = 15;
                                        break;
                                    case "Thursday":
                                        waitTime = 20;
                                        break;
                                    case "Friday":
                                        waitTime = 25;
                                        break;
                                    case "Saturday":
                                        waitTime = 30;
                                        break;
                                }
                                initDay = true;// make sure we never come back here once we do it a single time
                            }
                            if (waitTime == 0 && dayCount <= daysInMonth)
                            {// If we are done waiting we put the initial day and then increment the day, if we hit the number of days in the month we stop
                                output.Append(dayCount.ToString("00") + ":" + "00"); // The second "00" will be a variable later for the number of tasks on that day
                                dayCount++;
                                i += 4; // We do +4 here to keep the calendar spaced correctly because we are inserting 5 characters (a full block) all at once
                            }
                            else
                            {
                                output.Append(spacer); // Back to printing one character at a time
                                if (waitTime > 0)
                                {// We don't need wait time to go below 0 because what would be the point of that . . .
                                    waitTime--; // I could subtract 1/5th from wait time, but then I'd need floating points, YUCK!
                                }                                
                            }
                        }
                    }
                    if (rows == 16)
                    {// Constructs the bottom of the calendar
                        if (i == 0)
                        {
                            output.Append(extCornerBL);
                        }
                        if (i == 42)
                        {
                            output.Append(extCornerBR);
                            break;
                        }
                        if (i % 6 == 0 && i != 0)
                        {
                            output.Append(extTBeamU);
                        }
                        if (i % 6 != 0 && i < 42)
                        {
                            output.Append(extRowBar);
                        }

                    }
                }
                rows++;
                if (rows > 16)
                {// Break us out of the loop only after 16 rows have been created
                    break;
                }
            }
            return output.ToString();
        }
        public string Format(string column1, string column2)
        {
            int column1Width = 43;
            int column2Width = globalVar.screenWidth - (column1Width+1);
            int loopCount = 0;
            int movedChar = 0;
            int truncate = 0;
            bool spaceCheck = false;

            StringBuilder output = new StringBuilder();

            while (true)
            {// the col variables set a string each full loop to the size we want for our columns
                string col1 = new string(column1.Skip<char>(loopCount * column1Width).Take<char>(column1Width).ToArray()).PadRight(column1Width);
                string col2 = new string(column2.Skip<char>((loopCount * column2Width) - movedChar).Take<char>(column2Width).ToArray()).PadRight(column2Width);
                string truncCol2; // we are going to set this to a modified col2 later in the code
                for (int i = col2.Length - 1; i > 0; i--)
                {// count down from the end of the string to find the first space after which it will keep breaking out of the for loop
                    if (spaceCheck == true)
                    {// stop checking for spaces and break out of te for loop as soon as it starts
                        break;
                    }
                    else
                    {// check for the next space from the end of the string
                        if (col2[i] == ' ')
                        {// if a space is found we set up to break the for loop for remaining passes on the current line
                            spaceCheck = true;
                            break;
                        }
                        else
                        {// these govern the number of characters we need to include/exclude when writing the line
                            movedChar++; // the number of characters we have to add to the front of the next line
                            truncate++; // the number of characters we cut off from the end of the current line
                        }
                    }
                }
                truncCol2 = col2.Remove(column2Width - truncate);
                truncate = 0; // we reset the truncate with each loop so that we don't permantly change our column width
                spaceCheck = false; // we have to reset this so the next line can check for spaces too
                // break out of loop once all col variables contain only white space
                if (String.IsNullOrWhiteSpace(col1) && String.IsNullOrWhiteSpace(col2))
                {// when neither column has anything else to print
                    break;
                }
                output.AppendFormat("{0} {1}\n", col1, truncCol2); // this is what the console will print, two colums of the size defined above
                loopCount++;
            }
            return output.ToString();
        }
    }
}
