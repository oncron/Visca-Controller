using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Visca.Packets
{
    class CustomPacket : Packet
    {
        byte[] _data;

        public CustomPacket( params byte[] data )
        {
            _data = data;
        }

        public override byte[] GetDataBytes()
        {
            return _data;
        }
    }
}
