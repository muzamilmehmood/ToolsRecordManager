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
    /// Interaction logic for edit.xaml
    /// </summary>
    public partial class edit : Page
    {
        public edit()
        {
            InitializeComponent();
        }

        string pen_rec; //variable to store value of radio buttons

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            UsersModel u = new UsersModel();

            u.ID = int.Parse(idText.Text);
            u.Name = nameText.Text;
            u.PhoneNumber = double.Parse(phoneNumberText.Text);
            u.Location = locationText.Text;
            u.DOR = DORText.Text;
            u.DOE = DOEText.Text;
            u.ToolName = toolNameText.Text;
            u.PaymentMode = modeOfPaymentText.Text;
            u.Amount = int.Parse(amountText.Text);
            u.Payment = pen_rec;

            if (SqliteDataAccess.CheckUserExist(u.ID) == true)
            {
                SqliteDataAccess.UpdateByID(u);
                MessageBox.Show("Record Updated Successfully", "Success");
            }
            else
            {
                MessageBox.Show("No Record of this ID exist", "Error");
            }

        }


        //Delete Button
        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            UsersModel u = new UsersModel();


            //this if statement is checking if ID is empty otherwise return.
            if (idText.Text == ""){
                MessageBox.Show("ID Field cannot be empty, Please provide a valid ID","ERROR");
                return;
            }

            u.ID = int.Parse(idText.Text);

            //this checks if id exist? if exist then delete.
            if (SqliteDataAccess.CheckUserExist(u.ID) == true)
            {
                int id = int.Parse(idText.Text);

                if (MessageBox.Show("Are you sure you want to Delete the record with ID: " + id, "Delete Record", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    SqliteDataAccess.DeleteUsers(id);
                }
            }

            else
            {
                MessageBox.Show("No Record of this ID exist", "Error");
            }

            
        }


        //checked checkbox of received
        private void receivedText_Checked(object sender, RoutedEventArgs e)
        {
            pen_rec = "Received";
        }

        //checked checkbox of pending
        private void pendingText_Checked(object sender, RoutedEventArgs e)
        {
            pen_rec = "Pending";
        }
    }
}
