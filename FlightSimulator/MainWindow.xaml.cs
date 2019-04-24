using FlightSimulator.Views;
using FlightSimulator.Model;
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

namespace FlightSimulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Closed += MainWindowClosed;
            InitializeComponent();
        }
        /**
         * safe exit
         **/
        private void MainWindowClosed(object sender, EventArgs e)
        {
            if (Server.Instance.IsConnected()) {Server.Instance.Close();}
            if (Commands.Instance.IsConnected()) {Commands.Instance.Close();}
        }
    }
}
