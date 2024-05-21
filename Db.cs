using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appFrench
{
    internal class Db
    {
            SqlConnection connection = new SqlConnection("Server =LAPTOP143; Database=franchLangBase;Trusted_Connection=True;");
            public void openConnection()
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                    connection.Open();
            }

            public void closedConnection()
            {
                if (connection.State == System.Data.ConnectionState.Open)
                    connection.Close();
            }

            public SqlConnection getConnection()
            {
                return connection;
            }
        public static (string Word, List<(string Option, bool IsCorrect)> Options, string CorrectOption) GetWordAndOptions(string lastWordFromGame)
        {
            Db db = new Db();
            string correctWord = "";
            string correctOption = "";
            string lastWord = lastWordFromGame;
            int wordId;
            var options = new List<(string Option, bool IsCorrect)>();

            using (var connection = db.getConnection())
            {
                connection.Open();

                string query = "SELECT TOP 1 WordID, Word FROM Words WHERE LanguagePair = 'FR-RU' AND Word <> @lastWord ";
                List<string> selectedCategories = ListExtensions.LoadCheckedListBoxState(ListExtensions.getFilePath())
                    .Where(category => category.IsChecked)
                    .Select(category => category.Category)
                    .ToList();
                SqlCommand command;

                //Try если в файле категорий было что-то изменено и программа сработает некорректно
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
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@lastWord", lastWord);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("No words found");
                        }
                        correctWord = reader["Word"].ToString();
                        wordId = (int)reader["WordID"];
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Возникли ошибки, возможно файл был изменён.\nВ данной игре будут применены все категории. \n\nДля настройки категорий Вы можете перейти в профиль;)");

                    query = "SELECT TOP 1 WordID, Word FROM Words WHERE LanguagePair = 'FR-RU' AND Word <> @lastWord ORDER BY NEWID();";

                    // Получаем случайное слово на французском
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@lastWord", lastWord);
                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                        {
                            throw new Exception("No words found");
                        }
                        correctWord = reader["Word"].ToString();
                        wordId = (int)reader["WordID"];
                    }

                    // Перезаписываем список selectedCategories для дальнейшей работы метода
                    selectedCategories = ListExtensions.GetUniqueCategoriesFromDatabase()
                        .Where(category => category.IsChecked)
                        .Select(category => category.Category)
                        .ToList();

                    // Перезаписываем файл для следующей выборки слов
                    ListExtensions.SaveCheckedListBoxState(ListExtensions.getFilePath(), ListExtensions.GetUniqueCategoriesFromDatabase());
                }
                
                    // Получаем правильный перевод для слова
                command = new SqlCommand("SELECT Translation FROM Words WHERE WordID = @WordID AND IsCorrect = 1;", connection);
                command.Parameters.AddWithValue("@WordID", wordId);
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        throw new Exception("No correct translation found");
                    }
                    correctOption = reader["Translation"].ToString();
                    // Добавляем правильный перевод с меткой корректности
                    options.Add((correctOption, true));
                }

                string queryWrongTranslations = "SELECT TOP 2 Translation " +
                "FROM( SELECT DISTINCT Translation FROM Words" +
                " WHERE WordID <> @WordID AND IsCorrect = 0 AND LanguagePair = 'RU-FR' " +
                "AND Translation <> @correctOption ";
                if (selectedCategories.Any())
                {
                    // Формируем строку с условиями для выбранных категорий
                    string categoriesCondition = string.Join(" OR ", selectedCategories.Select(category => $"Category = '{category}'"));

                    // Добавляем условие к базовому запросу
                    queryWrongTranslations += $" AND ({categoriesCondition})";
                }

                queryWrongTranslations += ") AS UniqueTranslations ORDER BY NEWID(); ";

                // Получаем два неправильных перевода
                command = new SqlCommand(queryWrongTranslations, connection);

                command.Parameters.AddWithValue("@WordID", wordId);
                command.Parameters.AddWithValue("@correctOption", correctOption);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        options.Add((reader["Translation"].ToString(), false));
                    }
                }
                
                // Перемешиваем варианты ответа
                Shuffle(options);
            }

            return (correctWord, options, correctOption);
        }
        private static void Shuffle(List<(string Option, bool IsCorrect)> options)
        {
            var rng = new Random();
            int n = options.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = options[k];
                options[k] = options[n];
                options[n] = value;
            }
        }
    }
}
