using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace Hotel_Sibiu
{
    class Client
    {
        CONNECT conn = new CONNECT();
        public bool insertClient(String firstname, String lastname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String insertQuery = "Insert into client(firstname, lastname, phone, country) values(@fnm,@lnm, @phn, @cty)";
            command.CommandText = insertQuery;
            command.Connection = conn.getConnection();


            command.Parameters.Add("@fnm", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@cty", MySqlDbType.VarChar).Value = country;

            conn.openConnection();

            if(command.ExecuteNonQuery() ==1)
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
        public DataTable getClients()
        {
            MySqlCommand command = new MySqlCommand("SELECT* from CLIENT", conn.getConnection());
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            DataTable table = new DataTable();

            adapter.SelectCommand = command;
            adapter.Fill(table);


            return table;
        }


        public bool editClient(int id, String firstname, String lastname, String phone, String country)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "Update client set firstname=@fnm, lastname=@lnm, phone = @phn, country = @cty where clientID = @cID";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@cID", MySqlDbType.Int32).Value = id;
            command.Parameters.Add("@fnm", MySqlDbType.VarChar).Value = firstname;
            command.Parameters.Add("@lnm", MySqlDbType.VarChar).Value = lastname;
            command.Parameters.Add("@phn", MySqlDbType.VarChar).Value = phone;
            command.Parameters.Add("@cty", MySqlDbType.VarChar).Value = country;

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

        public bool removeClient(int id)
        {
            MySqlCommand command = new MySqlCommand();
            String editQuery = "delete from client where clientID = @cID";
            command.CommandText = editQuery;
            command.Connection = conn.getConnection();

            command.Parameters.Add("@cID", MySqlDbType.Int32).Value = id;

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
