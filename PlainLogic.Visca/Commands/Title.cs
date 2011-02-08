using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca.Commands
{
    class Title : ViscaCommand 
    {
        public Title()
            : base( CommandTypes.Command, CommandCategories.Camera, false )
        {
            Display = true;
        }

        public bool Display { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x74, (byte)( Display ? 0x02 : 0x03 ) ) );
        }
    }
}
