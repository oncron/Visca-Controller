using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.ViscaController.Visca.Responses
{
    public sealed class CommandError : ViscaResponse 
    {
        public enum ErrorCodes : byte
        {
            MessageLengthError = 0x01,
            SyntaxError = 0x02,
            CommandBufferFull = 0x03,
            CommandCancelled = 0x04,
            NoSocket_ToBeCancelled = 0x05,
            CommandNotExecutable = 0x41
        }

        public CommandError( byte data )
            : base( ResponseTypes.Error )
        {
            ErrorCode = (ErrorCodes)data;
        }

        public ErrorCodes ErrorCode { get; private set; }

    }
}
