using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class PanTiltDrive_Home : CommandBase 
    {
        public enum Modes : byte
        {
            Home = 4,
            Reset = 5
        }

        public PanTiltDrive_Home() : base( false ) { }

        public Modes Mode { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x01, 0x06, (byte)Mode ) );
        }
    }
}
