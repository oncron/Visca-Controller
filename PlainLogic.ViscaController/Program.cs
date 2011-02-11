using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Application;
using PlainLogic.ViscaController.Server;

namespace PlainLogic.ViscaController
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ServerEngine engine = new ServerEngine();

            engine.Start( true );

            App.Main();

            engine.Stop();
        }
    }
}
