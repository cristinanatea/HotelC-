using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_Sibiu
{
    public partial class Reservations_Form : Form
    {
        Client client = new Client();
        Room room = new Room();
        Reservation reservation = new Reservation();

        public Reservations_Form()
        {
            InitializeComponent();
        }
        private void buttonAddReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int clientID = Convert.ToInt32(comboBoxClientID.SelectedValue.ToString());
                int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue);
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                // date in must be = or > today date
                // date out must be = or > date in
                if (DateTime.Compare(dateIn.Date, DateTime.Now.Date) < 0)
                {
                    MessageBox.Show("The Date In Must Be = or > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (DateTime.Compare(dateOut.Date, dateIn.Date) < 0)
                {
                    MessageBox.Show("The Date Out Must Be = or > To Date In", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (reservation.addReservation(roomNumber, clientID, dateIn, dateOut))
                    {
                        // set the room free column to NO
                        // you can add a message if the room is edited
                        room.setRoomFree(roomNumber, "No");
                        dataGridView2.DataSource = reservation.getAllReserv();
                        MessageBox.Show("New Reservation Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reservation NOT Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void buttonEditReserv_Click(object sender, EventArgs e)
        {

            try
            {
                int rservID = Convert.ToInt32(textBoxReservId.Text);
                int clientID = Convert.ToInt32(comboBoxClientID.SelectedValue.ToString());
                int roomNumber = Convert.ToInt32(comboBoxRoomNumber.SelectedValue.ToString());
                DateTime dateIn = dateTimePickerIN.Value;
                DateTime dateOut = dateTimePickerOUT.Value;

                // date in must be = or > today date
                // date out must be = or > date in
                if (dateIn < DateTime.Now)
                {
                    MessageBox.Show("The Date In Must Be = or > To Today Date", "Invalid Date In", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (dateOut < dateIn)
                {
                    MessageBox.Show("The Date Out Must Be = or > To Date In", "Invalid Date Out", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //rservId
                    if (reservation.editReserv(rservID, roomNumber, clientID, dateIn, dateOut))
                    {
                        // set the room free column to NO
                        // you can add a message if the room is edited
                        room.setRoomFree(roomNumber, "No");
                        dataGridView2.DataSource = reservation.getAllReserv();
                        MessageBox.Show("Reservation Data Updated", "Edit Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Reservation NOT Added", "Add Reservation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void buttonRemoveReserv_Click(object sender, EventArgs e)
        {
            try
            {
                int reservId = Convert.ToInt32(textBoxReservId.Text);
                int roomNumber = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value.ToString());
                if (reservation.removeReserv(reservId))
                {
                    dataGridView2.DataSource = reservation.getAllReserv();
                    // after deleting a reservation we need to set free column to 'Yes'

                    room.setRoomFree(roomNumber, "Yes");
                    MessageBox.Show("Reservation Deleted", "Delete Reservation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Reservation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void Reservations_Form_Load(object sender, EventArgs e)
        {
            comboBoxRoomNumber.DataSource = room.getRooms();
            comboBoxRoomNumber.ValueMember = "number";
            comboBoxRoomNumber.DisplayMember = "number";
            comboBoxRoomNumber.SelectedIndex = 0;

            comboBoxClientID.DataSource = client.getClients();
            comboBoxClientID.ValueMember = "ClientID";
            comboBoxClientID.DisplayMember = "Firstname";
            comboBoxClientID.SelectedIndex = 0;
            

            dataGridView2.DataSource = reservation.getAllReserv();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxReservId.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                comboBoxClientID.SelectedValue = Convert.ToInt32(dataGridView2.CurrentRow.Cells[1].Value.ToString());
                comboBoxRoomNumber.SelectedValue = Convert.ToInt32(dataGridView2.CurrentRow.Cells[2].Value.ToString());

                dateTimePickerIN.Value = DateTime.Parse(dataGridView2.CurrentRow.Cells[3].Value.ToString());
                dateTimePickerOUT.Value = DateTime.Parse(dataGridView2.CurrentRow.Cells[4].Value.ToString());
            }
            catch (Exception ex) { }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellContentClick(sender, e);
        }

        private void comboBoxClientID_Format(object sender, ListControlConvertEventArgs e)
        {
            // Assuming your class called Employee , and Firstname & Lastname are the fields
            var val = ((DataRowView)e.ListItem);

            string lastname = val["firstname"].ToString();
            string firstname = val["lastname"].ToString();
            string clientID = val["ClientID"].ToString();
            e.Value = lastname + " " + firstname + " (" + clientID + ")";
        }
    }
}