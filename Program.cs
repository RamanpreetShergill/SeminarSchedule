using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Seminars
{
    class Program
    {
        static OleDbConnection connect = new OleDbConnection();
        static OleDbCommand command = new OleDbCommand();
        
        static string Login()
        {
            connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=SeminarsDB.accdb;" + @"Persist Security Info=False;";
            connect.Open();

            command.Connection = connect;
            command.CommandText = "SELECT EmailID, Password, UserType FROM Users";

            string email = "";
            string pswd = "";
            string utype = "";
            bool flag = false;

            do
            {
                flag = false;
                Console.WriteLine("Enter Email ID");
                email = Console.ReadLine();
                Console.WriteLine("Enter Password");
                pswd = Console.ReadLine();

                try
                {
                    OleDbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (email == reader.GetString(0) || pswd == reader.GetString(1))
                        {
                            Console.WriteLine("Welcome.\nYou are logged in successfully.");
                            utype = reader.GetValue(2).ToString();
                            connect.Close();
                            return utype;
                        }
                        else
                        {
                            flag = true;
                            Console.WriteLine("Invalid email ID or password");
                        }
                    }
                }
                catch (Exception ex)
                {
                    flag = true;
                    Console.WriteLine(ex.Message);
                }
            } while (flag);
            
            connect.Close();
            return null;
        }

        static void Main(string[] args)
        {
            Event evnt;

            uint choice = 0;
            uint i = 0;
            
            bool flag = false;

            string fname = "";
            string lname = "";
            string email = "";
            string pswd = "";
            string userType = "";
            string pcode = "";

            string eName = "";
            string organizer = "";
            string eDate = "";
            string eStartTime = "";
            string eEndTime = "";
            string eTopic = "";
            string eHost = "";
            uint eType = 0;

            do
            {
                Console.WriteLine("******WelCome*******");
                if (flag)
                {
                    Console.WriteLine("1. Logout");
                }
                else
                    Console.WriteLine("1. Login");
                Console.WriteLine("2. View Events");
                Console.WriteLine("3. Exit");
                try
                {
                    choice = uint.Parse(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e);
                }

                switch (choice)
                {
                    case 1:
                        if (!flag)
                        {
                            userType = Login();
                            flag = true;
                        }
                        else
                        {
                            flag = false;
                            Console.WriteLine("User logged out successfully");
                        }

                        if (userType == "1")
                        {
                            Admin ad = new Admin();
                            flag = ad.Menu();
                        }
                        if(userType == "2")
                        {
                            Guest gt = new Guest();
                            flag = gt.Menu();
                        }
                        break;

                    case 2:
                        Console.WriteLine("-*-*-*-*-All Events-*-*-*-*-");
                        Event.ShowEvents();
                        break;

                    case 3:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid Selection.");
                        break;
                }
            } while (choice != 3 || flag);

            connect.Close();
            Console.ReadKey();
        }
    }
}
