using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca
{
    class Message
    {
        public Message( Packet[] packets )
        {
            List<byte> data = new List<byte>();

            foreach( Packet packet in packets ) data.AddRange( packet.GetDataBytes() );

            Data = data.ToArray();
        }

        public byte[] Data { get; private set; }


    }
}
