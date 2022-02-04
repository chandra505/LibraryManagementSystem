using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class LibrarianForm : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\Documents\Mylibrarydb.mdf;Integrated Security=True;Connect Timeout=30");
        public LibrarianForm()
        {
            InitializeComponent();
        }

        private void LibrarianForm_Load(object sender, EventArgs e)
        {
            populate();
        }
        //Retrieving data from database for presenting in DATAGRIDVIEW
        public void populate()
        {
            Con.Open();
            string query = "select * from LibrarianTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            LibrarianDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into LibrarianTbl values(" + LibId.Text + ",'" + LibName.Text + "','" + LibPass.Text + "','" + LibPhone.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Librarian added successfully");

                Con.Close();
                populate();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "")
            {
                MessageBox.Show("Enter the Librarian ID");
            }
            else
            {
                Con.Open();
                string query = "delete from LibrarianTbl where LibId=" + LibId.Text + "";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Deleted");
                Con.Close();
                populate();
            }
        }

        private void LibrarianDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //libid.text = librariandgv.selectedrows[0].cells[0].value.tostring();
            //libname.text = librariandgv.selectedrows[0].cells[1].value.tostring();
            //libpass.text = librariandgv.selectedrows[0].cells[2].value.tostring();
            //libphone.text = librariandgv.selectedrows[0].cells[3].value.tostring();
        //    if (e.RowIndex >= 0)
        //    {
        //        DataGridViewRow row = LibrarianDGV.Rows[e.RowIndex];
        //        LibId.Text = row.Cells[0].Value.ToString();
        //        LibName.Text = row.Cells[1].Value.ToString();
        //        LibPass.Text = row.Cells[2].Value.ToString();
        //        LibPhone.Text = row.Cells[2].Value.ToString();
        //    }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (LibId.Text == "" || LibName.Text == "" || LibPass.Text == "" || LibPhone.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                Con.Open();
                string query = "update LibrarianTbl set LibName='"+LibName.Text+"',LibPassword='"+LibPass.Text+"',LibPhone='"+LibPhone.Text+"' where LibId='"+LibId.Text+"'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Successfully Updated");
                Con.Close();
                populate();
            }
        }

        private void LibrarianDGV_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
             DataGridViewRow row = LibrarianDGV.Rows[e.RowIndex];
                LibId.Text = row.Cells[0].Value.ToString();
               LibName.Text = row.Cells[1].Value.ToString();
               LibPass.Text = row.Cells[2].Value.ToString();
                LibPhone.Text = row.Cells[3].Value.ToString();
            }
            /*LibId.Text = LibrarianDGV.SelectedRows[0].Cells[0].Value.ToString();
            LibName.Text = LibrarianDGV.SelectedRows[0].Cells[1].Value.ToString();
            LibPass.Text = LibrarianDGV.SelectedRows[0].Cells[2].Value.ToString();
            LibPhone.Text = LibrarianDGV.SelectedRows[0].Cells[3].Value.ToString();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
