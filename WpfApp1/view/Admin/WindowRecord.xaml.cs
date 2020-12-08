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
using WpfApp1.view.Admin;
namespace WpfApp1.view.Admin
{
    /// <summary>
    /// Логика взаимодействия для WindowRecord.xaml
    /// </summary>
    public partial class WindowRecord : Window
    {
        Service s;
        public ObservableCollection<Service> Services { get; set; }
        public WindowRecord()
        {
            InitializeComponent();
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT * FROM OrderService", db.getConnection());
            db.openConnection();
            SqlDataReader reader = command.ExecuteReader();

            Services = new ObservableCollection<Service> { };
            while (reader.Read())
            {
                Services.Add(new Service
                {
                    ID = int.Parse(reader[0].ToString()),
                    Title = reader[1].ToString(),
                    Cost = "Итоговая стоимость - " + float.Parse(reader[3].ToString()).ToString() + " рублей",
                    Costedit = float.Parse(reader[3].ToString()).ToString(),
                    DurationInSeconds = "Оплаченное время работы мастера - " + int.Parse(reader[4].ToString()).ToString() + " мин",
                    DurationInSecondsEdit = int.Parse(reader[4].ToString()).ToString(),
                    Discount = "Скидка - " + float.Parse(reader[5].ToString()).ToString() + "%",
                    DiscountEdit = float.Parse(reader[5].ToString()).ToString(),
                    OrderDate = "Дата заказа - " + reader[6].ToString(),
                    OrderDateEdit = reader[6].ToString(),
                    ReservDay = "Дата записи - " + reader[7].ToString(),
                    MainImagePath = reader[9].ToString()
                });

            }
            recordlist.ItemsSource = Services;
        }

        private void recordlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service p = (Service)recordlist.SelectedItem;
        }

        private void Click_Change(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void delete_record(object sender, RoutedEventArgs e)
        {
            s = recordlist.SelectedItem as Service;
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить данную запись?", "Delete", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                DB db = new DB();
                SqlCommand command = new SqlCommand("DELETE FROM OrderService WHERE ID = @id", db.getConnection());
                command.Parameters.AddWithValue("@id", s.ID);
                db.openConnection();
                if (command.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Услуга успешно удалена!", "Ok");
                    WindowRecord record = new WindowRecord();
                    this.Close();
                    record.Show();
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

        private void edit_record(object sender, RoutedEventArgs e)
        {
            if (recordlist.SelectedIndex != -1) // Магия / не трогать
            {
                EditRecord ed = new EditRecord(recordlist.SelectedItem as Service);
                ed.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Выберите запись!", "Error");
            }
        }

        private void go_admzap(object sender, RoutedEventArgs e)
        {
            WindowAdmin adm = new WindowAdmin();
            this.Close();
            adm.Show();
        }
    }
}
