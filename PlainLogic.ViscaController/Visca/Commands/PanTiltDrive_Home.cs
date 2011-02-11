using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Visca.Packets;

namespace PlainLogic.ViscaController.Visca.Commands
{
    class PanTiltDrive_Home : ViscaCommand 
    {
        public enum Modes : byte
        {
            Home = 4,
            Reset = 5
        }

        public PanTiltDrive_Home() : base( CommandTypes.Command, CommandCategories.PanTilter, false ) { }

        public Modes Mode { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( (byte)Mode ) );
        }
    }
}
