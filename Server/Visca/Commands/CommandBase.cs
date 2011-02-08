using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    abstract class CommandBase
    {
        protected CommandBase( bool broadcast )
        {
            Broadcast = broadcast;
        }

        CommandPacket _command = new CommandPacket();

        public bool Broadcast
        {
            get { return _command.Broadcast; }
            private set { _command.Broadcast = value; }
        }

        public int DestinationDevice
        {
            get { return _command.DestinationDevice; }
            set { _command.DestinationDevice = value; }
        }

        public byte[] GetDataBytes()
        {
            List<byte> data = new List<byte>();

            foreach( Packet packet in GetPackets() ) data.AddRange( packet.GetDataBytes() );

            return data.ToArray();
        }

        Packet[] GetPackets()
        {
            List<Packet> _packets = new List<Packet>();

            _packets.Add( _command );

            var dataPackets = GetDataPackets();

            for( int i = 0; i < dataPackets.Length; i++ )
            {
                _packets.Add( dataPackets[i] );

                if( dataPackets[i].GetType() == typeof( TerminatorPacket ) )
                {
                    if( i < dataPackets.Length - 1 ) _packets.Add( _command );
                }
            }

            if( _packets.Last().GetType() != typeof( TerminatorPacket ) ) _packets.Add( new TerminatorPacket() );

            return _packets.ToArray();
        }

        protected abstract Packet[] GetDataPackets();

        
    }
}
