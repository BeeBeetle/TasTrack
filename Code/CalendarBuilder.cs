using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//"╔═════════════════════════════════════════╗" + This is the target calendar for now
//"║░░░░░░░░ May ░░░░░░░░░░░░░░ 2024 ░░░░░░░░║" + All the days are on the right day of the week
//"╠═════════════════════════════════════════╣" + If I can reporoduce this and then reproduce next month
//"║░ S ░░░ M ░░░ T ░░░ W ░░░ T ░░░ F ░░░ S ░║" + correctly then I think I've got it
//"╟─────┬─────┬─────┬─────┬─────┬─────┬─────╢" +
//"║░░░░░│░░░░░│░░░░░│01:00│02:00│03:00│04:00║" +
//"╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢" +
//"║05:00│06:00│07:00│08:00│09:00│10:00│11:00║" +
//"╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢" +
//"║12:00│13:00│14:00│15:00│16:00│17:00│18:00║" +
//"╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢" +
//"║19:00│20:00│21:00│22:00│23:00│24:00│25:00║" +
//"╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢" +
//"║26:00│27:00│28:00│29:00│30:00│31:00│░░░░░║" +
//"╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢" +
//"║░░░░░│░░░░░│░░░░░│░░░░░│░░░░░│░░░░░│░░░░░║" +
//"╚═════╧═════╧═════╧═════╧═════╧═════╧═════╝"

namespace TasTrack
{
    internal class CalendarBuilder
    {
        GlobalVar globalVar = new GlobalVar();
        public string create()
        {
            StringBuilder output = new StringBuilder();
            List<int> forbidRow = new List<int> {0, 2, 4, 16};
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1).ToString("D").Replace(", ", " ").Split(" "); //0 = the day of the week the 1st was on
            int todayVal = Convert.ToInt32(globalVar.dayNumber[2]);
            string todayNum = globalVar.dayNumber[2];
            char[] todayArray = todayNum.ToCharArray();
            string todayStr = globalVar.dayNumber[0];
            string month = globalVar.dayNumber[1];
            string year = globalVar.dayNumber[3];
            string numTasks = Convert.ToString(00); // Takes the number of tasks for each day and puts it on the calendar
            string[] monthSelect = { "░░░░░░ January ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ February ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ March ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ April ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░░ May ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ June ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░░ July ░░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░ August ░░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ September ░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░░ October ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ November ░░░░░░░░░░░░ " + year + " ░░░░░░░░", "░░░░░ December ░░░░░░░░░░░░ " + year + " ░░░░░░░░" };
            string days = "░ S ░░░ M ░░░ T ░░░ W ░░░ T ░░░ F ░░░ S ░";
            // These are all charcter variables so that they can be changed if needed to something else, maybe they will move to their own document so I can have different "styles" set up
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
                    {
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
                        {
                            output.Append(monthSelect[globalVar.monthListNum]);
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
                    {
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
                        {
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
                        {
                            output.Append(spacer);
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
        public string format(string column1, string column2)
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
