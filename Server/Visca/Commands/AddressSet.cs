using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    /// <summary>
    /// Address Setting
    /// 
    /// Resets the address of each device in the VISCA chain.  The response packet tells the controller how
    /// many devices are present in the the VISCA network. VISCA Control uses this when resetting the VISCA network.
    /// </summary>
    class AddressSet : CommandBase 
    {
        public AddressSet()
            : base( true )
        {

        }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x30, 0x01 ) );
        }
    }
}
