using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Packets
{
    public class FillerPacket : Packet 
    {
        int _count;
        byte _filler;

        public FillerPacket( int byteCount, byte filler )
        {
            _count = byteCount;
            _filler = filler;
        }

        protected override byte[] GetDataBytes()
        {
            byte[] data = new byte[_count];

            for( int i = 0; i < _count; i++ )
            {
                data[i] = _filler;
            }

            return data;
        }
    }
}
