using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace appFrench
{
    public partial class ListWordForm : Form
    {

        public ListWordForm()
        {
            InitializeComponent();
            LabelChanger listLabel = new LabelChanger("Название список");
            this.Controls.Add(listLabel);
            // Перемещаем элемент на передний план
            listLabel.BringToFront();
            listLabel.BackColor = Color.Transparent;
            FillListBox();
        }
        private void FillListBox()
        {
            Db db = new Db();
            try
            {
                string query = "SELECT Word, Translation FROM Words WHERE IsCorrect = 1 AND LanguagePair != 'RU-FR'";


                List<string> selectedCategories = ListExtensions.LoadCheckedListBoxState(ListExtensions.getFilePath())
                    .Where(category => category.IsChecked)
                    .Select(category => category.Category)
                    .ToList();

                // Создаем базовый запрос

                // Если выбраны какие-то категории, добавляем соответствующее условие к запросу
                if (selectedCategories.Any())
                {
                    // Формируем строку с условиями для выбранных категорий
                    string categoriesCondition = string.Join(" OR ", selectedCategories.Select(category => $"Category = '{category}'"));

                    // Добавляем условие к базовому запросу
                    query += $" AND ({categoriesCondition})";
                }


                SqlConnection connection = db.getConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    wordsList.Items.Clear();
                    while (reader.Read())
                    {
                        string word = reader["Word"].ToString();
                        string translation = reader["Translation"].ToString();

                        wordsList.Items.Add($"Слово: {word}");
                        wordsList.Items.Add($"      Перевод: {translation}");
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
