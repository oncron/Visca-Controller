using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PlainLogic.ViscaController.Application
{
    public static class VISCACommands
    {

        public static RoutedUICommand Tilt { get; private set; }
        public static RoutedUICommand Pan { get; private set; }
        public static RoutedUICommand Zoom { get; private set; }

        static VISCACommands()
        {
            Tilt = new RoutedUICommand( "Tilt Camera", "Tilt", typeof( VISCACommands ) );
            Pan = new RoutedUICommand( "Pan Camera", "Pan", typeof( VISCACommands ) );
            Zoom = new RoutedUICommand( "Zoom Camera", "Zoom", typeof( VISCACommands ) );
        }

    }
}
