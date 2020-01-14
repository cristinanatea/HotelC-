using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Sibiu
{
    class Room
    {
        CONNECT conn = new CONNECT();
        public bool addRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "Insert into room(number, type, phone, free) values(@rnm,@rtype, @phn, @free)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();


            command.Parameters.Add("@rnm", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@rtype", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", MySqlDbType.VarChar).Value = free;

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

        public DataTable getRooms()
        {
            MySqlCommand command = new MySqlCommand("SELECT* from room", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);
            
            return table;
        }

        public bool editRoom(int number, int type, String phone, String free)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "Update room set type=@rtype, free=@free, phone = @phn where number = @rnm";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();
            
            command.Parameters.Add("@rnm", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@rtype", MySqlDbType.Int32).Value = type;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@free", MySqlDbType.VarChar).Value = free;

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

        public Boolean setRoomFree(int number, string free)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "Update room set free=@free where number = @rnm";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@rnm", MySqlDbType.Int32).Value = number;
            command.Parameters.Add("@free", MySqlDbType.VarChar).Value = free;

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

        public bool removeRoom(int number)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "delete from room where number = @rnm";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@rnm", MySqlDbType.Int32).Value = number;

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
