using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    /// <summary>
    /// Power ON/OFF
    /// </summary>
    class CAM_Power : CommandBase
    {
        public enum Commands : byte 
        { 
            On = 0x02, 
            Off = 0x03 
        }

        public CAM_Power()
            : base( false )
        {
            Command = Commands.On;
        }

        public Commands Command { get; set; }

        protected override Packet[] GetDataPackets()
        {
            return new Packet[] { 
                new CustomPacket( 0x01, 0x04, 0x00, (byte)Command),
            };
        }
    }
}
