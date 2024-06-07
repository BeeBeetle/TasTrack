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
        while (GlobalVar.isLoggedIn == true && GlobalVar.calView == false && GlobalVar.taskList == false && GlobalVar.isAddTask == false && GlobalVar.isRemvTask == false)
        {
            var menu = new MainMenu();
        }
        while (GlobalVar.calView == true)
        {
            var calendarPrinter = new CalendarMenu();
        }
        while (GlobalVar.taskList == true)
        {
            var taskView = new TasksMenu();
        }
        while (GlobalVar.isAddTask == true)
        {
            var taskAdd = new TaskAdd();
        }
        while (GlobalVar.isRemvTask == true)
        {
            var taskRemv = new TaskRemove();
        }
    }
}

