using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;
using System.Net;
using System.Net.Sockets;

namespace FlightSimulator.Model
{
    /**
     * The TCP Server modul, a Singleton.
     * */
    public class Server : BaseNotify, IConnection
    {
        private static Server self = null;
        private TcpListener listener = null;
        private string dataFromSocket;
        private volatile bool stop;
        private readonly object locker;

        /**
         * private CTOR.
         * */
        private Server() {
            locker = new Object();
            stop = false;
        }

        /**
         * The Instance property getter, Singleton.
         * */
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


        /**
         * The Data property.
         * allows getting the information, and updating the observers about the changes.
         * */
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

        /**
         * a Mutex property to lock the threads from accessing the same fields at the same time.
         * */
        public object Mutex
        {
            get
            {
                return locker;
            }
        }

        /**
         * Opens a server to recieve data from the client.
         * */
        public void Open(string ip, int port)
        {
            try
            {
                IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
                listener = new TcpListener(ep);
                listener.Start();
                Console.WriteLine("Waiting for incoming connections...");
                TcpClient client = listener.AcceptTcpClient();
                Console.WriteLine("Accepted Client !");
                NetworkStream stream = client.GetStream();
                Thread t = new Thread(() => {
                    while (!stop)
                    {
                        if (stream.DataAvailable)
                        {
                            byte[] buffer = new byte[1024];
                            int read = stream.Read(buffer, 0, buffer.Length);
                            string parsedData = Encoding.UTF8.GetString(buffer, 0, read);
                            Data = parsedData;
                        }
                    }
                    stream.Close();
                    client.Close();
                });
                t.Start();
            } catch (SocketException e)
            {
                Console.WriteLine(e.Message);
            } finally
            {
                listener.Stop();
            }

        }

        /*
         * Stops the thread's loop to end the connection.
         * */
        public void Close()
        {
            stop = true;
        }

        /**
         * Is the connection open
         * */
        public bool IsConnected()
        {
            return this.listener != null;
        }
    }
}
