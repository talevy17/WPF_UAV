﻿using FlightSimulator.Model;
using FlightSimulator.Model.Interface;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace FlightSimulator.ViewModels
{
    public class FlightBoardViewModel : BaseNotify
    {
        private Info model;
        private double lon;
        private double lat;
        public FlightBoardViewModel() {
            model = new Info();
            model.PropertyChanged += DataRecieved;
            lon = 0;
            lat = 0;
        }
        public double Lon
        {
            get
            {
                return lon;
            }
            set
            {
                lon = value;
                NotifyPropertyChanged("Lon");
            }
        }

        public double Lat
        {
            get
            {
                return lat;
            }
            set
            {
                lat = value;
                NotifyPropertyChanged("Lat");
            }
        }

        private void DataRecieved(object sender, PropertyChangedEventArgs e)
        {
            Lon = model.Lon;
            Lat = model.Lat;
        }
        #region 

        private ICommand settingsCommand;
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnClick()));
            }
        }
        private void OnClick()
        {
            var settingWin = new Settings();
            settingWin.Show();
        }
        #endregion

        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                return connectCommand ?? (connectCommand = new CommandHandler(()=>OnClickConnect()));
            }
            
        }
        private void OnClickConnect()
        {
            Server server = Server.Instance;
            server.Open(ApplicationSettingsModel.Instance.FlightServerIP,
                ApplicationSettingsModel.Instance.FlightInfoPort);
            Commands commands = Commands.Instance;
            commands.Open(ApplicationSettingsModel.Instance.FlightServerIP,
                ApplicationSettingsModel.Instance.FlightCommandPort);
        }
    }
}
