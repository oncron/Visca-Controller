using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Commands;

namespace Server
{
    class Program
    {
        static void Main()
        {
            CAM_Title_Set cmd = new CAM_Title_Set();
            cmd.Color = 5;
            cmd.Blink = 3;
            cmd.Title = "teswting hello!";
            cmd.VPosition = 200;

            var results = cmd.GetCommandMessages();
        }

    }
}
