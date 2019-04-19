using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using FlightSimulator.Model.Interface;
using FlightSimulator.ViewModels;

namespace FlightSimulator.Model
{
    class Info : BaseNotify
    {
        private FlightData data = null;
        private Server server;


        public Info()
        {
            data = FlightData.Instance;
            server = Server.Instance;
            server.PropertyChanged += dataRecieved;
        }

        public double Lon
        {
            get
            {
                lock(server.Mutex)
                {
                    return data["/position/longitude-deg"];
                }
            }
        }

        public double Lat
        {
            get
            {
                lock (server.Mutex)
                {
                    return data["/position/latitude-deg"];
                }
            }
        }

        private void dataRecieved(object sender, PropertyChangedEventArgs e)
        {
            lock(server.Mutex)
            {
                String[] tokens = parse(server.Data);
                data.setDataValues(tokens);
                NotifyPropertyChanged("Data");
            }
        }

        private String[] parse(string line)
        {
            return line.Split(',');
        }
    }
}
