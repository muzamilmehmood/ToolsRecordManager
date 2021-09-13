using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Record_Manager
{
    public class SqliteDataAccess
    {
        public static List<UsersModel> LoadPeople()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UsersModel>("Select * from Users", new DynamicParameters());

                return output.ToList();
            }
        }

        //save users in db
        public static void saveUsers(UsersModel User)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Users (Name, PhoneNumber, Location, ToolName, DOR, DOE, PaymentMode, Amount, Payment) values (@Name, @PhoneNumber, @Location, @ToolName, @DOR, @DOE, @PaymentMode, @Amount, @Payment)", User);
            }
        }

        //connection string
        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        //search by user_name
        public static List<UsersModel> SearchPeopleByName(string name)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UsersModel>("Select * from Users where Name like'"+name+"%'", new DynamicParameters());

                return output.ToList();
            }
        }

        //search by toolname
        public static List<UsersModel> SearchPeopleByToolName(string toolname)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
                {
                    var output = cnn.Query<UsersModel>("Select * from v_Users where ToolName like '" + toolname + "%'", new DynamicParameters());

                    return output.ToList();
                }
        }

        //search by user_name
        public static List<UsersModel> SearchByExpiry(string date)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UsersModel>("Select * from Users where DOE ='" + date + "'", new DynamicParameters());

                return output.ToList();
            }
        }

        //Update user
        public static void UpdateByID(UsersModel User)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("update Users set Name = @Name, PhoneNumber = @PhoneNumber, Location = @Location, ToolName = @ToolName, DOR = @DOR, DOE = @DOE, PaymentMode = @PaymentMode, Amount = @Amount, Payment = @Payment where ID='"+User.ID+"'", User);
            }
        }

        //Delete User
        public static void DeleteUsers(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("Delete from Users where ID='"+id+"'");
            }
        }

        //Check user exist or not
        public static bool CheckUserExist(int id)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<UsersModel>("Select * from Users where ID ='" + id + "'", new DynamicParameters());

                if (output.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
