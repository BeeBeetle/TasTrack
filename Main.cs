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
        GlobalVal globVar = new GlobalVal();
        if (!Directory.Exists(GlobalVal.profiles))
        {
            Directory.CreateDirectory(GlobalVal.profiles);
        }
        while (GlobalVal.isLoggedIn == false)
        {
            var login = new Login();
        }
        while (GlobalVal.isLoggedIn == true && GlobalVal.calView == false && GlobalVal.taskList == false && GlobalVal.addTask == false && GlobalVal.remvTask == false)
        {
            var menu = new MainMenu();
        }
        while (GlobalVal.calView == true)
        {
            var calendarPrinter = new CalendarMenu();
        }
        while (GlobalVal.taskList == true)
        {
            var taskView = new TasksMenu();
        }
        while (GlobalVal.addTask == true)
        {
            var taskAdd = new TaskAdd();
        }
        while (GlobalVal.remvTask == true)
        {
            var taskRemv = new TaskRemove();
        }
    }
}

