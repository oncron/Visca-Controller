using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca
{
    class OutMessage
    {
        public OutMessage( Packet[] packets )
        {
            List<byte> byteData = new List<byte>();

            foreach( Packet packet in packets ) byteData.AddRange( packet.GetDataBytes_Internal() );

            Data = byteData.ToArray();
        }

        public byte[] Data { get; private set; }
    }
}
