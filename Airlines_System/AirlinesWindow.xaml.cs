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
    /// Interaction logic for AirlinesWindow.xaml
    /// </summary>
    public partial class AirlinesWindow : Window
    {
        public Queue<ModelAirlines> queAir = new Queue<ModelAirlines>();
        private string rbAirplane = "";
        private string rbMeal = "";
        public AirlinesWindow()
        {
            InitializeComponent();
        }
        private void Menu_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuQuit.Foreground = Brushes.Black;
        }

        private void Menu_LostFocus(object sender, RoutedEventArgs e)
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
                ModelAirlines selAir = (ModelAirlines)lbAirlines.SelectedItem;
                if (selAir != null)
                {
                    var data = (from air in queAir
                                where air.ID == selAir.ID && air.Name == selAir.Name
                                select air).First();

                    txtAirID.Text = data.ID.ToString();
                    txtAirName.Text = data.Name;
                    txtSeats.Text = data.SeatsAvailable.ToString();
                    rbAirplane = data.Airplane;
                    rbMeal = data.MealAvailable;
                    switch (rbAirplane)
                    {
                        case "Boeing 777":
                            rbBoeing.IsChecked = true;
                            break;
                        case "Airbus 320":
                            rbAirbus.IsChecked = true;
                            break;
                        case "Bombardier Q":
                            rbBmbrdQ.IsChecked = true;
                            break;
                        default:
                            rbBoeing.IsChecked = false;
                            rbAirbus.IsChecked = false;
                            rbBmbrdQ.IsChecked = false;
                            break;
                    }

                    switch (rbMeal)
                    {
                        case "Veg-salad":
                            rbVeg.IsChecked = true;
                            break;
                        case "Chicken":
                            rbChicken.IsChecked = true;
                            break;
                        case "Sushi":
                            rbSushi.IsChecked = true;
                            break;
                        default:
                            rbVeg.IsChecked = false;
                            rbSushi.IsChecked = false;
                            rbChicken.IsChecked = false;
                            break;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Airlines Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
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
                if (txtAirID.Text != "" && txtAirName.Text != "" && txtSeats.Text != "" && rbMeal != "" && rbAirplane != "")
                {
                    int id = int.Parse(txtAirID.Text);
                    var data = (from air in queAir
                                where air.ID == id
                                select air);
                    if (data.Count() > 0)
                    {
                        var aid = (from ar in queAir
                                   orderby ar.ID descending
                                   select ar.ID).First();
                        MessageBox.Show("Airline ID is duplicated please change it to next available ID: " + (aid + 1), "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        lbAirlines.DataContext = null;
                        queAir.Enqueue(new ModelAirlines(id, txtAirName.Text, rbAirplane, int.Parse(txtSeats.Text), rbMeal));
                        lbAirlines.DataContext = queAir;
                    }
                }
                else
                    MessageBox.Show("Every field in the form is mandatory, please fill all", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Something went wrong please try again opening Airlines Window", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Login.isSuperUser == 0)
            {
                MessageBox.Show("You are not allowed to perform this operation, Ask SuperUser for help", "Unauthorized access", MessageBoxButton.OK, MessageBoxImage.Hand);
                return;
            }
            try
            {
                if (txtAirID.Text == "" || lbAirlines.SelectedItem == null)
                {
                    MessageBox.Show("No Airline record is selected, please select one from left listbox", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ModelAirlines selAirline = (ModelAirlines)lbAirlines.SelectedItem;

                int id = int.Parse(txtAirID.Text);
                var data = from air in queAir
                           where air.ID == selAirline.ID
                           select air;

                if (data.Count() == 0)
                {
                    MessageBox.Show("Airline ID is not found please try selecting from left Airline list", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var dupID = from dair in queAir
                                where dair.ID == id && id != selAirline.ID
                                select dair;
                    if (dupID.Count() > 0)
                    {
                        MessageBox.Show("Airline ID is duplicated, please try another", "Error code", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (txtAirID.Text != "" && txtAirName.Text != "" && txtSeats.Text != "" && rbMeal != "" && rbAirplane != "")
                    {
                        if (MessageBox.Show("Do you want to UPDATE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            lbAirlines.DataContext = null;

                            Queue<ModelAirlines> qAir = new Queue<ModelAirlines>();
                            lbAirlines.DataContext = null;
                            while (queAir.Count() > 0)
                            {
                                if (queAir.Peek().ID != data.First().ID)
                                    qAir.Enqueue(queAir.Dequeue());
                                else
                                {
                                    queAir.Dequeue();
                                    queAir.Enqueue(new ModelAirlines(id, txtAirName.Text, rbAirplane, int.Parse(txtSeats.Text), rbMeal));
                                    break;
                                }
                            }
                            while (qAir.Count > 0)
                                queAir.Enqueue(qAir.Dequeue());

                            lbAirlines.DataContext = queAir;
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
                MessageBox.Show("Something went wrong please try again", "Unhandled error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                ModelAirlines selAir = (ModelAirlines)lbAirlines.SelectedItem;
                if (selAir == null)
                {
                    MessageBox.Show("No Airline is selected, please select one from Airline list", "Null reference", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var data = (from air in queAir
                            where air.ID == selAir.ID && air.Name == selAir.Name
                            select air).First();
                if (data != null)
                {
                    if (MessageBox.Show("Do you want to DELETE this record?", "Confirm Changes", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Queue<ModelAirlines> qAir = new Queue<ModelAirlines>();
                        lbAirlines.DataContext = null;
                        while (queAir.Count() > 0)
                        {
                            if (queAir.Peek().ID != data.ID)
                                qAir.Enqueue(queAir.Dequeue());
                            else
                            {
                                queAir.Dequeue();
                                break;
                            }
                        }
                        while (qAir.Count > 0)
                            queAir.Enqueue(qAir.Dequeue());
                        lbAirlines.DataContext = queAir;
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

        private void rbPlane_Checked(object sender, RoutedEventArgs e)
        {
            var rb = (RadioButton)sender;
            rbAirplane = rb.Content.ToString();
        }
        private void rbMeal_Checked(object sender, RoutedEventArgs e)
        {
            var rb = (RadioButton)sender;
            rbMeal = rb.Content.ToString();
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
                if (txtbox == txtAirID)
                    txtAirID.Text = "";
                else if (txtbox == txtSeats)
                    txtSeats.Text = "";
            }
        }
    }
}
