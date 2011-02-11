using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PlainLogic.ViscaController.Application
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            _timer = new DispatcherTimer( TimeSpan.FromSeconds( 1.0 / 30.0 ), DispatcherPriority.Normal, Tick, Dispatcher );
        }

        int _lastPanCmd = 0;
        int _lastTiltCmd = 0;
        int _lastZoom = 0;

        private void CommandBinding_Executed( object sender, ExecutedRoutedEventArgs e )
        {
            RoutedUICommand cmd = (RoutedUICommand)e.Command;

            temp.Text = string.Format( "{0} ({1})\n{2}",
                cmd.Name,
                e.Parameter,
                temp.Text );

            if( cmd == VISCACommands.Pan )
            {
                _lastPanCmd = (int)e.Parameter;
            }
            else if( cmd == VISCACommands.Tilt )
            {
                _lastTiltCmd = (int)e.Parameter;
            }
            else if( cmd == VISCACommands.Zoom )
            {
                _lastZoom = (int)e.Parameter;
            }
        }

        void Tick( object sender, EventArgs args )
        {
            Point pt = Point.Add( cameraField.FocalPointLocation, new Vector( _lastPanCmd / 10.0, _lastTiltCmd / 10.0 ) );

            cameraField.FocalPointLocation = pt;


            cameraField.FocalPointSize = new Size(
                cameraField.FocalPointSize.Width + ( _lastZoom / 1000.0 ),
                cameraField.FocalPointSize.Height + ( _lastZoom / 1000.0 ) );

        }
    }
}
