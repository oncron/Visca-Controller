using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    /// <summary>
    /// Clear All
    /// 
    /// Clear all of the devices, halting any pending commands.  VISCA Control uses this when resetting the
    /// VISCA network
    /// </summary>
    class IF_Clear : CommandBase 
    {
        public IF_Clear() : base( true ) { }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x01, 0x00, 0x01 ) );
        }
    }
}
