using DbConnect;
using System;
using System.Collections.Generic;
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
using Microsoft.Win32;
using WpfApp1.Model;
using System.Data;

namespace WpfApp1.view.Admin.Buttons
{
    /// <summary>
    /// Логика взаимодействия для AddProducts.xaml
    /// </summary>
    public partial class AddProducts : Window
    {
        public string imagepath;
        public AddProducts()
        {
            InitializeComponent();
        }

        private void go_add(object sender, RoutedEventArgs e)
        {
            if (imagepath != null)
            {
                string title = title_box.Text.Trim();
                string description = Desription_box.Text.Trim();
                string price = cost_box.Text.Trim();
                //GenderCode



                DB db = new DB();
                SqlCommand command = new SqlCommand("INSERT INTO Product ( Name, Price, Description, ImagePath)"
                    + "VALUES (@title, @cost, @Description, @imagepath)", db.getConnection());
                command.Parameters.Add("@title", SqlDbType.VarChar).Value = title;
                command.Parameters.Add("@Description", SqlDbType.VarChar).Value = description;
                command.Parameters.Add("@cost", SqlDbType.VarChar).Value = price;
                command.Parameters.Add("@imagepath", SqlDbType.VarChar).Value = imagepath;
                db.openConnection();

                if (CheckTextBoxes())
                {
                    if (command.ExecuteNonQuery() == 1)
                    {
                        WindowProducts winproducts = new WindowProducts();
                        MessageBox.Show("Продукт успешно добавлена!");
                        this.Close();
                        winproducts.Show();
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
                MessageBox.Show("Выберите изображение!");
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

        public Boolean CheckTextBoxes()
        {
            String Title = title_box.Text;
            String Cost = cost_box.Text;
            String Description = Desription_box.Text;


            if (Title == String.Empty || Cost == String.Empty || Description == String.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void go_back(object sender, RoutedEventArgs e)
        {
            WindowProducts winproducts = new WindowProducts();
            this.Close();
            winproducts.Show();
        }
    }
}
