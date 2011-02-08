using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    interface ICommandMessageGenerator
    {
        void CreateMessage( params Packet[] packets );
    }

    abstract class CommandBase
    {
        class CommandMsgGenerator : ICommandMessageGenerator
        {
            public List<Packet[]> PacketSets = new List<Packet[]>();

            public void CreateMessage( params Packet[] msgPackets )
            {
                if( msgPackets.OfType<CommandPacket>().Any() ) throw new ArgumentException( "Packet[] cannot contain a CommandPacket.  A CommandPacket is added automatically later on in the process.", "msgPackets" );
                if( msgPackets.OfType<TerminatorPacket>().Any() ) throw new ArgumentException( "Packet[] cannot contain a TerminatorPacket.  A TerminatorPacket is added automatically later on in the process.", "msgPackets" );
                
                PacketSets.Add( msgPackets );
            }
        }

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

        public Message[] GetCommandMessages()
        {
            CommandMsgGenerator gen = new CommandMsgGenerator();

            OnGenerateCommandMessage( gen );

            if( gen.PacketSets.Count == 0 ) throw new Exception( "Command did not generate any messages." );

            List<Message> msgs = new List<Message>();

            foreach( Packet[] packetSet in gen.PacketSets )
            {
                List<Packet> msgPackets = new List<Packet>();

                msgPackets.Add( _command );

                msgPackets.AddRange( packetSet );

                msgPackets.Add( new TerminatorPacket() );

                msgs.Add( new Message( msgPackets.ToArray() ) );
            }

            return msgs.ToArray();
        }

        //Packet[] GetPackets()
        //{
        //    List<Packet> _packets = new List<Packet>();

        //    _packets.Add( _command );

        //    var dataPackets = GetDataPackets();

        //    for( int i = 0; i < dataPackets.Length; i++ )
        //    {
        //        _packets.Add( dataPackets[i] );

        //        if( dataPackets[i].GetType() == typeof( TerminatorPacket ) )
        //        {
        //            if( i < dataPackets.Length - 1 ) _packets.Add( _command );
        //        }
        //    }

        //    if( _packets.Last().GetType() != typeof( TerminatorPacket ) ) _packets.Add( new TerminatorPacket() );

        //    return _packets.ToArray();
        //}

        protected abstract void OnGenerateCommandMessage( ICommandMessageGenerator gen );

        
    }
}
