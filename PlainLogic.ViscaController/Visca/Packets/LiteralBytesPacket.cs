using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Packets
{
    public class LiteralBytesPacket : Packet
    {
        byte[] _data;

        public LiteralBytesPacket( params byte[] data )
        {
            _data = data;
        }

        protected override byte[] GetDataBytes()
        {
            return _data;
        }
    }
}
