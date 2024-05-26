using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasTrack;

var start = new MainLoop();
start.Main();

public class MainLoop
{
    public void Main()
    {
        GlobalVar globVar = new GlobalVar();
        if (!Directory.Exists(GlobalVar.profiles))
        {
            Directory.CreateDirectory(GlobalVar.profiles);
        }
        while (GlobalVar.isLoggedIn == false)
        {
            var login = new Login();
        }
        while (GlobalVar.isLoggedIn == true && GlobalVar.calView == false && GlobalVar.taskList == false)
        {
            var menu = new MainMenu();
        }
        while (GlobalVar.calView == true)
        {
            var calendarPrinter = new CalendarMenu();
        }
        while (GlobalVar.taskList == true)
        {// change this later to be TaskList once class is complete
            var menu = new MainMenu();
        }
    }
}

