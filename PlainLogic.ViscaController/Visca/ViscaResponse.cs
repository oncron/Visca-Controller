using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.ViscaController.Visca.Responses;

namespace PlainLogic.ViscaController.Visca
{
    public enum ResponseTypes
    {
        Acknowledgement,
        CommandComplete,
        InqueryComplete,
        Error
    }

    public abstract class ViscaResponse
    {
        public ResponseTypes ResponseType { get; private set; }

        public byte SourceAddress { get; private set; }
        public byte Socket { get; private set; }

        internal static ViscaResponse ProcessResponse( byte[] packet, ViscaCommand pendingCmd, out bool matchesPending )
        {
            byte src = (byte)( packet[0] >> 4 );
            byte socket = (byte)( packet[1] & 0x0F );
            byte replyType = (byte)( packet[1] >> 4 );

            matchesPending = false;

            if( packet.Last() != 0xFF ) throw new ArgumentException( "Invalid Data:  Does not end with expected 0xFF" );

            ViscaResponse response =  null;

            switch( replyType )
            {
                case 4: //Ack
                    if( pendingCmd == null ) throw new InvalidOperationException();

                    response = new Acknowledgement();
                    matchesPending = true;
                    break;

                case 5: //Completion (commands/inquiries)
                    if( packet.Length == 3 ) //Command Complete
                    {
                        response = new CommandComplete();
                    }
                    else
                    {
                        matchesPending = true;
                        response = (ViscaInquiryResponse)Activator.CreateInstance( pendingCmd.InquiryResponseType );
                    }
                    break;

                case 6: //Error
                    if( packet.Length != 4 ) throw new ArgumentException( "Invalid Data: Error Packet not correct." );
                    response = new CommandError( packet[2] );

                    if( pendingCmd != null ) matchesPending = true;

                    break;

                default: throw new NotSupportedException();
            }

            response.SourceAddress = src;
            response.Socket = socket;

            if( response is ViscaInquiryResponse )
            {
                //process only the meat data of the packet (strip out header and footer bytes)
                byte[] data = new byte[packet.Length - 3];

                Array.Copy( packet, 2, data, 0, packet.Length - 3 );

                ( (ViscaInquiryResponse)response ).ProcessResponse_Internal( data );
            }

            return response;
        }

        internal ViscaResponse( ResponseTypes type )
        {
            ResponseType = type;
        }
    }

    public abstract class ViscaInquiryResponse : ViscaResponse
    {
        protected ViscaInquiryResponse() : base( ResponseTypes.InqueryComplete ) { }

        internal void ProcessResponse_Internal( byte[] data )
        {
            OnProcessResponse( data );
        }

        protected abstract void OnProcessResponse( byte[] data );
    }
}
