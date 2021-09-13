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
    /// Interaction logic for search.xaml
    /// </summary>
    public partial class search : Page
    {
        List<UsersModel> user = new List<UsersModel>(); //collection of users
        public search()
        {
            InitializeComponent();
        }

        private void searchByName_TextChanged(object sender, TextChangedEventArgs e)
        {
            user = SqliteDataAccess.SearchPeopleByName(searchByName.Text);

            searchDataGrid.ItemsSource = user;
        }

        private void searchByTool_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TODO something....
            return;
        }
    }
}
