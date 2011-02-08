using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainLogic.Visca.Commands;
using System.IO.Ports;
using PlainLogic.Visca.Inquiries;
using PlainLogic.Visca;

namespace PlainLogic.ViscaController.Server
{
    class Program
    {
        static void Main()
        {
            VISCASerialPort port = new VISCASerialPort( VISCASerialPort.GetPortNames()[0], PortSpeeds.Normal );

            port.Open();

            PowerInq inq = new PowerInq();

            inq.DestinationDevice = 4;

            port.SendCommand( inq );

            port.Close();
        }

    }
}
