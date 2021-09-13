using OfficeOpenXml;
using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Record_Manager
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Window
    {


        public home()
        {
            InitializeComponent();

            CheckForUpdates();
        }

        private async Task CheckForUpdates()
        {
            using (var manager = new UpdateManager(@"C:\Temp\Releases"))
            {
                await manager.UpdateApp();
            }
        }

        private void sign_out_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void dashboard_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            lbl1.Content = "DASHBOARD";
        }

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "ADD USER";
            mainframe.Content = new addUser();
        }

        private void record_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "RECORDS";

            mainframe.Content = new Record();

        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "SEARCH";

            mainframe.Content = new search();
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "UPDATE";

            mainframe.Content = new edit();
        }

        private void exp_today_Click(object sender, RoutedEventArgs e)
        {
            lbl1.Content = "EXPIRIES";

            mainframe.Content = new expirytoday();
        }

        private async void exportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<UsersModel> users = new List<UsersModel>(); //collection of users

             users = SqliteDataAccess.LoadPeople();  //loading the data from db and saving into users variable


            // adding license of epplus TO read write excel
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var file = new FileInfo(@"C:\Temp\ExportedData.xlsx");

            await SaveExcelFile(users, file);

            MessageBox.Show("Data is successfully Exported!", "Information", MessageBoxButton.OK);
        }

        // saving the data to ExcelFile
        private async Task SaveExcelFile(List<UsersModel> users, FileInfo file)
        {
            DeleteIfExist(file);

            using (var package = new ExcelPackage(file))
            {
                var ws = package.Workbook.Worksheets.Add("Record");

                var range = ws.Cells["A1"].LoadFromCollection(users, true);
                range.AutoFitColumns();

                await package.SaveAsync();
            }
        }


        //delete the excel file if exist | used by SaveExcelFile method
        private void DeleteIfExist(FileInfo file) 
        {
            if (file.Exists)
            {
                file.Delete();
            }
        }

        private async void importBtn_ClickAsync(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to import the data to your Application? If the data already exist, it may cause data redundancy. " +
                "Note: It is recommended to import the Data only after an update.","Information", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var file = new FileInfo(@"C:\Temp\ExportedData.xlsx");
                if (file.Exists)
                {
                    List<UsersModel> peopleFromExcel = await LoadExcelFile(file);

                    foreach (UsersModel people in peopleFromExcel)
                    {
                        SqliteDataAccess.saveUsers(people);
                    }

                    MessageBox.Show("Data imported successfully", "Success", MessageBoxButton.OK);
                    return;
                }
            }
            MessageBox.Show("Either Action is Cancelled or File does not exist for an import", "Error", MessageBoxButton.OK);
        }

        private async Task<List<UsersModel>> LoadExcelFile(FileInfo file)
        {
            List<UsersModel> people = new List<UsersModel>();

            using(var package = new ExcelPackage(file))
            {
                await package.LoadAsync(file);

                var ws = package.Workbook.Worksheets[0];
                int row = 2;
                int col = 2;
                while (string.IsNullOrEmpty(ws.Cells[row,col].Value?.ToString()) == false)
                {
                    UsersModel u = new UsersModel();
                    u.Name = ws.Cells[row, col].Value.ToString();
                    u.PhoneNumber = double.Parse(ws.Cells[row, col + 1].Value.ToString());
                    u.Location = ws.Cells[row, col + 2].Value.ToString();
                    u.ToolName = ws.Cells[row, col + 3].Value.ToString();
                    u.DOR = ws.Cells[row, col + 4].Value.ToString();
                    u.DOE = ws.Cells[row, col + 5].Value.ToString();
                    u.PaymentMode = ws.Cells[row, col + 6].Value.ToString();
                    u.Amount = int.Parse(ws.Cells[row, col + 7].Value.ToString());
                    u.Payment = ws.Cells[row, col + 8].Value.ToString();
                    people.Add(u);
                    row += 1;
                }

                return people;
            }
        }
    }
}
