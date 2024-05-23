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
        public CalendarMenu()
        {
            var calendarMenu = new CalendarBuilder();
            var globVar = new GlobalVar();
            Printer calendarPrinter = new Printer();
            calendarPrinter.menuText = "1: Select a day\n2: Add task\n3: Return to Main Menu";
            calendarPrinter.mainMenuSum = "Upcomming tasks\n";
            if (GlobalVar.errorNumber == 0)
            {
                calendarPrinter.oopsyDesc = "Oops! Please select a valid option.";
            }
            if (GlobalVar.errorNumber == 1)
            {
                calendarPrinter.oopsyDesc = "That profile doesn't exist.";
            }
            if (GlobalVar.errorNumber == 2)
            {
                calendarPrinter.oopsyDesc = "I'm sorry, the information you entered was invalid.";
            }
            if (GlobalVar.errorNumber == 3)
            {
                calendarPrinter.oopsyDesc = "Your passwords do not match. Please try again.";
            }
            if (GlobalVar.errorNumber == 4)
            {
                calendarPrinter.oopsyDesc = "Shit! Something broke, bad!";
            }
            calendarPrinter.PrintTitle();
            Console.WriteLine(calendarMenu.format(
                "╔═════════════════════════════════════════╗║░░░░░░░░ May ░░░░░░░░░░░░░░ 2024 ░░░░░░░░║╟─────┬─────┬─────┬─────┬─────┬─────┬─────╢║░░░░░│01:00│02:00│03:00│04:00│05:00│06:00║╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢║07:00│08:00│09:00│10:00│11:00│12:00│13:00║╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢║14:00│15:00│16:00│17:00│18:00│19:00│20:00║╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢║21:00│22:00│23:00│24:00│25:00│26:00│27:00║╟─────┼─────┼─────┼─────┼─────┼─────┼─────╢║28:00│29:00│30:00│31:00│░░░░░│░░░░░│░░░░░║╚═════╧═════╧═════╧═════╧═════╧═════╧═════╝",
            GlobalVar.dateArray[1] + ": " + "Gaius Julius Caesar Augustus (born Gaius Octavius; 23 September 63 BC – 19 August AD 14), also known as Octavian (Latin: Octavianus), was the founder of the Roman Empire. He reigned as the first Roman emperor from 27 BC until his death in AD 14.[a] The reign of Augustus initiated an imperial cult, as well as an era of imperial peace (the Pax Romana or Pax Augusta) in which the Roman world was largely free of armed conflict (aside from expansionary wars and the Year of the Four Emperors, which occurred after Augustus' reign). The Principate system of government was established during his reign and lasted until the Crisis of the Third Century. Octavian was born into an equestrian branch of the plebeian gens Octavia. His maternal great-uncle Julius Caesar was assassinated in 44 BC, and Octavian was named in Caesar's will as his adopted son and heir; as a result, he inherited Caesar's name, estate, and the loyalty of his legions. He, Mark Antony, and Marcus Lepidus formed the Second Triumvirate to defeat the assassins of Caesar. Following their victory at the Battle of Philippi (42 BC), the Triumvirate divided the Roman Republic among themselves and ruled as de facto dictators.")
            );
            calendarPrinter.PrintMenu();
            int select = Convert.ToInt32(Console.ReadLine());
            try
            {
                switch (select)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        GlobalVar.calView = false;
                        var start = new MainLoop();
                        start.Main();
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
