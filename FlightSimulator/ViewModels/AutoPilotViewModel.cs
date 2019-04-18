using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightSimulator.Model;
using System.Windows.Input;

namespace FlightSimulator.ViewModels
{
    class AutoPilotViewModel : BaseNotify
    {
        private ICommand _okCommand;
        private ICommand _clearCommand;
        private string text;
        private Commands model;

        public AutoPilotViewModel(Commands mod) {
            text = "";
            model = mod;
        }

        public string AutoPilotCommands
        {
            set
            {
                text = value;
                NotifyPropertyChanged("AutoPilotCommands");
                NotifyPropertyChanged("Color");
            }

            get
            {
                return text;
            }
        }

        public string Color
        {
            get
            {
                if (text == "")
                {
                    return "White";
                }
                else
                {
                    return "Pink";
                }
            }
        }

        private void parser()
        {
            string[] delimiter = { "\r\n" };
            String[] result = text.Split(delimiter, StringSplitOptions.None);
            text = "";
            NotifyPropertyChanged("Color");
            model.send(result);
        }

        private void clearTextBox()
        {
            AutoPilotCommands = "";
        }

        public ICommand okCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => parser()));
            }
        }

        public ICommand clearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => clearTextBox()));
            }
        }
    }
}
