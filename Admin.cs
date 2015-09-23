using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Seminars
{
    class Admin 
    {
        static OleDbConnection connect = new OleDbConnection();
        static OleDbCommand command = new OleDbCommand();

        uint i = 0;

        string fname = "";
        string lname = "";
        string email = "";
        string pswd = "";
        uint userType = 0;

        string eName = "";
        string organizer = "";
        string eDate = "";
        string eStartTime = "";
        string eEndTime = "";
        string eTopic = "";
        string eHost = "";
        uint eType = 0;

        public Admin()
        {
            connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=SeminarsDB.accdb;" + @"Persist Security Info=False;";
            connect.Open();
            command.Connection = connect;
        }

        public bool Menu()
        {
            uint choice = 0;

            do
            {
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. Add Events");
                Console.WriteLine("3. View Events");
                Console.WriteLine("4. Back");
                Console.WriteLine("5. Exit");

                choice = uint.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter first name");
                        fname = Console.ReadLine();
                        Console.WriteLine("Enter last name");
                        lname = Console.ReadLine();
                        Console.WriteLine("Enter email id");
                        email = Console.ReadLine();
                        Console.WriteLine("Enter password");
                        pswd = Console.ReadLine();
                        Console.WriteLine("Select user type");

                        command.CommandText = "SELECT UserType FROM UserType";
                        try
                        {
                            OleDbDataReader reader = command.ExecuteReader();
                            Console.WriteLine("S.no.\tUser Type");
                            while (reader.Read())
                            {
                                i++;
                                Console.WriteLine(i + "\t" + reader.GetString(0));
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        userType = uint.Parse(Console.ReadLine());

                        command.CommandText = "INSERT INTO Users(FName,LName,EmailID,Password,UserType) VALUES ('" + fname + "','" + lname + "','" + email + "','" + pswd + "'," + userType + ");";
                        command.ExecuteNonQuery();
                        connect.Close();

                        break;

                    case 2:
                        Console.WriteLine("Enter event name");
                        eName = Console.ReadLine();
                        Console.WriteLine("Select organizer name");

                        command.CommandText = "SELECT FName,LName FROM Users";
                        try
                        {
                            OleDbDataReader reader = command.ExecuteReader();
                            Console.WriteLine("S.no.\tName");
                            while (reader.Read())
                            {
                                i++;
                                Console.WriteLine(i + "\t" + reader.GetString(0)+" "+ reader.GetString(1));
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        organizer = Console.ReadLine();
                        
                        Console.WriteLine("Enter event date (dd/mm/yyyy)");
                        eDate = Console.ReadLine();
                        Console.WriteLine("Enter event start time");
                        eStartTime = Console.ReadLine();
                        Console.WriteLine("Enter event end time");
                        eEndTime = Console.ReadLine();
                        Console.WriteLine("Enter event start time");
                        eStartTime = Console.ReadLine();
                        Console.WriteLine("Enter event topic");
                        eTopic = Console.ReadLine();
                        Console.WriteLine("Enter event host name");
                        eHost = Console.ReadLine();
                        
                        Console.WriteLine("Select event type");

                        command.CommandText = "SELECT EventType FROM EventType";
                        try
                        {
                            OleDbDataReader reader = command.ExecuteReader();
                            Console.WriteLine("S.no.\tEvent Type");
                            while (reader.Read())
                            {
                                i++;
                                Console.WriteLine(i + "\t" + reader.GetString(0));
                            }
                            reader.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        eType = uint.Parse(Console.ReadLine());

                        command.CommandText = "INSERT INTO Events(EventName,Organizer,EventDate,EventStartTime,EventEndTime,EventTopic,EventHost,EventType) VALUES ('" + eName + "','" + organizer + "','" + eDate + "','" + eStartTime + "','" + eEndTime + "','"+eTopic+"','"+eHost+"',"+eType+");";
                        command.ExecuteNonQuery();
                        connect.Close();

                        break;

                    case 3:
                        Console.WriteLine("-*-*-*-*-All Events-*-*-*-*-");
                        Event.ShowEvents();
                        break;

                    case 4:
                        return true;
                        break;

                    case 5:
                        Environment.Exit(0);
                        break;
                }
            } while (choice != 5);
            return true;
        }
    }
}