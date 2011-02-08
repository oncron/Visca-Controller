using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Visca.Packets;

namespace Server.Visca.Commands
{
    class CAM_Zoom : CommandBase
    {
        public enum Commands : byte
        {
            Stop = 0x00,
            Tele_Standard = 0x02,
            Wide_Standard = 0x03,
            Tele_Variable = 0x20,
            Wide_Variable = 0x30,
            Direct  
        }

        public CAM_Zoom()
            : base( false )
        {

        }

        ushort _zoomPos = 0;
        public ushort DirectZoomPosition
        {
            get
            {
                if( Command != Commands.Direct ) return 0;

                return _zoomPos;
            }
            set
            {
                _zoomPos = value;
            }
        }

        public Commands Command { get; set; }

        byte _varZoom = 0;
        public byte VariableZoom
        {
            get
            {
                if( Command != Commands.Tele_Variable && Command != Commands.Wide_Variable ) return 0;

                return _varZoom;
            }
            set 
            {
                if( value < 0 || value > 7 ) throw new ArgumentOutOfRangeException();

                _varZoom = value;
            }
        }

        protected override Packets.Packet[] GetDataPackets()
        {
            Packet startPacket = new CustomPacket( 0x01, 0x04 );
            HalfBytePacket cmdPacket = new HalfBytePacket() { LowHalf = 0x07 };
            Packet dataPacket;

            if( Command == Commands.Direct )
            {
                cmdPacket.HighHalf = 0x04;

                dataPacket = new LowHalfUShortPacket() { Value = DirectZoomPosition };
            }
            else
            {
                dataPacket = new CustomPacket( (byte)(Command + VariableZoom) );
            }

            return new Packet[] { startPacket, cmdPacket, dataPacket };
        }
    }
}
