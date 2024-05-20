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
        while (Login.isLoggedIn == "n")
        {
            var login = new Login();
            login.GetLogin();
        }
        while (Login.isLoggedIn == "y")
        {
            MainMenu menu = new MainMenu();
        }
    }
}

