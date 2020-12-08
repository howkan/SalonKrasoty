using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DbConnect;
using WpfApp1.view;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button_reg(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
            this.Close();
        }

        private void Click_Regiter_Click(object sender, RoutedEventArgs e)
        {
            // string loginreg = reg_login_box.Text.Trim();
            string name = name_box.Text.Trim();
            string lastname = family_box.Text.Trim();
            string patron = patron_box.Text.Trim();
            string phone = phone_box.Text.Trim();
            string birthday = birthaday_box.Text.Trim();
            DateTime curDate = DateTime.Now;
            string email = email_box.Text.Trim();
            string passwordag = passwordbox_one.Password.Trim();
            string passwordag2 = passwordbox_two.Password.Trim();
            //GenderCode
            //try
            //{
                DB db = new DB();
                db.openConnection();
                SqlCommand command = new SqlCommand("INSERT INTO Client ( FirstName, LastName, Patronymic, Birthday, RegistrationDate, Email, Phone, GenderCode, Password, Role)"
                    + "VALUES (@firstname, @lastname, @patronymic, @birthday, @regdata, @email, @phone, @gencode, @passw, @role)", db.getConnection());
                command.Parameters.Add("@firstname", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@lastname", SqlDbType.VarChar).Value = lastname;
                command.Parameters.Add("@patronymic", SqlDbType.VarChar).Value = patron;
                command.Parameters.Add("@birthday", SqlDbType.VarChar).Value = birthday;
                command.Parameters.Add("@regdata", SqlDbType.VarChar).Value = curDate;
                command.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                command.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
                command.Parameters.Add("@gencode", SqlDbType.NChar).Value = gender_code.SelectedIndex;
                command.Parameters.Add("@passw", SqlDbType.VarChar).Value = passwordag;
                command.Parameters.Add("@role", SqlDbType.VarChar).Value = 0;

                if (name.Length >= 3)
                {
                    if (lastname.Length > 2)
                    {

                        if (phone.Length >= 11)
                        {

                            if (birthday != "")
                            {
                                if (email.Length > 4)
                                {
                                    if (passwordag.Length > 5 && passwordag2.Length > 5)
                                    {
                                        if (passwordag == passwordag2)
                                        {
                                            if (command.ExecuteNonQuery() == 1)
                                            {
                                                Login login = new Login();
                                                this.Close();
                                                login.Show();


                                            }
                                            else
                                            {
                                                MessageBox.Show("Проверьте правильность введеных данных");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Пароли не совпадают");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Введите пароль и повторный пароль\n Не меньше 5 символов");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Почта не может быть меньше 4 символов");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите дату рождения");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Номер телефона не может быть меньше 11 символов");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заполните пустое поле");
                    }
                }
                else
                {
                    MessageBox.Show("Имя не может быть меньше 3 символов");
                }



            //}
            //catch (SqlException)
            //{
            //    MessageBox.Show("Отсутствует подключение с базой данных");
            //}


        }
    }
}

