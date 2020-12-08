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
using System.Data.Sql;
using System.Data.SqlClient;
using DbConnect;
using System.Data;
using System.Collections.ObjectModel;
using WpfApp1.Model;
using WpfApp1.view.Admin.Buttons;

namespace WpfApp1.view.Client.Buttons
{
    /// <summary>
    /// Логика взаимодействия для OrderProducts.xaml
    /// </summary>
    public partial class OrderProducts : Window
    {
        Tovari Tovar;
        public string logins;
        public OrderProducts(Tovari init, string login)
        {
            InitializeComponent();
            logins = login;
            this.Tovar = init;
            title_box.Text = init.Title;
            desk_box.Text = init.DescriptionEdit;
            price_box.Text = init.CostEdit;
            topprice_box.Text = init.CostEdit;

        }
        private void go_order(object sender, RoutedEventArgs e)
        {

            DB db = new DB();
            SqlCommand command = new SqlCommand("INSERT INTO OrderProduct ( Name, Price, TopPrice, Description, Login, ImagePath)"
                + "VALUES (@title, @price, @topprice, @desk, @login, @image)", db.getConnection());
            command.Parameters.AddWithValue("@title", title_box.Text);
            command.Parameters.AddWithValue("@price", price_box.Text);
            command.Parameters.AddWithValue("@topprice", topprice_box.Text);
            command.Parameters.AddWithValue("@desk", desk_box.Text);
            command.Parameters.AddWithValue("@login", logins);
            command.Parameters.AddWithValue("@image", Tovar.MainImagePath);


            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                WindowClient winadm = new WindowClient(logins);
                MessageBox.Show("Товар успешно зарезервирована!");
                this.Close();
                winadm.Show();
            }
        }




        private void go_back(object sender, RoutedEventArgs e)
        {
            WindowClient winadm = new WindowClient(logins);
            this.Close();
            winadm.Show();
        }
    }
}
