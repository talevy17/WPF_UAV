using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class ManualViewModel : BaseNotify
    {
        private Commands model;
        private double throttle;
        private double rudder;
        private double aileron;
        private double elevator;

        /*
         * CTOR, takes one argument -> the commands channel.
         * */
        public ManualViewModel(Commands mod)
        {
            model = mod;
            throttle = 0;
            rudder = 0;
            aileron = 0;
            elevator = 0;
        }

        /**
         * The Throttle property
         * */
        public double Throttle
        {
            get
            {
                return throttle;
            }
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
                // send the new throttle value via the commands channel.
                model.ManualSend("throttle", throttle);
            }
        }

        /**
         * The Rudder property.
         **/
        public double Rudder
        {
            get
            {
                return rudder;
            }
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
                // send the new rudder value via the commands channel.
                model.ManualSend("rudder", rudder);
            }
        }

        /**
         * The Aileron property.
         * */
        public double Aileron
        {
            get
            {
                return aileron;
            }
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
                // send the new aileron value via the commands channel.
                model.ManualSend("aileron", aileron);
            }
        }

        /**
         * The Elevator property.
         * */
        public double Elevator
        {
            get
            {
                return elevator;
            }
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
                // send the new elevator value via the commands channel.
                model.ManualSend("elevator", elevator);
            }
        }

    }
}
