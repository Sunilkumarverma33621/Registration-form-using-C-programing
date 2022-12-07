using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace StudentRegistrationForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            load();
        }

        MySqlConnection con = new MySqlConnection("server = localhost;database =c_sharp;uid = root;pwd=\"\";");
        MySqlCommand cmd;
        MySqlDataReader read;
        MySqlDataAdapter drr;
        string id;
        bool mode = true;
        string sql;
                
        private void label2_Click(object sender, EventArgs e)
        {

        }

        public void load()
        {
            try
            {
                sql = "select * from studentregistration";
                cmd = new MySqlCommand(sql, con);
                con.Open();
                read = cmd.ExecuteReader();
                //drr = new MySqlDataAdapter(sql, con);
                dataGridView1.Rows.Clear();
                while(read.Read())
                {
                    dataGridView1.Rows.Add(read[0], read[1], read[2], read[3]);
                }
                con.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void getID(String id)
        {
            sql = "select * from studentregistration where id= '" + id +"'";
            cmd = new MySqlCommand(sql, con);
            con.Open();
            read = cmd.ExecuteReader();
            while(read.Read())
            {
                txtName.Text = read[1].ToString();
                txtCourse.Text = read[2].ToString();
                txtFee.Text = read[3].ToString();
            }
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string course = txtCourse.Text;
            string fee = txtFee.Text;

            if(mode==true)
            {
                sql = "insert into studentregistration (name,course,fee)values(@name,@course,@fee)";
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                MessageBox.Show("Data Recorded");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();

            }
            else
            {
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "update studentregistration set name=@name,course=@course,fee=@fee where id=@id";
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@fee", fee);
                cmd.Parameters.AddWithValue("@id", id);
                MessageBox.Show("Data Updated");
                cmd.ExecuteNonQuery();

                txtName.Clear();
                txtCourse.Clear();
                txtFee.Clear();
                txtName.Focus();
                button1.Text = "Save";
                mode = true;
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Edit"].Index && e.RowIndex>=0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                getID(id);
                button1.Text = "Edit";
            }
            else if (e.ColumnIndex == dataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                mode = false;
                id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                sql = "delete from studentregistration where id=@id";
                con.Open();
                cmd = new MySqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Deleted");
                con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            load();
        }
        



        private void button2_Click_1(object sender, EventArgs e)
        {
            txtName.Clear();
            txtCourse.Clear();
            txtFee.Clear();
            txtName.Focus();
            button1.Text = "Save";
            mode = true;
        }
    }
}