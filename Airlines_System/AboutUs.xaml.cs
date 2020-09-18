using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MidTerm_AjayDesai
{
    /// <summary>
    /// Interaction logic for AboutUs.xaml
    /// </summary>
    public partial class AboutUs : Window
    {
        public AboutUs()
        {
            InitializeComponent();
        }
        private void MenuQuit_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuQuit.Foreground = Brushes.Black;
        }

        private void MenuQuit_LostFocus(object sender, RoutedEventArgs e)
        {
            MenuQuit.Foreground = Brushes.White;
        }

        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void ViewCustomers_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow cw = new CustomersWindow();
            cw.Show();
        }

        private void ViewFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightsWindow flw = new FlightsWindow();
            flw.Show();
        }

        private void ViewAirlines_Click(object sender, RoutedEventArgs e)
        {
            AirlinesWindow airw = new AirlinesWindow();
            airw.Show();
        }

        private void ViewPassengers_Click(object sender, RoutedEventArgs e)
        {
            PassengersWindow pw = new PassengersWindow();
            pw.Show();
        }
        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
