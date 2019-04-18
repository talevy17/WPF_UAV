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
        private TcpClient client = null;
        private NetworkStream stream = null;
        private BinaryWriter writer = null;
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
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient(ep);
            client.Connect(ep);
            stream = client.GetStream();
            writer = new BinaryWriter(stream);
            Console.WriteLine("connected");
        }

        public void close()
        {
            writer.Close();
            stream.Close();
            client.Close();
        }
        
        private void sendControl(String[] cmds)
        {
            if (client != null)
            {
                int size = cmds.Length;
                for (int i = 0; i < size; ++i)
                {
                    writer.Write(cmds[i]);
                    Console.WriteLine("command: "+cmds[i]+" successfully sent");
                    Thread.Sleep(2000);
                }
                
            }
        }

        public void manualSend(String cmd)
        {
            if (client != null)
            {
                writer.Write(cmd);
            }
        }

        public void send (String[] cmds)
        {
            Task.Run(() => sendControl(cmds));
        }
    }
}
