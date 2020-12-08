using DbConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using WpfApp1.Model;
using Microsoft.Win32;
namespace WpfApp1.view.Admin.Buttons
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public string imagepath;
        public Add()
        {
            InitializeComponent();


        }

        private void go_add(object sender, RoutedEventArgs e)
        {
            string title = title_box.Text.Trim();
            string cost = cost_box.Text.Trim();
            string time = time_box.Text.Trim();
            string skidka = skidka_box.Text.Trim();
            //GenderCode



            DB db = new DB();
            SqlCommand command = new SqlCommand("INSERT INTO Service ( Name, Price, Time, Discount, ImagePath)"
                + "VALUES (@title, @cost, @time,  @skidka, @path)", db.getConnection());
            command.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
            command.Parameters.Add("@cost", SqlDbType.VarChar).Value = cost;
            command.Parameters.Add("@time", SqlDbType.Int).Value = time;
            command.Parameters.Add("@skidka", SqlDbType.VarChar).Value = skidka;
            command.Parameters.Add("@path", SqlDbType.VarChar).Value = imagepath;
            db.openConnection();
            if (imagepath.ToString() != "")
            {
                if (CheckTextBoxes())
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        WindowAdminService winadm = new WindowAdminService();
                        MessageBox.Show("Услуга успешно добавлена!");
                        this.Close();
                        winadm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Проверьте правильность введеных данных");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните пустые поля");
                }
            }
            else
            {
                MessageBox.Show("Выберите изображение");
            }
        }





        public Boolean CheckTextBoxes()
        {
            String Title = title_box.Text;
            String Cost = cost_box.Text;
            String DurationInSeconds = time_box.Text;
            String Discount = skidka_box.Text;


            if (Title == String.Empty || Cost == String.Empty || DurationInSeconds == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void select_image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                imagepath = openFileDialog.FileName;
            }
            Console.WriteLine("path" + imagepath);
        }

        private void go_back(object sender, RoutedEventArgs e)
        {
            WindowAdminService winadm = new WindowAdminService();
            this.Close();
            winadm.Show();
        }
    }
}
