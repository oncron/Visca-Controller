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

            cmd.Title = "President Carlson";
            cmd.Color = 5;
            cmd.Blink = 4;
            cmd.HPosition = 50;
            cmd.VPosition = 75;

            

            var results = cmd.GetDataBytes();
        }

    }
}
