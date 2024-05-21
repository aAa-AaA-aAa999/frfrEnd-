using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace appFrench
{
    public partial class FormSecondGame : Form
    {
        int id = 0;
        int count = 0;
        int liveHearts = 3;
        public FormSecondGame(int id_us)
        {
            id = id_us;
            InitializeComponent();

            Label[] labels = this.Controls.OfType<Label>().ToArray();
            LabelChanger.deleteBackgroundLabel(labels);

            wordCreate(labelForWord.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SearchTranslation();
            if (count != 0)
            {
                if (count <= 29)
                {
                    LoadPhraseFromDataBase(true);
                }
                else
                {
                    LoadPhraseFromDataBase(false);
                }
            }
        }
        private void SearchTranslation()
        {
            // Получаем слово из Label
            string wordToTranslate = labelForWord.Text;

            Db db = new Db();
            SqlConnection connection = db.getConnection();

            string query = "SELECT Translation FROM Words WHERE Word = @Word AND IsCorrect = 1";

            using (connection)
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Word", wordToTranslate);

                connection.Open();
                object translation = command.ExecuteScalar();
                connection.Close();

                if (translation.ToString().Equals(textBox1.Text, StringComparison.OrdinalIgnoreCase))
                {
                    count++;
                    labelCount.Text = count.ToString();
                    textBox1.Clear();
                    wordCreate(labelForWord.Text);
                }
                else
                {
                    liveHearts--;
                    if (liveHearts > 0)
                    {
                        textBox1.Clear();
                        wordCreate(labelForWord.Text);
                    }
                    else
                    {
                        saveRecord(count);
                        MessageBox.Show($"Игра окончена. Правильных ответов: {count}.", "Конец игры", MessageBoxButtons.OK);
                        count = 0;
                        liveHearts = 3; labelCount.Text = count.ToString();
                        textBox1.Clear();
                        labelForPhrase.Text = "";
                        wordCreate(labelForWord.Text);
                    }
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
                string quare = "UPDATE Games SET Score = @record, PlayedOn = @date WHERE UserID = @id AND @record > Score AND GameType = 2";
                string quareProgress = "UPDATE UserProgress SET SpeedGameRecord = @record WHERE UserID = @id AND @record > SpeedGameRecord";

                SqlCommand command = new SqlCommand(quare, connection);
                command.Parameters.AddWithValue("@record", correctAnswer);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@date", DateTime.Now);

                SqlCommand commandProgress = new SqlCommand(quareProgress, connection);
                commandProgress.Parameters.AddWithValue("@record", correctAnswer);
                commandProgress.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();
                int rowsAffected1 = commandProgress.ExecuteNonQuery();
            }

        }
        void wordCreate(string lastWordSpeedGame)
        {
            Db db = new Db();
            using (var connection = db.getConnection())
            {
                connection.Open();

                string query = "SELECT TOP 1 WordID, Word FROM Words WHERE LanguagePair = 'FR-RU'" +
                    "AND Word <> @lastWord ";

                List<string> selectedCategories = ListExtensions.LoadCheckedListBoxState(ListExtensions.getFilePath())
                   .Where(category => category.IsChecked)
                   .Select(category => category.Category)
                   .ToList();
                try
                {
                    // Динамическое формирование запроса, добавление к нему выбранных категорий
                    if (selectedCategories.Any())
                    {
                        // Формируем строку с условиями для выбранных категорий
                        string categoriesCondition = string.Join(" OR ", selectedCategories.Select(category => $"Category = '{category}'"));

                        // Добавляем условие к базовому запросу
                        query += $" AND ({categoriesCondition})";
                    }
                    query += " ORDER BY NEWID();";

                    // Получаем случайное слово на французском
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@lastWord", lastWordSpeedGame);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("No words found");
                        }
                        labelForWord.Text = reader["Word"].ToString();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Возникли ошибки, возможно файл был изменён.\nВ данной игре будут применены все категории. \n\nДля настройки категорий Вы можете перейти в профиль;)");
                    query = "SELECT TOP 1 WordID, Word FROM Words WHERE LanguagePair = 'FR-RU'" +
                    "AND Word <> @lastWord ORDER BY NEWID();";
                    var command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@lastWord", lastWordSpeedGame);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("No words found");
                        }
                        labelForWord.Text = reader["Word"].ToString();
                    }
                    ListExtensions.SaveCheckedListBoxState(ListExtensions.getFilePath(),ListExtensions.GetUniqueCategoriesFromDatabase());
                }
            }
        }
        private void LoadPhraseFromDataBase(bool scoreLess30)
        {
            Db db = new Db();
            SqlConnection connection = db.getConnection();

            string queryLess30 = "SELECT Phrase FROM Phrases WHERE TypePhrase = 'scoreLess30' ORDER BY NEWID()";
            string queryMore30 = "SELECT Phrase FROM Phrases WHERE TypePhrase = 'scoreMore30' ORDER BY NEWID()";
            SqlCommand command;

            using (connection)
            {
                connection.Open();
                if (scoreLess30) {
                    command = new SqlCommand(queryLess30, connection);
                    labelForPhrase.Text = command.ExecuteScalar().ToString();
                    int rowsAffected = command.ExecuteNonQuery();

                }
                else {
                    command = new SqlCommand(queryMore30, connection);
                    labelForPhrase.Text = command.ExecuteScalar().ToString();
                    int rowsAffected = command.ExecuteNonQuery();

                }
            }
        }

        private void FormSecondGame_Load(object sender, EventArgs e)
        {
                // Цвета для верхней половины формы (от верха к центру)
                Color startColor = Color.FromArgb(207, 209, 255); // Начальный цвет 
                Color endColor = Color.FromArgb(241, 243, 255); // Конечный цвет 

            if (this.ClientRectangle.Width > 0 && this.ClientRectangle.Height > 0)
            {

                // Создание градиентных кистей для верхней и нижней половин формы
                LinearGradientBrush topFirstHalfBrush = new LinearGradientBrush(
                        new Point(this.ClientSize.Width / 2, 0), new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 4),
                        startColor, endColor); // Цвет, который будет использоваться в середине верхней половины

                LinearGradientBrush bottomBrush = new LinearGradientBrush(
                    new Point(this.ClientSize.Width / 2, this.ClientSize.Height / 2), new Point(this.ClientSize.Width / 2, this.ClientSize.Height),
                    endColor, startColor);

                // Нарисовать градиентный фон на форме
                this.Paint += (s, pe) =>
                {
                    // Верхняя половина формы: первая половина
                    pe.Graphics.FillRectangle(topFirstHalfBrush, new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height / 4));
                    // Верхняя половина формы: вторая половина
                    pe.Graphics.FillRectangle(new SolidBrush(endColor),
                                               new Rectangle(0, 0 + this.ClientSize.Height / 4,
                                               this.ClientSize.Width, this.ClientSize.Height / 4));                // Нижняя половина формы
                    pe.Graphics.FillRectangle(bottomBrush, new Rectangle(0, (this.ClientSize.Height / 2) + 1, this.ClientSize.Width, this.ClientSize.Height / 2));
                };
            }
        }
        private void FormSecondGame_FormClosing(object sender, FormClosingEventArgs e) {
            saveRecord(count);
        }
    }
}
