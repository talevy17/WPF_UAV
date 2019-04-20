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
    /**
     * The commands channel, acts as a client of the simulator to send commands.
     * Singleton, there has to be a single instance of the client.
     * */
    public class Commands : IConnection
    {
        private TcpClient client = null;
        private NetworkStream stream = null;
        private readonly object locker;
        private Dictionary<string, string> paths = new Dictionary<string, string>();
        private static Commands self = null;

        /**
         * private CTOR, as part of the Singleton design pattern.
         * */
        private Commands() {
            SetPathMap();
            locker = new Object();
        }

        /**
         * The Instance static property for the Singleton getter.
         * */
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

        /**
         * Open a new Tcp Client connection to the server.
         * */
        public void Open(string ip, int port)
        {
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse(ip), port);
            client = new TcpClient(ep);
            client.Connect(ep);
            stream = client.GetStream();
            Console.WriteLine("connected");
        }

        /**
         * closes the client and the network stream.
         * */
        public void Close()
        {
            stream.Close();
            client.Close();
        }
        
        /**
         * sends all of the commands to the server, waiting 2 seconds between commands.
         * */
        private void SendControl(String[] cmds)
        {
            if (client != null)
            {
                foreach (String command in cmds)
                {
                    string cmd = command + "\r\n";
                    lock(locker)
                    {
                        // convert the command string to an array of bytes.
                        byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(cmd.ToString());
                        stream.Write(byteArr, 0, byteArr.Length);
                        Console.WriteLine("command: " + cmd + " successfully sent");
                    }
                    Thread.Sleep(2000);
                }
                
            }
        }

        /**
         * sends the manual input commands to the server.
         * */
        public void ManualSend(string cmd, double value)
        {
            if (client != null)
            {
                // get the path of the property that was changed.
                string path = paths[cmd];
                // add the new value to the command.
                path += " ";
                path += value.ToString("N5");
                // mutex lock.
                lock(locker)
                {
                    byte[] byteArr = System.Text.Encoding.ASCII.GetBytes(path.ToString());
                    stream.Write(byteArr, 0, byteArr.Length);
                    Console.WriteLine("command: " + path + " successfully sent");
                }
            }
        }

        /**
         * creates a new nameless task to run as the SendControl.
         * */
        public void Send (String[] cmds)
        {
            Task.Run(() => SendControl(cmds));
        }

        /**
         * Initializes the paths that are binded with the manuel commands.
         * */
        private void SetPathMap() {
            paths.Add("aileron", "set /controls/flight/aileron");
            paths.Add("elevator", "set /controls/flight/elevator");
            paths.Add("rudder", "set /controls/flight/rudder");
            paths.Add("throttle", "set /controls/engines/current-engine/throttle");
        }

        
    }
}