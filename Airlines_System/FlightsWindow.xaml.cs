using MidTerm_AjayDesai.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for FlightsWindow.xaml
    /// </summary>
    public partial class FlightsWindow : Window
    {
        public FlightsWindow()
        {
            InitializeComponent();
        }

        public Queue<ModelAirlines> queAir = new Queue<ModelAirlines>();
        public List<ModelFlights> lstFlights = new List<ModelFlights>();

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
            CustomersWindow cs = new CustomersWindow();
            cs.Show();
        }

        private void ViewFlights_Click(object sender, RoutedEventArgs e)
        {
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
            btnInsert_Click(sender, e);
        }

        private void MenuUpdate_Click(object sender, RoutedEventArgs e)
        {
            btnUpdate_Click(sender, e);
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            btnDelete_Click(sender, e);
        }

        private void lbAirlines_Loaded(object sender, RoutedEventArgs e)
        {
            lstFlights = new DBContext().FillFlights();
            queAir = new DBContext().FillAirlines();
            var data = from air in queAir
                       orderby air.ID
                       select air;
            lbAirlines.DataContext = data;
        }

        private void lbAirlines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ModelAirlines selAirline = (ModelAirlines)lbAirlines.SelectedItem;
                lbFlights.DataContext = null;
                if (selAirline != null)
                {
                    var data = from fl in lstFlights
                               join air in queAir
                               on fl.airlineID equals air.ID
                               where fl.airlineID == selAirline.ID
                               select fl;
                    lbFlights.DataContext = data;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Flightss Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lbFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ModelFlights selFlight = (ModelFlights)lbFlights.SelectedItem;

                if (selFlight != null)
                {
                    var data = (from fl in lstFlights
                                where fl.ID == selFlight.ID && fl.airlineID == selFlight.airlineID
                                select fl).First();

                    txtFlightID.Text = data.ID.ToString();
                    txtDepartureCity.Text = data.DepartureCity;
                    txtDestinationCity.Text = data.DestinationCity;
                    txtDepartureDate.Text = data.DepartureDate;
                    txtFlightTime.Text = data.FlightTime.ToString();
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Flightss Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (Login.isSuperUser == 0)
            {
                MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            try
            {
                if (txtFlightID.Text != "" && txtDepartureCity.Text != "" && txtDestinationCity.Text != "" && txtDepartureDate.Text != "" && txtFlightTime.Text != "" && lbAirlines.SelectedItem != null)
                {
                    int id = int.Parse(txtFlightID.Text);
                    var d = (from fl in lstFlights
                             where fl.ID == id
                             select fl);
                    if (d.Count() > 0)
                    {
                        var aid = (from fl in lstFlights
                                   orderby fl.ID descending
                                   select fl.ID).First();
                        MessageBox.Show("Flights ID is duplicated please change it to next available ID: " + (aid + 1), "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lbFlights.DataContext = null;
                        int AirID = (int)lbAirlines.SelectedValue;
                        lstFlights.Add(new ModelFlights(id, AirID, txtDepartureCity.Text, txtDestinationCity.Text, txtDepartureDate.Text, double.Parse(txtFlightTime.Text)));
                        var newdata = from fl in lstFlights
                                      where fl.airlineID == AirID
                                      select fl;
                        lbFlights.DataContext = newdata;
                    }
                }
                else
                    MessageBox.Show("Every field in the form is mandatory Including Airline Selection, Please Fill/select data", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Flightss Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.isSuperUser == 0)
                {
                    MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                if (txtFlightID.Text == "" || lbAirlines.SelectedItem == null || lbFlights.SelectedItem == null)
                {
                    MessageBox.Show("No Flights record is selected, please select one from left listboxes", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ModelAirlines selAirline = (ModelAirlines)lbAirlines.SelectedItem;
                ModelFlights selFlight = (ModelFlights)lbFlights.SelectedItem;

                int id = int.Parse(txtFlightID.Text);
                var flight = from fl in lstFlights
                             where fl.ID == selFlight.ID && fl.airlineID == selAirline.ID
                             select fl;

                if (flight.Count() == 0)
                {
                    MessageBox.Show("Flights ID is not found please try selecting from left Flights list", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var dupID = from flid in lstFlights
                                where flid.ID == id && id != selFlight.ID
                                select flid;
                    if (dupID.Count() > 0)
                    {
                        MessageBox.Show("Flights ID is duplicated, please try another", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtFlightID.Text != "" && txtDepartureCity.Text != "" && txtDestinationCity.Text != "" && txtDepartureDate.Text != "" && txtFlightTime.Text != "" && lbAirlines.SelectedItem != null && lbFlights.SelectedItem != null)
                    {
                        if (MessageBox.Show("Do you want to UPDATE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            lbFlights.DataContext = null;
                            lstFlights.Remove(flight.First());
                            lstFlights.Add(new ModelFlights(id, (int)lbAirlines.SelectedValue, txtDepartureCity.Text, txtDestinationCity.Text, txtDepartureDate.Text, double.Parse(txtFlightTime.Text)));
                            var newdata = from fl in lstFlights
                                          where fl.airlineID == (int)lbAirlines.SelectedValue
                                          select fl;
                            lbFlights.DataContext = newdata;
                        }
                        else
                        {
                            MessageBox.Show("No changes made", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    else
                        MessageBox.Show("Every field in the form is mandatory, please fill all", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Flights Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.isSuperUser == 0)
                {
                    MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                ModelFlights selFlight = (ModelFlights)lbFlights.SelectedItem;

                if (selFlight == null)
                {
                    MessageBox.Show("No Flights is selected, please select one from Flights list", "Null reference", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var data = (from fl in lstFlights
                            where fl.ID == selFlight.ID && fl.airlineID == selFlight.airlineID
                            select fl).First();
                if (data != null)
                {
                    if (MessageBox.Show("Do you want to DELETE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        lbFlights.DataContext = null;
                        lstFlights.Remove(data);
                        var newdata = from fl in lstFlights
                                      where fl.airlineID == selFlight.airlineID
                                      select fl;
                        lbFlights.DataContext = newdata;
                    }
                    else
                        return;
                }
            }
            catch
            {
                MessageBox.Show("Unable to delete the record", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void txt_Keyup(object sender, KeyEventArgs e)
        {
            TextBox txtbox = new TextBox();
            try
            {
                txtbox = (TextBox)sender;
                if (txtbox == txtFlightID)
                    int.Parse(txtbox.Text);
                else if (txtbox == txtFlightTime)
                    double.Parse(txtFlightTime.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input. value should be positive number", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                if (txtbox == txtFlightID)
                    txtFlightID.Text = "";
                else if (txtbox == txtFlightTime)
                    txtFlightTime.Text = "";
            }
        }
    }
}
