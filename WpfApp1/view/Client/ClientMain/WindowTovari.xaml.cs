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
using WpfApp1.Model;
using System.Data.Sql;
using System.Data.SqlClient;
using DbConnect;
using System.Data;
using WpfApp1.view.Client.Buy;
using System.Diagnostics;
using System.Collections.ObjectModel;
using WpfApp1.view.Client.Buttons;

namespace WpfApp1.view
{
    /// <summary>
    /// Логика взаимодействия для WindowTovari.xaml
    /// </summary>
    public partial class WindowTovari : Window
    {
        public string logins;
        public ObservableCollection<Tovari> Tovaris { get; set; }
        public WindowTovari(string login)
        {
            InitializeComponent();
            logins = login;
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM Product", db.getConnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Tovaris = new ObservableCollection<Tovari> { };
            //Service ser = new Service();
            while (reader.Read())
            {
                // ser.ID = int.Parse(reader[0].ToString());
                //ser.Title = reader[1].ToString();
                //ser.Cost = float.Parse(reader[2].ToString());
                //ser.DurationInSeconds = int.Parse(reader[3].ToString());
                //ser.Description = reader[4].ToString();
                //ser.Discount = float.Parse(reader[5].ToString());
                //ser.MainImagePath = reader[6].ToString();
                //float cost = float.Parse(reader[2].ToString());
                //string costs = cost.ToString() + " руб";
                Tovaris.Add(new Tovari
                {
                    ID = int.Parse(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    Cost = "Цена - " + float.Parse(reader[2].ToString()).ToString() + " рублей",
                    CostEdit = float.Parse(reader[2].ToString()).ToString(),
                    DescriptionEdit = reader[3].ToString(),
                    Description = "Описание - " + reader[3].ToString(),
                    MainImagePath = reader[4].ToString(),
                });
            }
            productList.ItemsSource = Tovaris;
    }

        private void go_service(object sender, RoutedEventArgs e)
        {
            WindowService main = new WindowService(logins);
            main.Show();
            this.Close();
        }


        private void go_exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    

        private void go_main(object sender, RoutedEventArgs e)
        {
            WindowClient windowClient = new WindowClient(logins);
            windowClient.Show();
            this.Close();
        }

        private void go_change(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void go_order(object sender, RoutedEventArgs e)
        {
            if (productList.SelectedIndex != -1)
            {
                OrderProducts order = new OrderProducts(productList.SelectedItem as Tovari, logins);
                this.Hide();
                order.Show();
            }
            else
            {
                MessageBox.Show("Выберите продукт!", "Error");
            }
        }

        private void productList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
