using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class PanTiltDrive_Position : PanTiltDrive_Base
    {
        public enum Modes : byte
        {
            Absolute = 2,
            Relative = 3
        }

        public Modes Mode { get; set; }

        short _panPos = 0;
        public short PanPosition
        {
            get { return _panPos; }
            set
            {
                if( value < -2267 || value > 2267 ) throw new ArgumentOutOfRangeException();

                _panPos = value;
            }
        }

        short _tiltPos = 0;
        public short TiltPosition
        {
            get { return _tiltPos; }
            set
            {
                if( value < -400 || value > 1200 ) throw new ArgumentOutOfRangeException();

                _tiltPos = value;
            }
        }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage(
                new LiteralBytesPacket( 0x01, 0x06, (byte)Mode, (byte)PanSpeed, (byte)TiltSpeed ),
                new LowHalfShortPacket( PanPosition ),
                new LowHalfShortPacket( TiltPosition )
                );
        }

    }
}
