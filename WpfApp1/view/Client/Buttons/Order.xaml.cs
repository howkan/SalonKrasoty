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

namespace WpfApp1.view.Client.Buy
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        Service Service;
        public string logins;
        public Order(Service init, string login)
        {
            InitializeComponent();
            logins = login;
            this.Service = init;
            title_box.Text = init.Title;
            time_box.Text = init.DurationInSeconds;
            cost_box.Text = init.Costedit;
            skidka_box.Text = init.DiscountEdit.ToString() + "%";

            if (double.Parse(init.DiscountEdit) != 0)
            {
                double percent = double.Parse(init.DiscountEdit);
                double number = double.Parse(cost_box.Text);
                int result = int.Parse(cost_box.Text) - (int)Math.Round(number * (percent / 100));
                topprice_box.Text = result.ToString();
            }
            else if (double.Parse(init.DiscountEdit) == 0)
            {
                skidka_box.Text = "Отсутствует";
                topprice_box.Text = init.Costedit;
            }

        }
        private void go_order(object sender, RoutedEventArgs e)
        {
            DateTime curDate = DateTime.Now;
            DateTime? selectedDate = calendar_date.SelectedDate;
            if (selectedDate > DateTime.Now)
            {
                
                DB db = new DB();
                SqlCommand command = new SqlCommand("INSERT INTO OrderService ( Name, Price, TotalPrice, Time, Discount, OrderDate, DayReserv, Login, ImagePath)"
                    + "VALUES (@title,  @cost, @total, @time, @skidka, @orderdate, @buydate, @login, @image)", db.getConnection());
                command.Parameters.AddWithValue("@title", title_box.Text);
                command.Parameters.AddWithValue("@cost", cost_box.Text);
                command.Parameters.AddWithValue("@total", topprice_box.Text);
                command.Parameters.AddWithValue("@time", time_box.Text);
                command.Parameters.AddWithValue("@skidka", Service.DiscountEdit);
                command.Parameters.AddWithValue("@orderdate", curDate);
                command.Parameters.AddWithValue("@buydate", selectedDate.Value.Date.ToShortDateString());
                command.Parameters.AddWithValue("@login", logins);
                command.Parameters.AddWithValue("@image", Service.MainImagePath);


                db.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    WindowService winadm = new WindowService(logins);
                    MessageBox.Show("Услуга успешно зарезервирована!");
                    this.Close();
                    winadm.Show();
                }
            }
            else
            {
                MessageBox.Show("Выберите правильную дату!", "Error");
            }
        }


        private void go_back(object sender, RoutedEventArgs e)
        {
            WindowService winadm = new WindowService(logins);
            this.Close();
            winadm.Show();
        }
    }

}
