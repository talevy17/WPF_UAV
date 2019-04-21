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
    /**
     * The Info channel, binded to the data recieved from the server, parses the data and notifes the changes.
     * */
    class Info : BaseNotify
    {
        private FlightData data = null;
        private Server server;

        /**
         * CTOR, gets the instances and the Flight Data, adds itself as a listener of the server.
         * */
        public Info()
        {
            data = FlightData.Instance;
            server = Server.Instance;
            server.PropertyChanged += DataRecieved;
        }

        /**
         * The Longitude property.
         * */
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

        /**
         * The Latitude property.
         * */
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

        /**
         * DataRecieved invoked upon a change in Server, sets the data in the indexer and notifes the viewmodel 
         * about the change.
         * */
        private void DataRecieved(object sender, PropertyChangedEventArgs e)
        {
            lock (server.Mutex)
            {
                String[] tokens = server.Data.Split(',');
                data.SetDataValues(tokens);
            }
            NotifyPropertyChanged("Data");
        }
    }
}
