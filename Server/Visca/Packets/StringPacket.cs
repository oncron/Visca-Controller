using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Visca.Packets
{
    class StringPacket : Packet
    {
        public string Value { get; set; }

        public override byte[] GetDataBytes()
        {
            return ASCIIEncoding.ASCII.GetBytes( Value );
        }
    }
}
