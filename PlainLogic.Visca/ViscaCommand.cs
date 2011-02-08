using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Packets;

namespace PlainLogic.Visca
{
    public interface ICommandMessageGenerator
    {
        void CreateMessage( params Packet[] packets );
    }

    public abstract class ViscaCommand
    {
        public enum CommandTypes : byte
        {
            Command = 0x01,
            Inquiry = 0x09,
            Custom = 0xFF
        }

        public enum CommandCategories : byte
        {
            Interface = 0x00,
            Camera = 0x04,
            PanTilter = 0x06,
            Custom = 0xFF
        }

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

        protected ViscaCommand( CommandTypes cmdType, CommandCategories category, bool broadcast )
        {
            if( broadcast && cmdType == CommandTypes.Inquiry ) throw new ArgumentException( "Inquiries cannot be broadcast." );
            if( cmdType == CommandTypes.Custom && category != CommandCategories.Custom ) throw new ArgumentException( "category must be Custom when cmdType is Custom." );

            Broadcast = broadcast;
            Type = cmdType;
            Category = category;
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

        public CommandTypes Type { get; private set; }
        public CommandCategories Category { get; private set; }

        Type _responseType = null;
        public Type InquiryResponseType
        {
            get { return _responseType; }
            protected set
            {
                if( Type != CommandTypes.Inquiry ) throw new InvalidOperationException();
                if( !value.IsSubclassOf( typeof( ViscaInquiryResponse ) ) ) throw new ArgumentException( "Type must implement ViscaInquiryResponse" );

                _responseType = value;
            }
        }

        /// <summary>
        /// Generates the byte version of the command
        /// </summary>
        /// <returns>Returns the command in a multi-dimensional byte array.  The first index is a</returns>
        internal OutMessage[] GetCommandMsgs()
        {
            if( Type == CommandTypes.Inquiry && _responseType == null ) throw new InvalidOperationException( "InquiryResponseType was not set for Inquiry Command." );

            CommandMsgGenerator gen = new CommandMsgGenerator();

            OnGenerateCommandMessage( gen );

            if( gen.PacketSets.Count == 0 ) throw new Exception( "Command did not generate any messages." );

            List<OutMessage> msgs = new List<OutMessage>();

            foreach( Packet[] packetSet in gen.PacketSets )
            {
                List<Packet> msgPackets = new List<Packet>();

                msgPackets.Add( _command );

                if( Type != CommandTypes.Custom ) msgPackets.Add( new LiteralBytesPacket( (byte)Type ) );
                if( Category != CommandCategories.Custom ) msgPackets.Add( new LiteralBytesPacket( (byte)Category ) );

                msgPackets.AddRange( packetSet );

                msgPackets.Add( new TerminatorPacket() );

                msgs.Add( new OutMessage( msgPackets.ToArray() ) );
            }

            return msgs.ToArray();
        }

        protected abstract void OnGenerateCommandMessage( ICommandMessageGenerator gen );

        
    }
}
