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

namespace QUIZ_APPLICATION
{
    public partial class Level5 : Form
    {
        public Level5()
        {
            InitializeComponent();
        }
        string submittedanswer;

        private void button1_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(@"Data Source=DEVI\SQLEXPRESS;Initial Catalog=QUIZ_APPLICATION;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[submittedanswer]
           ([sa])
     VALUES
           ('" + submittedanswer + "')", con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Answer submitted successfully");
            SqlCommand cmd1 = new SqlCommand("SELECT submittedanswer.sa, answer.ans FROM answer INNER JOIN (SELECT TOP 1 sa FROM submittedanswer order by ID DESC) AS submittedanswer ON submittedanswer.sa=answer.ans", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                SqlCommand cmd3 = new SqlCommand(@"INSERT INTO [dbo].[compare]
           ([value])
     VALUES
           ('correct')", con);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                SqlCommand cmd3 = new SqlCommand(@"INSERT INTO [dbo].[compare]
           ([value])
     VALUES
           ('incorrect')", con);
                con.Open();
                cmd3.ExecuteNonQuery();
                con.Close();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "Dim";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "Int";
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "Static";
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            submittedanswer = "Declare";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=DEVI\SQLEXPRESS;Initial Catalog=QUIZ_APPLICATION;Integrated Security=True");
            SqlCommand cmd4 = new SqlCommand("SELECT TOP 5 value FROM compare ORDER BY ID DESC", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd4);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            MessageBox.Show("your answer is " + dt.Rows[0][0]);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Congratulations! You completed all the questions successfully!");
            foreach(Form form in Application.OpenForms.Cast<Form>().ToArray())
            {
                form.Close();
            }
        }
    }
}
