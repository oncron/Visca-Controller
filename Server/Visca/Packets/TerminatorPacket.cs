using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Visca.Packets
{
    class TerminatorPacket : Packet
    {
        public override byte[] GetDataBytes()
        {
            return new byte[] { 0xFF };
        }
    }
}
