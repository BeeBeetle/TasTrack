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
            calendarPrinter.menuText = "1: Select a day\n2: Next Month\n3: Previous Month\n4: Tasks Menu\n5: Return to Main Menu";
            if (GlobalVar.errorNumber == 0) { calendarPrinter.oopsyDesc = "Oops! Please select a valid option."; }
            if (GlobalVar.errorNumber == 1) { calendarPrinter.oopsyDesc = "Oops! You can't do that yet!"; }
            if (GlobalVar.errorNumber == 2) { calendarPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid."; }
            if (GlobalVar.errorNumber == 3) { calendarPrinter.oopsyDesc = "Your passwords do not match. Please try again."; }
            if (GlobalVar.errorNumber == 4) { calendarPrinter.oopsyDesc = "You gotta enter a number before you can continue!"; }
            calendarPrinter.PrintTitle();
            Console.WriteLine(calendarMenu.Format(calendarMenu.Create(),
            globalVar.dateArray[1] + ": " + "Gaius Julius Caesar Augustus (born Gaius Octavius; 23 September 63 BC – 19 August AD 14), also known as Octavian (Latin: Octavianus), was the founder of the Roman Empire. He reigned as the first Roman emperor from 27 BC until his death in AD 14.[a] The reign of Augustus initiated an imperial cult, as well as an era of imperial peace (the Pax Romana or Pax Augusta) in which the Roman world was largely free of armed conflict (aside from expansionary wars and the Year of the Four Emperors, which occurred after Augustus' reign). The Principate system of government was established during his reign and lasted until the Crisis of the Third Century. Octavian was born into an equestrian branch of the plebeian gens Octavia. His maternal great-uncle Julius Caesar was assassinated in 44 BC, and Octavian was named in Caesar's will as his adopted son and heir; as a result, he inherited Caesar's name, estate, and the loyalty of his legions. He, Mark Antony, and Marcus Lepidus formed the Second Triumvirate to defeat the assassins of Caesar. Following their victory at the Battle of Philippi (42 BC), the Triumvirate divided the Roman Republic among themselves and ruled as de facto dictators.")
            );
            calendarPrinter.PrintMenu();
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
