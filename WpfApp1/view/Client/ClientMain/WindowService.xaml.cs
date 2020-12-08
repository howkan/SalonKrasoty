using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WpfApp1.view;
using System.Data.Sql;
using System.Data.SqlClient;
using DbConnect;
using System.Data;
using WpfApp1.view.Client.Buy;
using System.Diagnostics;
namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class WindowService : Window
    {
        public string logins;
        public ObservableCollection<Service> Services { get; set; }

        public WindowService(string login)
        {
            
            InitializeComponent();

            logins = login;
            Console.WriteLine(logins);
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Service", db.getConnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Services = new ObservableCollection<Service> { };
            //Service ser = new Service();
            while (reader.Read())
            {

                Services.Add(new Service { ID = int.Parse(reader[0].ToString()), 
                    Title = reader[1].ToString(),
                    Cost = float.Parse(reader[2].ToString()).ToString() + " рублей за " + float.Parse(reader[3].ToString()).ToString() + " мин",
                    Costedit = float.Parse(reader[2].ToString()).ToString(),
                    DurationInSeconds = int.Parse(reader[3].ToString()).ToString(),
                    Discount = float.Parse(reader[4].ToString()).ToString() + "% скидка", 
                    DiscountEdit = float.Parse(reader[4].ToString()).ToString(),
                    MainImagePath = reader[5].ToString()
                });
            }
            phonesList.ItemsSource = Services;
        }


        public float converttime(float time)
        {
            float y = time / 60;
            float z = y / 60;
            time = y * 60;
            time = z / 3600;
            y = z * 60;
            return y;
        }

        private void phonesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service p = (Service)phonesList.SelectedItem;
        }

        private void Click_Tovari(object sender, RoutedEventArgs e)
        {
            WindowTovari windowTovari = new WindowTovari(logins);
            windowTovari.Show();
            this.Close();
        }

        private void go_order(object sender, RoutedEventArgs e)
        {
            if (phonesList.SelectedIndex != -1)
            {
                Order order = new Order(phonesList.SelectedItem as Service, logins);
                this.Hide();
                order.Show();
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Error");
            }
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_Akk(object sender, RoutedEventArgs e)
        {
            WindowClient windowClient = new WindowClient(logins);
            windowClient.Show();
            this.Close();
        }

        private void Click_change(object sender, RoutedEventArgs e)
        { 
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}
