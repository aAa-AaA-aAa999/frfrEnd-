using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace appFrench
{
    public partial class FormMain : Form
    {
        ToolTipChanger helpForButtons;
        int id = 0;
        static string log;
        int imageId = 0;
        int phraseId = 0;

        public FormMain(int idUs, string logUs)
        {
            id = idUs; 
            log = logUs;
            InitializeComponent();
            Label[] labels = this.Controls.OfType<Label>().ToArray();
            LabelChanger.deleteBackgroundLabel(labels);//передаёт массив labels чтобы сделать фон прозрачным
            Paint += BackGround_Paint;
            Button[] buttons = this.Controls.OfType<Button>().ToArray();
            foreach (Button button in buttons) { button.BackColor = Color.Transparent; }
            helpForButtons = new ToolTipChanger();
            helpForButtons.SetToolTip(buttonLearnSpeed, "Программа предлагает Вам слово на французском,   \nВам нужно вписать перевод слова, у Вас есть 3 попытки.\nBonne chance ;)");
            helpForButtons.SetToolTip(buttonLearnSlowed, "Программа предлагает Вам слово на французском,   \nВам нужно выбрать перевод слова,из предложенных вариантов.\nУ Вас есть 3 попытки.\nBonne chance ;)");
            LoadImageFromDataBase();
            LoadPhraseFromDataBase();
        }

        private void buttonLearnSlowed_Click(object sender, EventArgs e)
        {
            FormGame form = new FormGame(id);
            form.ShowDialog();
        }

        private void buttonLearnSpeed_Click(object sender, EventArgs e)
        {
            FormSecondGame form = new FormSecondGame(id);
            form.ShowDialog();
        }

        private void buttonProfile_Click(object sender, EventArgs e)
        {
            ProfileForm formx = new ProfileForm(id);
            formx.ShowDialog();
        }

        private void buttonTopArray_Click(object sender, EventArgs e)
        {
            TopBoardForm form2  = new TopBoardForm();
            form2.ShowDialog();
        }

        private void buttonListWord_Click(object sender, EventArgs e)
        {
            ListWordForm form = new ListWordForm();
            form.ShowDialog();
        }

        private void buttonTranslate_Click(object sender, EventArgs e)
        {
            FormTranslate formq = new FormTranslate();
            formq.ShowDialog();
        }
        private void BackGround_Paint(object sender, PaintEventArgs e)
        {

            // Определение цветов для градиента
            Color startColor = Color.FromArgb(131, 145, 255);
            Color endColor = Color.FromArgb(191, 203, 255);

            if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
            {
                // Создание градиентной кисти с углом 45 градусов
                LinearGradientBrush brushh = new LinearGradientBrush(
                    this.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

                // Нарисовать градиентный фон на форме
                e.Graphics.FillRectangle(brushh, this.ClientRectangle);
            }
        }

        private void LoadPhraseFromDataBase() 
        {
            Db db = new Db();
            SqlConnection connection = db.getConnection();

            string queryID = "SELECT PhraseID FROM Phrases WHERE TypePhrase = 'FormMain' ORDER BY NEWID()";
            string query = "SELECT Phrase FROM Phrases WHERE PhraseID=@phrID";
            string queryUpdate = "UPDATE Users " +
                "SET PhraseID = @phrID " +
                "WHERE UserID = @id";

            using (connection) {
                connection.Open();

                SqlCommand commandID = new SqlCommand(queryID, connection);
                phraseId = (int)commandID.ExecuteScalar();

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@phrID", phraseId);
                labelForPhrase.Text = command.ExecuteScalar().ToString();

                SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);
                commandUpdate.Parameters.AddWithValue("@phrID", phraseId);
                commandUpdate.Parameters.AddWithValue("@id", id);

                int rowsAffected = commandUpdate.ExecuteNonQuery();
            }
        }

        private void LoadImageFromDataBase()
        {
            Db db = new Db();
            SqlConnection connection = db.getConnection();
            string queryID = "SELECT ImageID FROM Images WHERE TypeImage = 'FormMain' ORDER BY NEWID()";
            string query = "SELECT Image FROM Images WHERE ImageID=@imgID"; 
            string queryUpdate = "Update Users " +
                "Set ImageID = @imgID " +
                "Where UserID = @id";

            try
            {
                using (connection)
                {
                    connection.Open();

                    SqlCommand commandID = new SqlCommand(queryID, connection);
                    imageId = (int)commandID.ExecuteScalar();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@imgID", imageId);

                    // Чтение изображения из базы данных
                    byte[] imageData = (byte[])command.ExecuteScalar();

                    // Конвертация массива байтов в изображение
                    Image image;
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        image = Image.FromStream(ms);
                    }

                    // Установка изображения в PictureBox
                    pictureBox.Image = image;

                    SqlCommand commandUpdate = new SqlCommand(queryUpdate, connection);
                    commandUpdate.Parameters.AddWithValue("@imgID", imageId);
                    commandUpdate.Parameters.AddWithValue("@id", id);

                    int rowsAffected = commandUpdate.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { 
            connection?.Close();
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public static string getLog() { return log; }

    }
}
