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
    /**
     * The FlightBoard's viewmodel.
     * */
    public class FlightBoardViewModel : BaseNotify
    {
        private Info model;
        private double lon;
        private double lat;
        private ICommand settingsCommand;
        private ICommand connect;

        /**
         * CTOR
         * */
        public FlightBoardViewModel() {
            // init the info channel.
            model = new Info();
            // add the vm as an observer of the info channel.
            model.PropertyChanged += DataRecieved;
            lon = 0;
            lat = 0;
        }

        /**
         * Longitude property.
         * */
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

        /**
         * Latitude property.
         * */
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

        /**
         * to be registered as the listener upon changes in the info channel.
         * */
        private void DataRecieved(object sender, PropertyChangedEventArgs e)
        {
            Lon = model.Lon;
            Lat = model.Lat;
        }
        #region 

        /**
         * Binded to the Settings button in the view.
         * */
        public ICommand SettingsCommand
        {
            get
            {
                return settingsCommand ?? (settingsCommand = new CommandHandler(() => OnClick()));
            }
        }

        /**
         * Opens the settings window.
         * */
        private void OnClick()
        {
            var settingWin = new Settings();
            settingWin.Show();
        }

        /**
         * Binded with the Connect button in the view.
         * */
        public ICommand ConnectCommand
        {
            get
            {
                return connect ?? (connect = new CommandHandler(() => ConnentOnClick()));
            }
        }

        /**
         * Starts the connections of the server and the client.
         * */
        private void ConnentOnClick()
        {
            Server.Instance.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightInfoPort);
            Commands.Instance.Open(ApplicationSettingsModel.Instance.FlightServerIP, ApplicationSettingsModel.Instance.FlightCommandPort);
        }
        #endregion
    }
}
