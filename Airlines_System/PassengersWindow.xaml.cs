using MidTerm_AjayDesai.Model;
using System;
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
    /// Interaction logic for PassengersWindow.xaml
    /// </summary>
    public partial class PassengersWindow : Window
    {
        public PassengersWindow()
        {
            InitializeComponent();
        }

        public List<ModelCustomer> lstCst = new List<ModelCustomer>();
        public List<ModelFlights> lstFlights = new List<ModelFlights>();
        public Stack<ModelPassenger> stkPassengers = new Stack<ModelPassenger>();

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
            FlightsWindow fw = new FlightsWindow();
            fw.Show();
        }

        private void ViewAirlines_Click(object sender, RoutedEventArgs e)
        {
            AirlinesWindow airw = new AirlinesWindow();
            airw.Show();
        }

        private void ViewPassengers_Click(object sender, RoutedEventArgs e)
        {
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

        private void lbPassengers_Loaded(object sender, RoutedEventArgs e)
        {
            stkPassengers = new DBContext().FillPassenger();
            var data = from pas in stkPassengers
                       orderby pas.ID
                       select pas;
            lbPassengers.DataContext = data;
        }

        private void lbPassengers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ModelPassenger selPas = (ModelPassenger)lbPassengers.SelectedItem;
                if (selPas != null)
                {
                    var data = (from c in lstCst
                                join ps in stkPassengers
                                on c.ID equals ps.customerID
                                join fl in lstFlights
                                on ps.flightID equals fl.ID
                                where ps.ID == selPas.ID
                                select new List<int> { c.ID, fl.ID }).First();

                    lbCustomers.SelectedValue = data[0];
                    lbFlights.SelectedValue = data[1];
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Flights Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lbCustomers_Loaded(object sender, RoutedEventArgs e)
        {
            lstCst = new DBContext().FillCust();
            var data = from cst in lstCst
                       orderby cst.ID
                       select cst;
            lbCustomers.DataContext = data;
        }

        private void lbFlights_Loaded(object sender, RoutedEventArgs e)
        {
            lstFlights = new DBContext().FillFlights();
            var data = from fl in lstFlights
                       orderby fl.ID
                       select fl;
            lbFlights.DataContext = data;
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Login.isSuperUser == 0)
                {
                    MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                    return;
                }
                if (lbFlights.SelectedItem != null && lbCustomers.SelectedItem != null)
                {
                    var id = (from pas in stkPassengers
                              orderby pas.ID descending
                              select pas.ID).First();

                    lbPassengers.DataContext = null;
                    stkPassengers.Push(new ModelPassenger(id + 1, (int)lbCustomers.SelectedValue, (int)lbFlights.SelectedValue));

                    var newdata = from ps in stkPassengers
                                  orderby ps.ID
                                  select ps;
                    lbPassengers.DataContext = newdata;
                }
                else
                    MessageBox.Show("Please Select Customer and Flights", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Passenger Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (lbPassengers.SelectedItem == null || lbFlights.SelectedItem == null || lbCustomers.SelectedItem == null)
                {
                    MessageBox.Show("No Flights record is selected, please select one from left listboxes", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ModelPassenger selPass = (ModelPassenger)lbPassengers.SelectedItem;

                var data = (from ps in stkPassengers
                            where ps.ID == selPass.ID
                            select ps).First();

                if (data == null)
                {
                    MessageBox.Show("Passenger ID is not found please try selecting from left Flights list", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (lbPassengers.SelectedItem != null)
                    {
                        if (MessageBox.Show("Do you want to UPDATE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            lbPassengers.DataContext = null;
                            Stack<ModelPassenger> temp = new Stack<ModelPassenger>();
                            while (stkPassengers.Count() > 0)
                            {
                                if (stkPassengers.Peek().ID != data.ID)
                                    temp.Push(stkPassengers.Pop());
                                else
                                {
                                    stkPassengers.Pop();
                                    break;
                                }
                            }
                            stkPassengers.Push(new ModelPassenger(data.ID, (int)lbCustomers.SelectedValue, (int)lbFlights.SelectedValue));
                            while (temp.Count > 0)
                                stkPassengers.Push(temp.Pop());

                            var newdata = from ps in stkPassengers
                                          orderby ps.ID
                                          select ps;
                            lbPassengers.DataContext = newdata;
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
                MessageBox.Show("Something went wrong please try again opening Passengers Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
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
                ModelPassenger selPass = (ModelPassenger)lbPassengers.SelectedItem;

                if (selPass == null)
                {
                    MessageBox.Show("No Flight is selected, please select one from Flights list", "Null reference", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var data = (from ps in stkPassengers
                            where ps.ID == selPass.ID && ps.flightID == selPass.flightID
                            select ps).First();
                if (data != null)
                {
                    if (MessageBox.Show("Do you want to DELETE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        lbPassengers.DataContext = null;
                        Stack<ModelPassenger> temp = new Stack<ModelPassenger>();
                        while (stkPassengers.Count() > 0)
                        {
                            if (stkPassengers.Peek().ID != data.ID)
                                temp.Push(stkPassengers.Pop());
                            else
                            {
                                stkPassengers.Pop();
                                break;
                            }
                        }
                        while (temp.Count > 0)
                            stkPassengers.Push(temp.Pop());

                        var newdata = from ps in stkPassengers
                                      orderby ps.ID
                                      select ps;
                        lbPassengers.DataContext = newdata;
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
    }
}
