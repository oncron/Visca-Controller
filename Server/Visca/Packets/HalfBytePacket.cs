using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Visca.Packets
{
    class HalfBytePacket : Packet 
    {
        public byte HighHalf { get; set; }
        public byte LowHalf { get; set; }

        public override byte[] GetDataBytes()
        {
            return new byte[] { (byte)((HighHalf << 4) + LowHalf) };
        }
    }
}
