using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Visca.Responses;
using PlainLogic.ViscaController.Visca.Packets;

namespace PlainLogic.ViscaController.Visca.Inquiries
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
