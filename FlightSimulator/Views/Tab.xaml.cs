﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlightSimulator.Model;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Tab.xaml
    /// </summary>
    public partial class Tab : UserControl
    {
        public Tab()
        {
            InitializeComponent();
            Commands.Instance.open("127.0.0.1", 5402);
        }
    }
}
