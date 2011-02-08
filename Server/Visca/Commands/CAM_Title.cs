using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class CAM_Title : CommandBase 
    {
        public CAM_Title()
            : base( false )
        {
            Display = true;
        }

        public bool Display { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x01, 0x04, 0x74, (byte)( Display ? 0x02 : 0x03 ) ) );
        }
    }
}
