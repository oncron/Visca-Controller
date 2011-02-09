using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PlainLogic.ViscaController.App
{
    public static class VISCACommands
    {

        static VISCACommands()
        {
            TiltUp = new RoutedUICommand( "Tilt Camera Up", "Tilt Up", typeof( VISCACommands ) );
            TiltDown = new RoutedUICommand( "Tilt Camera Down", "Tilt Down", typeof( VISCACommands ) );
            PanLeft = new RoutedUICommand( "Pan Camera Left", "Pan Left", typeof( VISCACommands ) );
            PanRight = new RoutedUICommand( "Pan Camera Right", "Pan Right", typeof( VISCACommands ) );
        }

        public static RoutedUICommand TiltUp { get; private set; }
        public static RoutedUICommand TiltDown { get; private set; }
        public static RoutedUICommand PanLeft { get; private set; }
        public static RoutedUICommand PanRight { get; private set; }
    }
}
