using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class CAM_Title_Set : CommandBase 
    {
        public CAM_Title_Set() : base( false ) { }

        public byte VPosition { get; set; }
        public byte HPosition { get; set; }
        public byte Color { get; set; }
        public byte Blink { get; set; }

        string _title = null;
        public string Title
        {
            get { return _title; }
            set
            {
                if( value.Length > 20 ) throw new ArgumentOutOfRangeException();

                _title = value;
            }
        }

        protected override Packet[] GetDataPackets()
        {
            if( String.IsNullOrEmpty( Title ) )
            {
                //Title Clear
                return new Packet[] { new CustomPacket( 0x01, 0x04, 0x74, 0x00 ) };
            }
            else
            {
                string str1 = Title.Length > 10 ? Title.Substring( 0, 10 ) : Title;
                string str2 = Title.Length > 10 ? Title.Substring( 10 ) : "";

                List<Packet> packets = new List<Packet>();

                CustomPacket startPacket = new CustomPacket( 0x01, 0x04, 0x73 );

                //Title Set1
                packets.Add( startPacket );
                packets.Add( new CustomPacket( 0x00, VPosition, HPosition, Color, Blink ) );
                packets.Add( new FillerPacket( 6, 0x00 ) );
                packets.Add( new TerminatorPacket() );

                //Title Set2
                packets.Add( startPacket );
                packets.Add( new CustomPacket( 0x01 ) );
                packets.Add( new StringPacket() { Value = str1 } );
                packets.Add( new FillerPacket( 10 - str1.Length, 0x00 ) );
                packets.Add( new TerminatorPacket() );

                //Title Set3
                if( str2.Length > 0 )
                {
                    packets.Add( startPacket );
                    packets.Add( new CustomPacket( 0x02 ) );
                    packets.Add( new StringPacket() { Value = str2 } );
                    packets.Add( new FillerPacket( 10 - str2.Length, 0x00 ) );
                    packets.Add( new TerminatorPacket() );
                }

                return packets.ToArray();
            }

        }
    }
}
