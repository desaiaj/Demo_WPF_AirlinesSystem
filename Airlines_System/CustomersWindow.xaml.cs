using MidTerm_AjayDesai.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CustomersWindow.xaml
    /// </summary>
    public partial class CustomersWindow : Window
    {
        public List<ModelCustomer> lstCst = new List<ModelCustomer>();
        public CustomersWindow()
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
            AboutUs a = new AboutUs();
            a.Show();
        }

        private void ViewCustomers_Click(object sender, RoutedEventArgs e)
        {
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

        private void lbCustName_Loaded(object sender, RoutedEventArgs e)
        {
            lstCst = new DBContext().FillCust();
            var data = from cst in lstCst
                       orderby cst.ID
                       select cst;
            lbCustName.DataContext = data;
        }

        private void lbCustName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ModelCustomer selCust = (ModelCustomer)lbCustName.SelectedItem;
                if (selCust != null)
                {
                    var d = (from cs in lstCst
                             where cs.ID == selCust.ID && cs.Name == selCust.Name
                             select cs).First();

                    txtCstID.Text = d.ID.ToString();
                    txtCstName.Text = d.Name;
                    txtCstAddr.Text = d.Address;
                    txtxCstEmail.Text = d.Email;
                    txtCstPhone.Text = d.Phone;
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Customers Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (txtCstID.Text != "" && txtCstName.Text != "" && txtCstAddr.Text != "" && txtxCstEmail.Text != "" && txtCstPhone.Text != "")
                {
                    int id = int.Parse(txtCstID.Text);
                    var d = (from cs in lstCst
                             where cs.ID == id
                             select cs);
                    if (d.Count() > 0)
                    {
                        var aid = (from cs in lstCst
                                   orderby cs.ID descending
                                   select cs.ID).First();
                        MessageBox.Show("Customer ID is duplicated please change it to next available ID: " + (aid + 1), "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lbCustName.DataContext = null;
                        lstCst.Add(new ModelCustomer(id, txtCstName.Text, txtCstAddr.Text, txtxCstEmail.Text, txtCstPhone.Text));
                        lbCustName.DataContext = lstCst;
                    }
                }
                else
                    MessageBox.Show("Every field in the form is mandatory, please fill all", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Customers Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (txtCstID.Text == "" || lbCustName.SelectedItem == null)
                {
                    MessageBox.Show("No Customer record is selected, please select one from left listbox", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ModelCustomer selCust = (ModelCustomer)lbCustName.SelectedItem;

                int id = int.Parse(txtCstID.Text);
                var cust = from cs in lstCst
                           where cs.ID == selCust.ID
                           select cs;

                if (cust.Count() == 0)
                {
                    MessageBox.Show("Customer ID is not found please try selecting from left customer list", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var dupID = from cid in lstCst
                                where cid.ID == id && id != selCust.ID
                                select cid;
                    if (dupID.Count() > 0)
                    {
                        MessageBox.Show("Customer ID is duplicated, please try another", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtCstID.Text != "" && txtCstName.Text != "" && txtCstAddr.Text != "" && txtxCstEmail.Text != "" && txtCstPhone.Text != "")
                    {
                        if (MessageBox.Show("Do you want to UPDATE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            lbCustName.DataContext = null;
                            lstCst.Remove(cust.First());
                            lstCst.Add(new ModelCustomer(id, txtCstName.Text, txtCstAddr.Text, txtxCstEmail.Text, txtCstPhone.Text));
                            lbCustName.DataContext = lstCst;
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
                MessageBox.Show("Something went wrong please try again opening Airlines Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Login.isSuperUser == 0)
            {
                MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            try
            {
                ModelCustomer selCust = (ModelCustomer)lbCustName.SelectedItem;
                if (selCust == null)
                {
                    MessageBox.Show("No customer is selected, please select one from customer list", "Null reference", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var d = (from cs in lstCst
                         where cs.ID == selCust.ID && cs.Name == selCust.Name
                         select cs).First();
                if (d != null)
                {
                    if (MessageBox.Show("Do you want to DELETE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        lbCustName.DataContext = null;
                        lstCst.Remove(d);
                        lbCustName.DataContext = lstCst;
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
                int.Parse(txtbox.Text);
            }
            catch
            {
                MessageBox.Show("Invalid input. value should be positive number", "Invalid input", MessageBoxButton.OK, MessageBoxImage.Error);
                if (txtbox == txtCstID)
                    txtCstID.Text = "";

            }
        }
    }
}
