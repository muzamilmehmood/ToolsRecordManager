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
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class addUser : Page
    {
        public addUser()
        {
            InitializeComponent();
        }

        string pen_rec; //variable to store value of radio buttons
        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            UsersModel u = new UsersModel();
            
            u.Name = nameText.Text;
            u.PhoneNumber = double.Parse(phoneNumberText.Text);
            u.Location = locationText.Text;
            u.ToolName = toolNameText.Text;
            u.DOR = DORText.Text;
            u.DOE = DOEText.Text;
            u.PaymentMode = modeOfPaymentText.Text;
            u.Amount = int.Parse(amountText.Text);
            u.Payment = pen_rec;

            SqliteDataAccess.saveUsers(u);

            MessageBox.Show("Record added Successfully");

        }

        private void receivedText_Checked(object sender, RoutedEventArgs e)
        {
            pen_rec = "Received";
        }

        private void pendingText_Checked(object sender, RoutedEventArgs e)
        {
            pen_rec = "Pending";
        }
    }
}
