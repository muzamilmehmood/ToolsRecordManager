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
    /// Interaction logic for expirytoday.xaml
    /// </summary>
    public partial class expirytoday : Page
    {
        List<UsersModel> user = new List<UsersModel>(); //collection of users

        public expirytoday()
        {
            InitializeComponent();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            user = SqliteDataAccess.SearchByExpiry(expDate.Text);

            expiryDataGrid.ItemsSource = user;
        }
    }
}
