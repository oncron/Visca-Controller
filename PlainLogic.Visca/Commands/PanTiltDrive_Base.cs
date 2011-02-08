using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Commands
{
    abstract class PanTiltDrive_Base : ViscaCommand 
    {
        protected PanTiltDrive_Base() : base( CommandTypes.Command, CommandCategories.PanTilter, false ) { }

        byte _panSpeed = 1;
        public byte PanSpeed
        {
            get { return _panSpeed; }
            set
            {
                if( _panSpeed < 1 || _panSpeed > 18 ) throw new ArgumentOutOfRangeException();

                _panSpeed = value;
            }
        }

        byte _tiltSpeed = 1;
        public byte TiltSpeed
        {
            get { return _tiltSpeed; }
            set
            {
                if( _tiltSpeed < 1 || _tiltSpeed > 17 ) throw new ArgumentOutOfRangeException();

                _tiltSpeed = value;
            }

        }
    }
}
