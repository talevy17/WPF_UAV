using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlightSimulator.Model.Interface;
using System.Collections.Generic;

namespace FlightSimulator.Model
{
    public class Commands : IConnection
    {
        private TcpClient client = null;
        private NetworkStream stream = null;
        private readonly object locker;
        private Dictionary<string, string> paths = new Dictionary<string, string>();
        private static Commands self = null;

        private Commands() {
            setPathMap();
            locker = new Object();
        }

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
            Console.WriteLine("connected");
        }

        public void close()
        {
            stream.Close();
            client.Close();
        }
        
        private void sendControl(String[] cmds)
        {
            if (client != null)
            {
                foreach (String command in cmds)
                {
                    string cmd = command + "\r\n";
                    lock(locker)
                    {
                        byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(cmd.ToString());
                        stream.Write(byteArr, 0, byteArr.Length);
                        Console.WriteLine("command: " + cmd + " successfully sent");
                    }
                    Thread.Sleep(2000);
                }
                
            }
        }

        public void manualSend(string cmd, double value)
        {
            if (client != null)
            {
                string path = paths[cmd];
                path += " ";
                path += value.ToString("N5");
                lock(locker)
                {
                    byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(path.ToString());
                    stream.Write(byteArr, 0, byteArr.Length);
                    Console.WriteLine("command: " + path + " successfully sent");
                }
            }
        }

        public void send (String[] cmds)
        {
            Task.Run(() => sendControl(cmds));
        }

        private void setPathMap() {
            paths.Add("aileron", "set /controls/flight/aileron");
            paths.Add("elevator", "set /controls/flight/elevator");
            paths.Add("rudder", "set /controls/flight/rudder");
            paths.Add("throttle", "set /controls/engines/current-engine/throttle");
        }

        public void test()
        {
            Console.WriteLine("hi");
        }
    }
}
