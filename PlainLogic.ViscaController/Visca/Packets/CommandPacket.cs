using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Packets
{
    public sealed class CommandPacket : Packet 
    {
        int _sourceDevice = 0;
        public int SourceDevice
        {
            get { return _sourceDevice; }
            //set
            //{
            //    if( value < 0 || value > 7 ) throw new ArgumentOutOfRangeException();

            //    _sourceDevice = value;
            //}
        }

        int _destDevice = 1;
        public int DestinationDevice
        {
            get
            {
                if( Broadcast ) return 0;

                return _destDevice;
            }
            set
            {
                if( Broadcast ) throw new ArgumentOutOfRangeException( "DestinationDevice cannot be used when Broadcasting." );
                if( value < 1 || value > 7 ) throw new ArgumentOutOfRangeException();

                _destDevice = value;
            }
        }

        public bool Broadcast { get; set; }

        protected override byte[] GetDataBytes()
        {
            byte cmdByte = 0x80;

            cmdByte |= (byte)(_sourceDevice << 7);

            if( Broadcast )
            {
                cmdByte |= 0x08;
            }
            else
            {
                cmdByte |= (byte)_destDevice;
            }

            return new byte[] { cmdByte };
        }
    }
}
