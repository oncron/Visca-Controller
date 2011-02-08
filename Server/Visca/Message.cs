using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca
{
    class Message
    {
        List<Packet> _packets = new List<Packet>();

        public List<Packet> Packets { get { return _packets; } }


    }
}
