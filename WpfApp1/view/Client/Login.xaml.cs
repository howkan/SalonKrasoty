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
using WpfApp1.view;
using System.Data.Sql;
using System.Data.SqlClient;
using DbConnect;
using System.Data;
using WpfApp1.Model;
using System.Collections.ObjectModel;
namespace WpfApp1
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string login = Email_box.Text.Trim();
            string password = password_box.Password.Trim();

            try
            {
                DB db = new DB();
                DataTable table = new DataTable();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Email= @uL AND Password = @uP", db.getConnection());
                db.openConnection();

                command.Parameters.Add("@uL", SqlDbType.VarChar).Value = login;
                command.Parameters.Add("@uP", SqlDbType.VarChar).Value = password;
                adapter.SelectCommand = command;
                adapter.Fill(table);

                if (table.Rows.Count > 0)
                {
                    if (CheckRole())
                    {

                        WindowAdmin windowCompany = new WindowAdmin();
                        this.Close();
                        windowCompany.Show();
                    }
                    else
                    {

                        WindowClient client = new WindowClient(login);
                        this.Close();

                        client.Show();

                    }
                }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
            catch (SqlException)
            {
                MessageBox.Show("Отсутствует подключение с базой данных");
            }
        }


        private bool CheckRole()
        {
            DB db = new DB();

            String loginUser = Email_box.Text;

            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE Email = @email and Role = 1", db.getConnection());
            command.Parameters.Add("@email", SqlDbType.VarChar).Value = loginUser;
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            this.Close();
        }


    }
}
