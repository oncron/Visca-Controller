using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Packets
{
    public class HalfBytePacket : Packet 
    {
        public HalfBytePacket() { }

        public HalfBytePacket( byte highHalf, byte lowHalf )
        {
            HighHalf = highHalf;
            LowHalf = lowHalf;
        }

        public byte HighHalf { get; set; }
        public byte LowHalf { get; set; }

        protected override byte[] GetDataBytes()
        {
            return new byte[] { (byte)((HighHalf << 4) + LowHalf) };
        }
    }
}
