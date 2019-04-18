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
                model.manualSend("throttle", throttle);
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
                model.manualSend("rudder", rudder);
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
                model.manualSend("aileron", aileron);
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
                model.manualSend("elevator", elevator);
            }
        }

    }
}
