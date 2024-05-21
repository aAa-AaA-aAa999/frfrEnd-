using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace appFrench
{
    public partial class EntryForm : Form
    {
        int colourMouseEnter = 0x886ED7; // при наведении мыши
        int colourMouseLeave = 0x6C48D7; // для возврата цвета после ухода мыши
        int colourMouseDown = 0x3B14AF; // при нажатии на текст

        public EntryForm()
        {
            InitializeComponent();
            Label[] labels = this.Controls.OfType<Label>().ToArray();
            LabelChanger.deleteBackgroundLabel(labels);//передаёт массив labels чтобы сделать фон прозрачным
            reg.MouseEnter += (sender, e) => LabelChanger.lab_MouseEnter(sender, e, colourMouseEnter);//изменение цветов
            reg.MouseLeave += (sender, e) => LabelChanger.lab_MouseLeave(sender, e, colourMouseLeave);
            reg.MouseDown += (sender, e) => LabelChanger.lab_MouseDown(sender, e, colourMouseDown);
            Paint += BackGround_Paint;
        }

        private void entry_Click(object sender, EventArgs e)
        {
            Boolean auth = AuthenticateUser(logTextBox.Text, passwordTextBox.Text);
            if (auth)
            {
                Db db = new Db();
                SqlConnection connection = db.getConnection();
                using (connection)
                {
                    connection.Open();
                    string quare = "SELECT UserID FROM Users WHERE Username = @log";
                    SqlCommand command = new SqlCommand(quare, connection);
                    command.Parameters.AddWithValue("@log",logTextBox.Text);
                    int id = Convert.ToInt32(command.ExecuteScalar());
                    string quarelog = "SELECT Username FROM Users WHERE Username = @log";
                    command = new SqlCommand(quarelog, connection);
                    command.Parameters.AddWithValue("@log", logTextBox.Text);
                    string log = command.ExecuteScalar().ToString();

                    clearing(logTextBox);
                    clearing(passwordTextBox);
                    FormMain formM = new FormMain(id, log);
                    formM.ShowDialog();
                }
                
            }
            else
            {
                MessageBox.Show("Ошибка входа, данные введены некорректно." +
                    "Возможно, такой пользователь не зарегистрирован");
            }
        }
        public bool AuthenticateUser(string username, string password)
        {
            Db db = new Db();
            bool isAuthenticated = false;
            try
            {
                db.openConnection();
                string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                SqlCommand cmd = new SqlCommand(query, db.getConnection());
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    isAuthenticated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                db.closedConnection();
            }
            return isAuthenticated;
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            FormReg form1 = new FormReg();
            form1.ShowDialog();
        }

        private void clearing(TextBox a)
        {
            a.Text = "";
        }

        private void BackGround_Paint(object sender, PaintEventArgs e)
        {

            // Определение цветов для градиента
            Color startColor = Color.FromArgb(207, 209, 255);
            Color endColor = Color.FromArgb(241, 243, 255);

            if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
            {
                // Создание градиентной кисти с углом 45 градусов
                LinearGradientBrush brushh = new LinearGradientBrush(
                    this.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

                // Нарисовать градиентный фон на форме
                e.Graphics.FillRectangle(brushh, this.ClientRectangle);
            }
        }
    }
}
