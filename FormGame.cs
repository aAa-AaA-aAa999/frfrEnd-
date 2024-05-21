using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace appFrench
{
    public partial class FormGame : Form
    {
        private int correctAnswers = 0;
        private int wrongAnswers = 0;
        private int id = 0;


        int colourMouseEnter = 0x886ED7; // при наведении мыши
        int colourMouseLeave = 0x6C48D7; // для возврата цвета после ухода мыши
        int colourMouseDown = 0x3B14AF; // при нажатии на текст

        public FormGame(int idUs)
        {
            id = idUs;
            InitializeComponent();

            Paint += BackGround_Paint;

            Label[] labels = this.Controls.OfType<Label>().ToArray();
            LabelChanger.deleteBackgroundLabel(labels);//передаёт массив labels чтобы сделать фон прозрачным

            buttonSkipLevel.MouseEnter += (sender, e) => LabelChanger.lab_MouseEnter(sender, e, colourMouseEnter);//изменение цветов
            buttonSkipLevel.MouseLeave += (sender, e) => LabelChanger.lab_MouseLeave(sender, e, colourMouseLeave);
            buttonSkipLevel.MouseDown += (sender, e) => LabelChanger.lab_MouseDown(sender, e, colourMouseDown);

            StartNewGame();
        }

        private void StartNewGame()
        {
            correctAnswers = 0;
            wrongAnswers = 0;
            LoadNextWord();
        }
        private void LoadNextWord()
        {
            ResetOptionsState();
            string lastWord = labelWordd.Text;
            var wordAndOptions = Db.GetWordAndOptions(lastWord);
            labelWordd.Text = wordAndOptions.Word;

            // Убедитесь, что список опций содержит кортежи с Option и IsCorrect
            buttonOption1.Text = wordAndOptions.Options[0].Option;
            buttonOption1.Tag = wordAndOptions.Options[0].IsCorrect;

            buttonOption2.Text = wordAndOptions.Options[1].Option;
            buttonOption2.Tag = wordAndOptions.Options[1].IsCorrect;

            buttonOption3.Text = wordAndOptions.Options[2].Option;
            buttonOption3.Tag = wordAndOptions.Options[2].IsCorrect;


            // Сделать кнопки видимыми и активными
            buttonOption1.Visible = buttonOption2.Visible = buttonOption3.Visible = true;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }

        private void buttonOption1_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var option = button.Text;
            var isCorrect = (bool)button.Tag; // Предполагаем, что мы сохраняем правильность ответа в Tag кнопки

            if (isCorrect)
            {
                correctAnswers++;
                button.BackColor = Color.FromArgb(179, 255, 210);
                button.FlatAppearance.BorderColor = Color.FromArgb(134, 236, 176);

                // Отключаем кнопки после выбора ответа, пока не будет нажата кнопка "Следующий уровень"
                buttonOption1.Enabled = buttonOption2.Enabled = buttonOption3.Enabled = false;
                buttonNextLevel.Visible = true;

            }
            else
            {
                wrongAnswers++;
                button.BackColor = Color.FromArgb(255, 161, 162);
                button.FlatAppearance.BorderColor = Color.FromArgb(235, 117, 119);

                // Отключаем кнопки после выбора ответа, пока не будет нажата кнопка "Следующий уровень"
                buttonOption1.Enabled = buttonOption2.Enabled = buttonOption3.Enabled = false;
                buttonNextLevel.Visible = true;

                if (wrongAnswers >= 3)
                {
                    saveRecord(correctAnswers);
                    MessageBox.Show($"Игра окончена. Правильных ответов: {correctAnswers}.", "Конец игры", MessageBoxButtons.OK);
                    StartNewGame();
                }
            }
        }
        void saveRecord(int correctAnswer)
        {
            Db db = new Db();
            SqlConnection connection = db.getConnection();
            using (connection)
            {
                connection.Open();
                string quare = "UPDATE Games SET Score = @record, PlayedOn = @date WHERE UserID = @id AND @record > Score AND GameType = 1";

                SqlCommand command = new SqlCommand(quare, connection);
                command.Parameters.AddWithValue("@record", correctAnswer);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                int rowsAffected = command.ExecuteNonQuery();
            }

        }
        private void buttonNextLevel_Click(object sender, EventArgs e)
        {
            LoadNextWord();
        }
        private void ResetOptionsState()
        {
            buttonOption1.BackColor = buttonOption2.BackColor = buttonOption3.BackColor = Color.FromArgb(192, 192, 255);
            buttonOption1.FlatAppearance.BorderColor = buttonOption2.FlatAppearance.BorderColor = buttonOption3.FlatAppearance.BorderColor = Color.FromArgb(128, 128, 255);
            buttonOption1.Enabled = buttonOption2.Enabled = buttonOption3.Enabled = true;
            buttonNextLevel.Visible = false;
        }

        private void buttonSkipLevel_Click(object sender, EventArgs e)
        {
            wrongAnswers++;
            if (wrongAnswers >= 3)
            {
                saveRecord(correctAnswers);
                MessageBox.Show($"Игра окончена. Правильных ответов: {correctAnswers}.", "Конец игры", MessageBoxButtons.OK);
                StartNewGame();
            }
            LoadNextWord();
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveRecord(correctAnswers);
        }


        private void BackGround_Paint(object sender, PaintEventArgs e)
        {

            // Определение цветов для градиента
            Color startColor = Color.FromArgb(207, 209, 255);
            Color endColor = Color.FromArgb(241, 243, 255);

            //Создание градиентной кисти с углом 45 градусов
            LinearGradientBrush brushh = new LinearGradientBrush(
                this.ClientRectangle, startColor, endColor, LinearGradientMode.Vertical);

            // Нарисовать градиентный фон на форме
            e.Graphics.FillRectangle(brushh, this.ClientRectangle);
        }
    }
}