using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlainLogic.Visca.Responses
{
    public sealed class CommandComplete : ViscaResponse
    {
        public CommandComplete() : base( ResponseTypes.CommandComplete ) { }
    }
}
