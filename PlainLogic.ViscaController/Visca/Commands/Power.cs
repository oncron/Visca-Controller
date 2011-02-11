using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Visca.Packets;

namespace PlainLogic.ViscaController.Visca.Commands
{
    /// <summary>
    /// Power ON/OFF
    /// </summary>
    class Power : ViscaCommand
    {
        public enum Commands : byte 
        { 
            On = 0x02, 
            Off = 0x03 
        }

        public Power()
            : base( CommandTypes.Command, CommandCategories.Camera, false )
        {
            Command = Commands.On;
        }

        public Commands Command { get; set; }

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new LiteralBytesPacket( 0x00, (byte)Command ) );
        }
    }
}
