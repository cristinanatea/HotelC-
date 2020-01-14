using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Sibiu
{
    class Reservation
    {
        CONNECT conn = new CONNECT();
        public bool addReservation(int roomNumber, int clientID, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "Insert into reservation(roomNumber, clientID, CHECKIN, CHECKOUT) values(@rnm,@clientID, @dateIn, @dateOut)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();


            command.Parameters.Add("@rnm", MySqlDbType.Int32).Value = roomNumber;
            command.Parameters.Add("@clientID", MySqlDbType.Int32).Value = clientID;
            command.Parameters.Add("@dateIn", MySqlDbType.Date).Value = dateIn;
            command.Parameters.Add("@dateOut", MySqlDbType.Date).Value = dateOut;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }

        public DataTable getAllReserv()
        {
            MySqlCommand command = new MySqlCommand("SELECT* from reservation", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        public bool editReserv(int rservID, int roomNumber, int clientID, DateTime dateIn, DateTime dateOut)
        {
            MySqlCommand command = new MySqlCommand();

            String editQuery = "Update reservation set roomNumber=@roomNumber, clientID=@clientID, CHECKIN = @dateIn, CHECKOUT = @dateOut where reservID = @rservID";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@roomNumber", MySqlDbType.Int32).Value = roomNumber;
            command.Parameters.Add("@clientID", MySqlDbType.Int32).Value = clientID;
            command.Parameters.Add("@dateIn", MySqlDbType.DateTime).Value = dateIn;
            command.Parameters.Add("@dateOut", MySqlDbType.DateTime).Value = dateOut;
            command.Parameters.Add("@rservID", MySqlDbType.Int32).Value = rservID;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }

        }

        public bool removeReserv(int reservId)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "delete from reservation where reservID = @reservID";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@reservID", MySqlDbType.Int32).Value = reservId;

            conn.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                conn.closeConnection();
                return true;
            }
            else
            {
                conn.closeConnection();
                return false;
            }
        }
    }
}
