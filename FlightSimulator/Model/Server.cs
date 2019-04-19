using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    public class Server : BaseNotify, IConnection
    {
        private static Server self = null;
        private TcpClient client;
        private string dataFromSocket;
        private readonly object locker;

        private Server() { locker = new Object(); }

        public static Server Instance
        {
            get
            {
                if (self == null)
                {
                    self = new Server();
                }
                return self;
            }
        }

        public string Data
        {
            get
            {
                return dataFromSocket;
            }
            set
            {
                dataFromSocket = value;
                NotifyPropertyChanged("Data");
            }
        }

        public object Mutex
        {
            get
            {
                return locker;
            }
        }

        public void open(string ip, int port)
        {

        }

        public void close()
        {

        }
    }
}
