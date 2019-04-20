﻿using System;
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

        /**
         * CTOR, accepts the commands channel as a model.
         * */
        public AutoPilotViewModel(Commands mod) {
            text = "";
            model = mod;
        }

        /**
         * The textbox commands property.
         * */
        public string AutoPilotCommands
        {
            set
            {
                // take the value from the textbox and notify the changes in color and commands.
                text = value;
                NotifyPropertyChanged("AutoPilotCommands");
                NotifyPropertyChanged("Color");
            }

            get
            {
                return text;
            }
        }

        /**
         * The Color property, white when there aren't new commands to send, pink when there are.
         * */
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

        /**
         * Lexing the input, emptying the text and parses the commands via the commands channel.
         * */
        private void Parser()
        {
            string[] delimiter = { "\r\n" };
            String[] result = text.Split(delimiter, StringSplitOptions.None);
            text = "";
            NotifyPropertyChanged("Color");
            model.Send(result);
        }

        /**
         * clears the textbox.
         * */
        private void ClearTextBox()
        {
            AutoPilotCommands = "";
        }

        /**
         * binded with the OK button, activates the Parser on click.
         * */
        public ICommand okCommand
        {
            get
            {
                return _okCommand ?? (_okCommand = new CommandHandler(() => Parser()));
            }
        }

        /**
         * binded with the CLEAR button, activates the ClearTextBox on click.
         * */
        public ICommand clearCommand
        {
            get
            {
                return _clearCommand ?? (_clearCommand = new CommandHandler(() => ClearTextBox()));
            }
        }
    }
}
