using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Visca.Packets
{
    abstract class Packet : IFreezable
    {


        public abstract byte[] GetDataBytes();

    }
}
