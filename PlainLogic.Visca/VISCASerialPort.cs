using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using PlainLogic.Visca;
using System.Threading;

namespace PlainLogic.Visca
{
    public enum PortSpeeds
    {
        Normal,
        Fast
    }

    public class VISCASerialPort : IDisposable
    {
        SerialPort _innerPort;

        Thread _readThread = null;

        public static string[] GetPortNames() { return SerialPort.GetPortNames(); }

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
            if( _readThread != null ) throw new InvalidOperationException();

            _innerPort.Open();

            _readThread = new Thread( ReadThread );
            _readThread.Name = "VISCASerialPort Reader";
            _readThread.Start();
        }

        public void Close()
        {
            if( _readThread == null ) throw new InvalidOperationException();

            Thread thread = _readThread;
            _readThread = null;
            thread.Join();

            _innerPort.Close();

            _responseQueue.Clear();
        }

        void IDisposable.Dispose()
        {
            _innerPort.Dispose();
        }

        Queue<ViscaResponse> _responseQueue = new Queue<ViscaResponse>();

        public int ResponsePending
        {
            get
            {
                lock( _responseQueue )
                {
                    return _responseQueue.Count;
                }
            }
        }

        public bool CommandInProcess { get { return CurrentCommand != null; } }

        public ViscaResponse ReadResponse()
        {
            lock( _responseQueue )
            {
                if( _responseQueue.Count == 0 ) throw new InvalidOperationException();

                return _responseQueue.Dequeue();
            }
        }

        public ViscaCommand CurrentCommand { get; private set; }

        public void SendCommand( ViscaCommand cmd )
        {
            if( CommandInProcess ) throw new InvalidOperationException();

            CurrentCommand = cmd;

            var msgs = cmd.GetCommandMsgs();

            foreach( OutMessage msg in msgs )
            {
                _innerPort.Write( msg.Data, 0, msg.Data.Length );

                while( CurrentCommand != null ) Thread.Sleep( 10 );
            }
        }

        void ReadThread()
        {
            List<byte> buffer = new List<byte>();

            while( _readThread != null )
            {
                while( _innerPort.BytesToRead > 0 )
                {
                    byte newByte = (byte)_innerPort.ReadByte();

                    if( newByte == 0xFF )
                    {
                        bool matchesCurrent;

                        //end byte.  queue up message
                        var response = ViscaResponse.ProcessResponse( buffer.ToArray(), CurrentCommand, out matchesCurrent );

                        if( matchesCurrent ) CurrentCommand = null;

                        lock( _responseQueue ) _responseQueue.Enqueue( response );

                        buffer.Clear();
                    }
                    else
                    {
                        buffer.Add( newByte );
                    }
                }

                Thread.Sleep( 10 );
            }
        }
    }
}
