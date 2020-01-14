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
    public partial class Room_form : Form
    {
        Room room = new Room();

        public Room_form()
        {
            InitializeComponent();
        }

        private void buttonAddRoom_Click(object sender, EventArgs e)
        {

            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            string phone = textBoxPhone.Text;
            string free = "";

            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);
                if (radioButtonYES.Checked)
                {
                    free = "Yes";
                }
                else if (radioButtonNO.Checked)
                {
                    free = "No";
                }

                if (room.addRoom(number, type, phone, free))
                {
                    dataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("Room Added Successfully", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Not Added", "Add Room", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private void buttonEditRoom_Click(object sender, EventArgs e)
        {

            int type = Convert.ToInt32(comboBoxRoomType.SelectedValue.ToString());
            String phone = textBoxPhone.Text;
            String free = "";

            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);
                if (radioButtonYES.Checked)
                {
                    free = "Yes";
                }
                else if (radioButtonNO.Checked)
                {
                    free = "No";
                }

                if (room.editRoom(number, type, phone, free))
                {
                    dataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("Room Data Updated", "Edit Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Data NOT Updated", "Edit Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void buttonRemoveRoom_Click(object sender, EventArgs e)
        {
            try
            {
                int number = Convert.ToInt32(textBoxNumber.Text);

                if (room.removeRoom(number))
                {
                    dataGridView1.DataSource = room.getRooms();
                    MessageBox.Show("Room Data Deleted", "Remove Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Room Data NOT Deleted", "Remove Room", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Room Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonClearR_Click(object sender, EventArgs e)
        {
            textBoxNumber.Text = "";
            comboBoxRoomType.SelectedIndex = 0;
            textBoxPhone.Text = "";
            radioButtonYES.Checked = true;
        }

        private void Room_form_Load(object sender, EventArgs e)
        {
            var roomTypes = new BindingList<KeyValuePair<int, string>>();

            roomTypes.Add(new KeyValuePair<int, string>(1, "Single"));
            roomTypes.Add(new KeyValuePair<int, string>(2, "Double"));

            comboBoxRoomType.DataSource = roomTypes;
            comboBoxRoomType.ValueMember = "Key";
            comboBoxRoomType.DisplayMember = "Value";
            comboBoxRoomType.SelectedIndex = 0;



            dataGridView1.DataSource = room.getRooms();
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBoxNumber.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                comboBoxRoomType.SelectedValue = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                textBoxPhone.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

                if (dataGridView1.CurrentRow.Cells[3].Value.ToString() == "Yes")
                {
                    radioButtonYES.Checked = true;
                }
                else
                {
                    radioButtonNO.Checked = true;
                }
            }
            catch (Exception ex) { }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1_CellContentClick(sender, e);
        }
    }
}
