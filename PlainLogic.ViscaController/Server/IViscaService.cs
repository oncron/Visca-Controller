using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace PlainLogic.ViscaController.Server
{
    [ServiceContract]
    interface IViscaService
    {
        [OperationContract]
        void DoTest();
    }
}
