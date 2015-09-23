using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace Seminars
{
    class Event
    {
        static OleDbConnection connect = new OleDbConnection();
        static OleDbCommand command = new OleDbCommand();

        string eventName = "";
        string organizer = "";
        string eventDate = "";
        string eventStartTime = "";
        string eventEndTime = "";
        string eventTopic = "";
        string eventHost = "";
        string eventType = "";

        public static void ShowEvents()
        {
            uint i=1;

            connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=SeminarsDB.accdb;" + @"Persist Security Info=False;";
            connect.Open();
            command.Connection = connect;
            
            command.CommandText = "SELECT EventName,EventDate,EventTopic FROM Events";
            
            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                Console.WriteLine("S.No.\tEvent Name\tEvent Date\tEvent Topic");
                while (reader.Read())
                {
                    Console.WriteLine(i + "\t" + reader.GetString(0) + "\t" + reader.GetString(1) + "\t" + reader.GetString(2));
                    i++;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            connect.Close();
        }

        public static void AddEvent(string ename, string org, string edate, string estime, string eetime, string etopic, string ehost, uint etype)
        {
            connect.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + @"Data Source=SeminarsDB.accdb;" + @"Persist Security Info=False;";
            connect.Open();
            command.Connection = connect;
            
            command.CommandText = "INSERT INTO Events(EventName,Organizer,EventDate,EventStartTime,EventEndTime,EventTopic,EventHost,EventType) VALUES ('"+ename+"','"+org+"','"+edate+"','"+estime+"','"+eetime+"','"+etopic+"','"+ehost+"',"+etype+")";
            command.ExecuteNonQuery();
            connect.Close();
        }
    }
}
