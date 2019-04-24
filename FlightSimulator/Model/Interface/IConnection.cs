using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.Model.Interface
{
    interface IConnection
    {
        // Open a connection with the given ip and port/
        void Open(string ip, int port);
        // Close the connection.
        void Close();
        // Was the connection opened
        bool IsConnected();
    }
}
