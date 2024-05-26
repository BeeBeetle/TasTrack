using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasTrack;

var start = new TT();
start.Main();

public class TT
{
    public void Main()
    {
        if (!Directory.Exists(GlobalVar.profiles))
        {
            Directory.CreateDirectory(GlobalVar.profiles);
        }
        while (GlobalVar.isLoggedIn == "n")
        {
            var login = new Login();
            login.GetLogin();
        }
<<<<<<< Updated upstream:Code/LandingPage.cs
        while (GlobalVar.isLoggedIn == "y")
=======
        while (GlobalVar.isLoggedIn == true && GlobalVar.calView == false && GlobalVar.taskList == false)
>>>>>>> Stashed changes:Code/Main.cs
        {
            MainMenu menu = new MainMenu();
        }
        while (GlobalVar.taskList == true)
        {// change this later to be TaskList once class is complete
            var menu = new MainMenu();
        }
    }
}

