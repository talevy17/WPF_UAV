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
        private readonly static object locker = new object();
        private Dictionary<string, string> paths = new Dictionary<string, string>();
        private static Commands self = null;

        /**
         * private CTOR, as part of the Singleton design pattern.
         * */
        private Commands() {
            SetPathMap();
        }

        /**
         * The Instance static property for the Singleton getter.
         * */
        public static Commands Instance
        {
            get
            {
                lock (locker)
                {
                    if (null == self)
                    {
                        self = new Commands();
                    }
                    return self;
                }
            }
        }

        /**
         * Open a new Tcp Client connection to the server.
         * */
        public void Open(string ip, int port)
        {
            client = new TcpClient(ip, port);
            stream = client.GetStream();
            stream.Flush();
            Console.WriteLine("connected, " + ip + " " + port.ToString());
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
         * sends the manual input commands to the server.
         * */
        public void ManualSend(string cmd, double value)
        {
            if (client != null)
            {
                // get the path of the property that was changed.
                string path = paths[cmd] + value.ToString("N5") + "\r\n";
                Sender(path);
            }
        }

        /**
         * Sends the string to the server.
         * */
        private void Sender(string toSend)
        {
            // mutex lock.
            lock (locker)
            {
                // convert the command string to an array of bytes.
                byte[] buffer = System.Text.Encoding.ASCII.GetBytes(toSend.ToString());
                stream.Write(buffer, 0, buffer.Length);
                Console.WriteLine("command: " + toSend);
                stream.Flush();
            }
        }

        /**
         * Sends all commands to the server, waiting two seconds between commands.
         * */
        public void Send (List<string> cmds)
        {
            if (null == client) { return; }
            Thread thread = new Thread(() =>
            {
                foreach (string command in cmds)
                {
                    string cmd = command + "\r\n";
                    Sender(cmd);
                    Thread.Sleep(2000);
                }
            });
            thread.Start();
        }

        /**
         * Initializes the paths that are binded with the manuel commands.
         * */
        private void SetPathMap() {
            paths.Add("aileron", "set /controls/flight/aileron ");
            paths.Add("elevator", "set /controls/flight/elevator ");
            paths.Add("rudder", "set /controls/flight/rudder ");
            paths.Add("throttle", "set /controls/engines/current-engine/throttle ");
        }

        /**
         * Is the connection open
         * */
        public bool IsConnected()
        {
            return this.client != null;
        }
    }
}