using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Responses
{
    public class PowerInq_Response : ViscaInquiryResponse 
    {
        public bool PoweredOn { get; private set; }

        protected override void OnProcessResponse( byte[] data )
        {
            if( data.Length != 1 ) throw new ArgumentOutOfRangeException();

            switch( data[0] )
            {
                case 2:
                    PoweredOn = true;
                    break;

                case 3:
                    PoweredOn = false;
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
