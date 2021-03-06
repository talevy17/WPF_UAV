﻿using FlightSimulator.Model;
using FlightSimulator.ViewModels.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        private SettingsWindowViewModel settingsWindowViewModel;
        public Settings()
        {
            InitializeComponent();
            settingsWindowViewModel = new SettingsWindowViewModel(ApplicationSettingsModel.Instance, this);
            DataContext = settingsWindowViewModel;
        }
    }
}
