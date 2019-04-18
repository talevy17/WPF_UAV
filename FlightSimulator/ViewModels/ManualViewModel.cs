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

        public ManualViewModel(Commands mod)
        {
            model = mod;
            throttle = 0;
            rudder = 0;
            aileron = 0;
            elevator = 0;
        }

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
                String command = new StringBuilder("set /controls/engines/current-engine/throttle ").Append(throttle.ToString()).ToString();
                model.manualSend(command);
            }
        }

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
                String command = new StringBuilder("set /controls/flight/rudder ").Append(rudder.ToString()).ToString();
                model.manualSend(command);
            }
        }

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
                String command = new StringBuilder("set /controls/flight/aileron ").Append(aileron.ToString()).ToString();
                model.manualSend(command);
            }
        }
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
                String command = new StringBuilder("set /controls/flight/elevator ").Append(elevator.ToString()).ToString();
                model.manualSend(command);
            }
        }

    }
}
