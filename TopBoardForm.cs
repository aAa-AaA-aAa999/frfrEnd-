using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.Odbc;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appFrench
{
    public partial class TopBoardForm : Form
    {
        public TopBoardForm()
        {
            InitializeComponent();
            FillTop();
        }
        private void FillTop()
        {
            Db db = new Db();
            try
            {
                string query = "SELECT Users.Username, UserProgress.SpeedGameRecord " +
                           "FROM Users " +
                           "INNER JOIN UserProgress ON Users.UserID = UserProgress.UserID";

                SqlConnection connection = db.getConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    topList.Items.Clear();
                    while (reader.Read())
                    {
                        string name = reader["Username"].ToString();
                        string score = reader["SpeedGameRecord"].ToString();

                        topList.Items.Add($"Никнейм: {name}");
                        topList.Items.Add($"   Счёт в быстрой игре:  ◌ {score}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("данные не переданы");
                MessageBox.Show("Error: " + ex.Message);
            }
            finally 
            {
                db.closedConnection();
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
