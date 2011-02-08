using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Packets
{
    public class StringPacket : Packet
    {
        public string Value { get; set; }

        protected override byte[] GetDataBytes()
        {
            return ASCIIEncoding.ASCII.GetBytes( Value );
        }
    }
}
