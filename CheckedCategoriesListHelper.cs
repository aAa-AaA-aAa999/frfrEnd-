using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace appFrench
{
    public static class CheckedCategoriesHelper
    {
        //cтрока пути. Состоит из комбинации:
        //первая часть:
        //Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName
        //нужна для обращения к общему каталогу папки, в любом другом случае 
        //путь обращается к bin\debug
        // т.е. AppDomain.CurrentDomain.BaseDirectory получаем bin\debug
        // GetParent() принимает путь и возвращает
        // родительскую директорию этого пути
        // далее .Parent.Parent чтобы подняться от bin\debug
        // .FullName-свойство возвращает полный путь к папке в виде строки.
        // ну и вторая часть - обращение к нужным папкам
        private static string filePath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "Resources", "categoryInfo", "checkedCategoriesList.xml");

        //метод десериализации, загрузки состояния объекта checkedCategoriesList
        public static List<(string Category, bool IsChecked)> LoadCheckedListBoxState()
        {
            if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
            {
                return GetUniqueCategoriesFromDatabase();
            }

            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<(string Category, bool IsChecked)>));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (List<(string Category, bool IsChecked)>)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load state.");
                MessageBox.Show("Error: " + ex.Message);
                return new List<(string Category, bool IsChecked)>();
            }
        }


        //метод сериализации, сохранения текущего состояния объекта checkedCategoriesList
        public static void SaveCheckedListBoxState(List<(string Category, bool IsChecked)> checkedCategoriesList)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<(string Category, bool IsChecked)>));
                using (FileStream fs = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(fs, checkedCategoriesList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save state.");
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        // метод для загрузки категорий из базы данных
        private static List<(string Category, bool IsChecked)> GetUniqueCategoriesFromDatabase()
        {
            List<(string Category, bool IsChecked)> categories = new List<(string, bool)>();

            Db db = new Db();
            try
            {
                string query = "SELECT DISTINCT Category FROM Words;";
                SqlConnection connection = db.getConnection();
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add((reader["Category"].ToString(), false));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not retrieved.");
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                db.closedConnection();
            }
            return categories;
        }
    }
}
