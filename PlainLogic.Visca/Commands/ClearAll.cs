using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca.Commands
{
    /// <summary>
    /// Clear All
    /// 
    /// Clear all of the devices, halting any pending commands.  VISCA Control uses this when resetting the
    /// VISCA network
    /// </summary>
    class ClearAll : ViscaCommand 
    {
        public ClearAll() : base( CommandTypes.Command, CommandCategories.Interface, true ) { }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x01 ) );
        }
    }
}
