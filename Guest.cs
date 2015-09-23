using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Seminars
{
    class Guest 
    {
        static OleDbConnection connect = new OleDbConnection();
        static OleDbCommand command = new OleDbCommand();

        public Guest()
        {
            connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=SeminarsDB.accdb;" + @"Persist Security Info=False;";
            connect.Open();
            command.Connection = connect;
        }

        public bool Menu()
        {
            uint choice = 0;
            int i = 0;
            string evnt="";
            string uName = "";

            do
            {
                Console.WriteLine("1. View Events");
                Console.WriteLine("2. Register for Event");
                Console.WriteLine("3. Back");
                Console.WriteLine("4. Exit");

                choice = uint.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("-*-*-*-*-All Events-*-*-*-*-");
                        Event.ShowEvents();
                        break;

                    case 2:
                        Console.WriteLine("-*-*-*-*-All Events-*-*-*-*-");
                        Event.ShowEvents();
                        Console.WriteLine("Select event number");
                        OleDbDataReader reader = command.ExecuteReader();
                            
                        command.CommandText = "SELECT EventID,EventName,EventDate FROM Events";
                        try
                        {
                            while (reader.Read())
                            {
                                evnt= reader.GetString(0).ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        Console.WriteLine("Enter user name");
                        uName = Console.ReadLine();

                        command.CommandText = "INSERT INTO Register(UserName,EventName,EventDate) VALUES ('" + uName + "','" + reader.GetString(1) + "','" + reader.GetString(3) + ");";
                        command.ExecuteNonQuery();
                        connect.Close();

                        break;

                    case 3:
                        return true;
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            } while (choice != 5);
            return true;
        }
    }
}
