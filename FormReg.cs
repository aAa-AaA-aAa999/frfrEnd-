using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace appFrench
{
    public partial class FormReg : Form
    {
        public FormReg()
        {
            InitializeComponent();
            //слово Регистрация
            LabelChanger reg = new LabelChanger("Регистрация");
            this.Controls.Add(reg);
            // Перемещаем элемент на передний план
            reg.BringToFront();
            Label[] labels = this.Controls.OfType<Label>().ToArray();
            LabelChanger.deleteBackgroundLabel(labels);//передаёт массив labels чтобы сделать фон прозрачным
        }

        private void regButton_Click(object sender, EventArgs e)
        {
           Boolean regAuth =  RegisterUser(textBoxLogin.Text, textBoxPassword.Text, "french", textBoxMail.Text, textBoxName.Text);
            if (regAuth)
            {
                MessageBox.Show("Регистрация прошла успешна");

            }
            else
            {
                MessageBox.Show("Вас не удалось зарегистрировать");
            }

        }
        public bool RegisterUser(string username, string password,string learn, string mail, string name) 
        {
            Db db = new Db();

            try
            {
                if (textBoxName.Text != "" & textBoxLogin.Text != "" & textBoxMail.Text != "" & textBoxPassword.Text != "" & textBoxOneMore.Text != "")//проверка что поля заполнены
                {
                    db.openConnection();
                    string queryCheck = "Select Count(1) UserName From Users Where UserName = @inputName";

                    SqlCommand commandCheck = new SqlCommand(queryCheck, db.getConnection());
                    commandCheck.Parameters.AddWithValue("@inputName", username);
                    int existUsers = (int)commandCheck.ExecuteScalar();

                    if (existUsers == 1) //проверка если уже есть пользователь с таким логином
                    {
                        MessageBox.Show("Пользователь с таким логином уже существует.\nВведите, пожалуйста, другой логин:)");
                        return false;

                    }
                    else
                    {
                        if (IsValidPassword(textBoxPassword.Text) )
                        {
                            if (textBoxPassword.Text.Equals(textBoxOneMore.Text)){
                                SqlTransaction transactionForCreate = db.getConnection().BeginTransaction();


                                string query = "INSERT INTO Users (PhraseID, ImageID, Username, Password, LanguageLearning, Mail, UserFirstName) VALUES (@PhraseID, @ImageID, @Username, @Password, @learn, @Mail, @Name) " +
                                    "DECLARE @userID INT;" +
                                    "SET @userID = SCOPE_IDENTITY()" +
                                    "INSERT INTO UserProgress (UserID, IsLearned, SpeedGameRecord, SlowGameRecord) VALUES (@userID, @bit , 0, 0) " +
                                    "INSERT INTO Games (GameType, Score, UserID, PlayedOn) VALUES (1, 0, @userID, @Date) " +
                                    "INSERT INTO Games (GameType, Score, UserID, PlayedOn) VALUES (2, 0, @userID, @Date) ";

                                SqlCommand cmd = new SqlCommand(query, db.getConnection(), transactionForCreate);

                                cmd.Parameters.AddWithValue("@PhraseID", 1);
                                cmd.Parameters.AddWithValue("@ImageID", 1);
                                cmd.Parameters.AddWithValue("@Username", username);
                                cmd.Parameters.AddWithValue("@Password", password);
                                cmd.Parameters.AddWithValue("@learn", learn);
                                cmd.Parameters.AddWithValue("@Mail", mail);
                                cmd.Parameters.AddWithValue("@Name", name);
                                cmd.Parameters.AddWithValue("@bit", 1);
                                cmd.Parameters.AddWithValue("@Date", DateTime.Now);


                                int rowsAffected = cmd.ExecuteNonQuery();


                                if (rowsAffected > 0)
                                {
                                    //фиксируем транзакцию если всё успешно
                                    transactionForCreate.Commit();
                                    return true; // Регистрация удалась

                                }
                                else
                                {

                                    transactionForCreate.Rollback();
                                    return false; // Регистрация не удалась
                                }
                            }
                            else {
                                MessageBox.Show("Ваши пароли не совпадают");
                                return false;
                            }
                        }
                        else {
                            MessageBox.Show("В пароле должно быть не менее 6 символов. Как минимум 1 буква и 1 цифра.");
                            return false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введены не все данные.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                db.closedConnection();
            }
            return false; // Ошибка при регистрации
        }
        private static bool IsValidPassword(string password)
        {
            // Регулярное выражение для проверки пароля
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$";
            //^: начало строки.
            //(?=.* [A - Za - z]): проверка наличия хотя бы одной буквы.
            //(?= ...) — это позитивная проверка на наличие. [A - Za - z] означает любую букву, как заглавную, так и строчную.
            //(?=.*\d): проверка наличия хотя бы одной цифры. \d соответствует любой цифре.
            //[A-Za - z\d]{ 6,}: проверка, что строка состоит только из букв и цифр и имеет длину не менее 6 символов. { 6,}
            // означает "не менее 6 символов".
            //$: конец строки.
            Regex regex = new Regex(pattern);
            return regex.IsMatch(password);
        }
    }
}
