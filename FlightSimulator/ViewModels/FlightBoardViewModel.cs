using FlightSimulator.Model;
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
        private ICommand connect;
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
        public ICommand ConnectCommand
        {
            get
            {
                return connect ?? (connect = new CommandHandler(() => ConnentOnClick()));
            }
        }
        private void ConnentOnClick()
        {
            Server.Instance.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
            Commands.Instance.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
        }
        #endregion
    }
}
