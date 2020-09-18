using MidTerm_AjayDesai.Model;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public static int isSuperUser;
        public Login()
        {
            InitializeComponent();
            new DBContext().FillLogin();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (ModelLogins.dcLogins.ContainsKey(txtUser.Text) && ModelLogins.dcLogins[txtUser.Text].Password.Equals(pbPass.Password))
            {
                isSuperUser = ModelLogins.dcLogins[txtUser.Text].SuperUser;
                MainWindow m = new MainWindow();
                m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Close();
                m.ShowDialog();
            }
            else
                MessageBox.Show("Wrong usename or password", "Warning", MessageBoxButton.OK, MessageBoxImage.Stop);
            btnClear_Click(sender, e);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtUser.Text = pbPass.Password = "";
        }
    }
}
