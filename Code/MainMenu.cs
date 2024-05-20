using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasTrack;

namespace TasTrack
{
    internal class MainMenu
    {
        public MainMenu()
        {
            Printer menuPrinter = new Printer();

            menuPrinter.menuText = "1: Calendar\n2: View Tasks\n3: Add A Task\n4: Remove A Task\n5: Logout";
            menuPrinter.Print();

            try
            {
                int select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    //case 1:

                    case 5:
                        Login.isLoggedIn = "n";
                        Login.displayName = null;
                        var start = new TT();
                        start.Main();
                        break;
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine("Oops! Please select a valid option.\n");
            }
        }
    }
}
