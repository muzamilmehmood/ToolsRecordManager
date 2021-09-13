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

namespace Record_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            //close button _ closing the app on login screen
            this.Close();
        }

        private void sign_in_Click(object sender, RoutedEventArgs e)
        {

            string name1 = "faizan";
            string pass1 = "toxicated12";
            string name2 = "moin";
            string pass2 = "moin789";

            if (username.Text == name1 && password.Password == pass1 || username.Text == name2 && password.Password == pass2)
            {
                this.Hide();

                home win = new home();  //loads up the home windows
                win.Show();
            }

            else
            {
                MessageBox.Show("Incorrect Password!", "Alert", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}
