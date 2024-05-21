using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appFrench
{
    public partial class ProfileForm : Form
    {
        internal static List<(string Category, bool IsChecked)> checkedCategoriesList = new List<(string, bool)>();
        int id = 0;
        public ProfileForm(int id_us)
        {
            id = id_us;
            InitializeComponent();

            // Проверка на существование файла и его не пустоты находится в методе ListExtensions.LoadCheckedListBoxState()
            checkedCategoriesList = ListExtensions.LoadCheckedListBoxState(ListExtensions.getFilePath());

            // Заполнение элемента CheckedListBox с помощью списка
            foreach (var category in checkedCategoriesList)
            {
                checkedListBox.Items.Add(category.Category);
                int index = checkedListBox.Items.IndexOf(category.Category);
                if (category.IsChecked)
                {
                    checkedListBox.SetItemChecked(index, true);
                }
            }
            // Обработчик события нажатия для CheckedListBox
            // Нужен для постоянного обновления списка
            // Чтобы в любой момент закрыть окно и сохранить состояния
            checkedListBox.ItemCheck += CheckedListBox_ItemCheck;

            setRecords();
        }
        void setRecords() { 
        Db db = new Db();
            SqlConnection connection = db.getConnection();
            using (connection)
            {
                connection.Open();
                string quary = "SELECT Score FROM Games WHERE UserID = @id AND GameType = 1";
                SqlCommand command = new SqlCommand(quary, connection);
                command.Parameters.Add("@id", id);
                labelFirstRecord.Text = command.ExecuteScalar().ToString();
                string quary2 = "SELECT Score FROM Games WHERE UserID = @id AND GameType = 2";
                SqlCommand command2 = new SqlCommand(quary2, connection);
                command2.Parameters.Add("@id", id);
                labelFirstSecond.Text = command2.ExecuteScalar().ToString();
            }
        }
        static void CheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            CheckedListBox checkedListBox = (CheckedListBox)sender;
            string category = checkedListBox.Items[e.Index].ToString();
            bool isChecked = e.NewValue == CheckState.Checked;

            // Ищем категорию в списке и обновляем её состояние выбранности
            for (int i = 0; i < checkedCategoriesList.Count; i++)
            {
                if (checkedCategoriesList[i].Category == category)
                {
                    checkedCategoriesList[i] = (category, isChecked);
                    ListExtensions.SaveCheckedListBoxState(ListExtensions.getFilePath(), checkedCategoriesList);
                    break;
                }
            }
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
