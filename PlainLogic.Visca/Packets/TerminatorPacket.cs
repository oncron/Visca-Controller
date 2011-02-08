using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Packets
{
    public sealed class TerminatorPacket : Packet
    {
        protected override byte[] GetDataBytes()
        {
            return new byte[] { 0xFF };
        }
    }
}
