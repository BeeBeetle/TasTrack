using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasTrack
{
    internal class CalendarBuilder
    {
        public string format(string column1, string column2)
        {
            int column1Width = 43;
            int column2Width = 75; // I may try to find a way to make this dynamic based on the console's window size per user, no guarantees though
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
