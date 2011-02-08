using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Server.Visca
{
    public enum PortSpeeds
    {
        Normal,
        Fast
    }

    class VISCASerialPort : IDisposable
    {
        SerialPort _innerPort;

        PortSpeeds _speed;
        public PortSpeeds Speed
        {
            get { return _speed; }
            set
            {
                _speed = value;

                switch( value )
                {
                    case PortSpeeds.Normal:
                        _innerPort.BaudRate = 9600;
                        break;
                    case PortSpeeds.Fast:
                        _innerPort.BaudRate = 38400;
                        break;
                    default: throw new NotImplementedException();
                }
            }
        }

        public VISCASerialPort( string serialPortName, PortSpeeds speed )
        {
            _innerPort = new SerialPort( serialPortName );
            _innerPort.DataBits = 8;
            _innerPort.StopBits = StopBits.One;
            _innerPort.Parity = Parity.None;
            _innerPort.RtsEnable = false;
        }

        public void Open()
        {
            _innerPort.Open();
        }

        public void Close()
        {
            _innerPort.Close();
        }

        public void Dispose()
        {
            _innerPort.Dispose();
        }

        public int MessagesPending { get; private set; }

        public bool MessageInProcess { get; private set; }

        public Message ReadMessage()
        {
        }

        public void WriteMessage( Message msg )
        {
            if( MessageInProcess ) throw new InvalidOperationException();
        }
    }
}
