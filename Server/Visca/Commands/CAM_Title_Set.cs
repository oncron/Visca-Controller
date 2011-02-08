using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class CAM_Title_Set : CommandBase
    {
        public enum Colors : byte
        {
            White = 0,
            Yellow = 1,
            Violet = 2,
            Red = 3,
            Cyan = 4,
            Green  = 5,
            Blue = 6
        }

        public CAM_Title_Set() : base( false ) { }

        public byte VPosition { get; set; }
        public byte HPosition { get; set; }
        public Colors Color { get; set; }

        public bool Blink { get; set; }

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

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            if( String.IsNullOrEmpty( Title ) )
            {
                //Title Clear
                gen.CreateMessage( new LiteralBytesPacket( 0x01, 0x04, 0x74, 0x00 ) );
            }
            else
            {
                string str1 = Title.Length > 10 ? Title.Substring( 0, 10 ) : Title;
                string str2 = Title.Length > 10 ? Title.Substring( 10 ) : "";

                LiteralBytesPacket startPacket = new LiteralBytesPacket( 0x01, 0x04, 0x73 );

                //Title Set1
                gen.CreateMessage(
                    startPacket,
                    new LiteralBytesPacket( 0x00, VPosition, HPosition, (byte)Color, (byte)( Blink ? 1 : 0 ) ),
                    new FillerPacket( 6, 0x00 )
                    );

                //Title Set2
                gen.CreateMessage(
                    startPacket,
                    new LiteralBytesPacket( 0x01 ),
                    new StringPacket() { Value = str1 },
                    new FillerPacket( 10 - str1.Length, 0x00 )
                    );

                //Title Set3
                if( str2.Length > 0 )
                {
                    gen.CreateMessage(
                        startPacket,
                        new LiteralBytesPacket( 0x02 ),
                        new StringPacket() { Value = str2 },
                        new FillerPacket( 10 - str2.Length, 0x00 )
                        );
                }
            }
        }
    }
}
