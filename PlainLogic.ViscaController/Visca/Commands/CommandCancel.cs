using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Visca.Packets;

namespace PlainLogic.ViscaController.Visca.Commands
{
    class CommandCancel : ViscaCommand
    {
        public CommandCancel() : base( CommandTypes.Custom, CommandCategories.Custom, false ) { }

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

        protected override void OnGenerateCommandMessage( ICommandMessageGenerator gen )
        {
            gen.CreateMessage( new HalfBytePacket( 2, _socket ) { HighHalf = 2, LowHalf = _socket } );
        }
    }
}
