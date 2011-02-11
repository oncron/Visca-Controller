using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Packets
{
    public class LowHalfShortPacket : Packet 
    {
        public LowHalfShortPacket( short value ) { Value = value; }
        public LowHalfShortPacket() { }

        public short Value { get; set; }

        protected override byte[] GetDataBytes()
        {
            byte LL = (byte)((Value & 0xF000) >> 12);
            byte LM = (byte)((Value & 0x0F00) >> 8);
            byte RM = (byte)((Value & 0x00F0) >> 4);
            byte RR = (byte)(Value & 0x000F);

            return new byte[] { LL, LM, RM, RR };
        }
    }
}
