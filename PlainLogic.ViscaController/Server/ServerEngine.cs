using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace PlainLogic.ViscaController.Server
{
    class ServerEngine
    {
        ServiceHost _serviceHost = null;
        ViscaService _service = null;

        public void Start( bool debugMode )
        {
            if( Running ) throw new InvalidOperationException();

            _service = new ViscaService();

            _serviceHost = new ServiceHost( _service, new Uri( "http://localhost:8001/plainlogic/viscacontroller" ) );

            if( debugMode )
            {
                //DEBUG CODE
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior() { HttpGetEnabled = true };
                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;

                _serviceHost.Description.Behaviors.Add( smb );
                _serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
                _serviceHost.AddServiceEndpoint( typeof( IMetadataExchange ), MetadataExchangeBindings.CreateMexHttpBinding(), "mex" );
                //
            }

            _serviceHost.AddServiceEndpoint( typeof( IViscaService ), new WSHttpBinding(), "" );

            _serviceHost.Open();

            Running = true;
        }

        public bool Running { get; private set; }

        public void Stop()
        {
            if( !Running ) throw new InvalidOperationException();

            _serviceHost.Close( TimeSpan.FromSeconds( 3 ) );
            _serviceHost = null;
            _service = null;

            Running = false;
        }
    }
}
