using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PlainLogic.ViscaController.Server
{

    [ServiceBehavior( InstanceContextMode = InstanceContextMode.Single )]
    class ViscaService : IViscaService
    {
        public void DoTest()
        {
            throw new NotImplementedException();
        }
    }
}
