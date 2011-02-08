using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Packets
{
    public abstract class Packet
    {
        internal byte[] GetDataBytes_Internal()
        {
            return GetDataBytes();
        }

        protected abstract byte[] GetDataBytes();
    }
}
