using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;

namespace FlightSimulator.Model
{
    public class Commands : IConnection
    {
        private TcpClient client;
        private IPEndPoint ep;
        private static Commands self = null;

        private Commands() { }

        public static Commands Instance
        {
            get
            {
                if (null == self)
                {
                    self = new Commands();
                }
                return self;
            }
        }

        public void open(string ip, int port)
        {
            ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient(ep);
            client.Connect(ep);
        }

        public void close()
        {
            client.Close();
        }
        
        private void sendControl(String[] cmds)
        {
            if (client != null)
            {
                int size = cmds.Length;
                using (NetworkStream stream = client.GetStream())
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < size; ++i)
                    {
                        writer.Write(cmds[i]);
                        Thread.Sleep(2000);
                    }
                }
            }
        }

        public void send (String[] cmds)
        {
            Task.Run(() => sendControl(cmds));
        }
    }
}
