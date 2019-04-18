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
        private ICommand _throttle;
        private double throttle;
        private ICommand _rudder;
        private double rudder;
        private ICommand _aileron;
        private double aileron;
        private ICommand _elevator;
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
                String[] commands = { command };
                model.send(commands);
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
                String[] commands = { command };
                model.send(commands);
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
                String[] commands = { command };
                model.send(commands);
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
                aileron = value;
                NotifyPropertyChanged("Elevator");
                String command = new StringBuilder("set /controls/flight/elevator ").Append(elevator.ToString()).ToString();
                String[] commands = { command };
                model.send(commands);
            }
        }

    }
}
