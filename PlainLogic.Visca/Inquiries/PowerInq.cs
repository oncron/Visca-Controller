using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Responses;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca.Inquiries
{
    public class PowerInq : ViscaCommand
    {
        public PowerInq()
            : base( CommandTypes.Inquiry, CommandCategories.Camera, false )
        {
            InquiryResponseType = typeof( PowerInq_Response );
        }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x00 ) );
        }
    }
}
