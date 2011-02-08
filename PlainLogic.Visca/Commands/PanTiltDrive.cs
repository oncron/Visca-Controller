using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca.Commands
{
    class PanTiltDrive : PanTiltDrive_Base 
    {

        public enum PanOptions : byte
        {
            Left = 1,
            Right = 2,
            Stop = 3
        }

        public enum TiltOptions : byte
        {
            Up = 1,
            Down = 2,
            Stop = 3
        }

        public PanTiltDrive()
        {
            Pan = PanOptions.Stop;
            Tilt = TiltOptions.Stop;
        }

        public PanOptions Pan { get; set; }
        public TiltOptions Tilt { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x01, PanSpeed, TiltSpeed, (byte)Pan, (byte)Tilt ) );
        }
    }
}
