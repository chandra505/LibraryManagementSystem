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
    public partial class IssueBookForm : Form
    {
        public IssueBookForm()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\dell\Documents\Mylibrarydb.mdf;Integrated Security=True;Connect Timeout=30");
        private void FillStudent()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select StdId from StudentTbl",Con);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("StdId",typeof(int));
            dt.Load(sdr);
            StdCb.ValueMember = "StdId";
            StdCb.DataSource = dt;
            Con.Close();
        }
        private void FillBook()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("select BookName from BookTbl where Qty>"+0+"", Con);
            SqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("BookName", typeof(string));
            dt.Load(sdr);
            BookCb.ValueMember = "BookName";
            BookCb.DataSource = dt;
            Con.Close();
        }
        public void populate()
        {
            Con.Open();
            string query = "select * from IssueTbl";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            IssueBookDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void fetchstdata()
        {
            Con.Open();
            string query = "select * from StudentTbl where StdId='" + StdCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                stdname.Text=dr["StdName"].ToString();
                stddept.Text = dr["StdDep"].ToString();
                stdphone.Text = dr["StdPhone"].ToString();
            }
            Con.Close();
        }
        private void UpdateBook()
        {
            int Qty,newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + BookCb.SelectedValue.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
               Qty  = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty - 1;
                string query1 = "update BookTbl set Qty = '" + newQty + "' where BookName = '" + BookCb.SelectedValue.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }
            

            Con.Close();
        }
        private void UpdateBookCancellation()
        {
            int Qty, newQty;
            Con.Open();
            string query = "select * from BookTbl where BookName='" + BookCb.SelectedItem.ToString() + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                Qty = Convert.ToInt32(dr["Qty"].ToString());
                newQty = Qty + 1;
                string query1 = "update BookTbl set Qty = '" + newQty + "' where BookName = '" + BookCb.SelectedItem.ToString() + "'";
                SqlCommand cmd1 = new SqlCommand(query1, Con);
                cmd1.ExecuteNonQuery();
            }


            Con.Close();
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (issuenum.Text == "" || stdname.Text == "" || stddept.Text == "" || stdphone.Text == "" )
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
               // string issuedate = issueDate.Value.Day.ToString() + "/"+ issueDate.Value.Month.ToString()+"/"+ issueDate.Value.Year.ToString();
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into IssueTbl values(" + issuenum.Text + ",'" + StdCb.SelectedValue.ToString() + "','" + stdname.Text + "','" + stddept.Text + "','" + stdphone.Text + "','" + BookCb.SelectedValue.ToString() + "','" + this.issueDate.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Book issued successfully");

                Con.Close();
                UpdateBook();
                populate();
            }
        }

        private void IssueBookForm_Load(object sender, EventArgs e)
        {
            populate();
            FillStudent();
            FillBook();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm main = new MainForm();
            main.Show();
        }

        private void StdCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StdCb_SelectedValueChanged(object sender, EventArgs e)
        {

        }

        private void StdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchstdata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (issuenum.Text == "")
            {
                MessageBox.Show("Enter the IssueNumber");
            }
            else
            {
                Con.Open();
                string query = "delete from IssueTbl where IssueNum='" + issuenum.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Issue Successfully Canceled");
                Con.Close();
                UpdateBookCancellation();
                populate();
            }
    }

        private void IssueBookDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = IssueBookDGV.Rows[e.RowIndex];

                issuenum.Text = row.Cells[0].Value.ToString();
                StdCb.Text = row.Cells[1].Value.ToString();
                stdname.Text = row.Cells[2].Value.ToString();
                stddept.Text = row.Cells[3].Value.ToString();
                stdphone.Text = row.Cells[4].Value.ToString();
                BookCb.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
    }
