using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class CommandCancel : CommandBase
    {
        public CommandCancel() : base( false ) { }

        byte _socket = 1;
        public byte Socket
        {
            get { return _socket; }
            set
            {
                if( value < 1 || value > 2 ) throw new ArgumentOutOfRangeException();

                _socket = value;
            }
        }

        protected override Packet[] GetDataPackets()
        {
            return new Packet[] { new HalfBytePacket() { HighHalf = 2, LowHalf = _socket } };
        }
    }
}
