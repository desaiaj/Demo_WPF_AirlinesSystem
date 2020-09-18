using MidTerm_AjayDesai.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MidTerm_AjayDesai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //MessageBox.Show(Login.isSuperUser);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MessageBox.Show("You are logged out successfully");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult r = MessageBox.Show("Are you sure? do you want to exit?", "Exit system", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (r == MessageBoxResult.Yes)
            {
                Login m = new Login();
                m.Show();
            }
            else
                e.Cancel = true;
        }
        private void btnViewCust_Click(object sender, RoutedEventArgs e)
        {
            CustomersWindow csw = new CustomersWindow();
            csw.ShowDialog();
        }

        private void btnViewFlights_Click(object sender, RoutedEventArgs e)
        {
            FlightsWindow flw = new FlightsWindow();
            flw.ShowDialog();
        }

        private void btnViewAirlines_Click(object sender, RoutedEventArgs e)
        {
            AirlinesWindow airw = new AirlinesWindow();
            airw.ShowDialog();
        }
        private void btnViewPassengers_Click(object sender, RoutedEventArgs e)
        {
            PassengersWindow pw = new PassengersWindow();
            pw.ShowDialog();
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
            AboutUs a = new AboutUs();
            a.Show();
        }

        private void ViewCustomers_Click(object sender, RoutedEventArgs e)
        {
            btnViewCust_Click(sender, e);
        }

        private void ViewFlights_Click(object sender, RoutedEventArgs e)
        {
            btnViewFlights_Click(sender, e);
        }

        private void ViewAirlines_Click(object sender, RoutedEventArgs e)
        {
            btnViewAirlines_Click(sender, e);
        }

        private void ViewPassengers_Click(object sender, RoutedEventArgs e)
        {
            btnViewPassengers_Click(sender, e);
        }

        private void MenuInsert_Click(object sender, RoutedEventArgs e)
        {
            DisplayError();
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            DisplayError();
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            DisplayError();
        }

        private void DisplayError()
        {
            MessageBox.Show("This operations are not allowed here", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
