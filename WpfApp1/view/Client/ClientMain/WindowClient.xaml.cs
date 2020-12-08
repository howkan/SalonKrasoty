using DbConnect;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1.view
{
    /// <summary>
    /// Логика взаимодействия для WindowClient.xaml
    /// </summary>
    public partial class WindowClient : Window
    {
        public ObservableCollection<Service> Services { get; set; }
        public string logins;
        public WindowClient( string login)
        {
            InitializeComponent();
            logins = login;
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM OrderService Where Login = @login", db.getConnection());
            command.Parameters.AddWithValue("@login", logins);
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Services = new ObservableCollection<Service> { };
            //Service ser = new Service();
            while (reader.Read())
            {
                Services.Add(new Service
                {
                    ID = int.Parse(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    DurationInSecondsEdit =  "Время работы мастера " + float.Parse(reader[4].ToString()).ToString() + " мин",
                    OrderDate = "Дата записи: " + reader[7].ToString(),
                    Discount = "Цена с учетом скидок: " + float.Parse(reader[3].ToString()).ToString() + " руб",
                    MainImagePath = reader[9].ToString()
                });
            }
            client_ak.ItemsSource = Services;
        }

       

        private void Click_Tovari(object sender, RoutedEventArgs e)
        {
            WindowTovari windowTovari = new WindowTovari(logins);
            windowTovari.Show();
            this.Close();
        }

        private void Click_Sirvice(object sender, RoutedEventArgs e)
        {
            WpfApp1.WindowService main = new WpfApp1.WindowService(logins);
            main.Show();
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
