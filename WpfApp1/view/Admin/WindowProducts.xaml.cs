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
using System.Data.SqlClient;
using DbConnect;
using System.Data;
using System.Collections.ObjectModel;
using WpfApp1.Model;
using WpfApp1.view.Admin.Buttons;
using WpfApp1.view.Admin;

namespace WpfApp1.view.Admin
{
    /// <summary>
    /// Логика взаимодействия для WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        Service s;
        public ObservableCollection<Service> Services { get; set; }
        public WindowProducts()
        {
            InitializeComponent();
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Product", db.getConnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Services = new ObservableCollection<Service> { };
            while (reader.Read())
            {
                Services.Add(new Service
                {
                    ID = int.Parse(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    Cost = "Цена - " + float.Parse(reader[2].ToString()).ToString() + " рублей",
                    Costedit = float.Parse(reader[2].ToString()).ToString(),
                    DescriptionEdit = reader[3].ToString(),
                    Description = "Описание - " + reader[3].ToString(),
                    MainImagePath = reader[4].ToString()
                });

            }
            productlist.ItemsSource = Services;
        }


        private void go_edit(object sender, RoutedEventArgs e)
        {
            if (productlist.SelectedIndex != -1) // Магия / не трогать
            {
                EditProducts ed = new EditProducts(productlist.SelectedItem as Service);
                ed.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Error");
            }
        }

        private void go_delete(object sender, RoutedEventArgs e)
        {
            s = productlist.SelectedItem as Service;
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную услугу?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DB db = new DB();
                SqlCommand command = new SqlCommand("DELETE FROM Product WHERE ID = @id", db.getConnection());
                command.Parameters.AddWithValue("@id", s.ID);
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Услуга успешно удалена!", "Ok");
                    WindowProducts adm = new WindowProducts();
                    this.Close();
                    adm.Show();
                }
                else
                {
                    MessageBox.Show("Выберите услугу");
                }
            }
            else
            {

            }
        }

        private void go_add(object sender, RoutedEventArgs e)
        {
            AddProducts add = new AddProducts();
            this.Close();
            add.Show();


        }
        private void productlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service p = (Service)productlist.SelectedItem;
        }

        private void go_main(object sender, RoutedEventArgs e)
        {
            WindowAdmin rec = new WindowAdmin();
            rec.Show();
            this.Close();
        }
        private void go_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void go_change(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
