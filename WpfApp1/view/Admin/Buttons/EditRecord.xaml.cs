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
using WpfApp1.Model;
using DbConnect;
using System.Data.SqlClient;
namespace WpfApp1.view.Admin.Buttons
{
    /// <summary>
    /// Логика взаимодействия для EditRecord.xaml
    /// </summary>
    public partial class EditRecord : Window
    {
        Service Service;
        public EditRecord(Service init)
        {
            InitializeComponent();
            this.Service = init;
            title_box.Text = init.Title;
            cost_box.Text = init.Costedit;
            time_box.Text = init.DurationInSecondsEdit;
            skidka_box.Text = init.DiscountEdit;
            order_date.Text = init.OrderDateEdit;
            //calendar_record.SelectedDate = DateTime.Parse(init.ReservDay);

        }

        private void record_save(object sender, RoutedEventArgs e)
        {
            DateTime? selectedDate = calendar_record.SelectedDate;
            if (selectedDate > DateTime.Now)
            {

                DB db = new DB();
                SqlCommand command = new SqlCommand("UPDATE OrderService SET DayReserv = @day Where ID = @ids", db.getConnection());
                command.Parameters.AddWithValue("@ids", Service.ID);
                command.Parameters.AddWithValue("@day", selectedDate.Value.Date.ToShortDateString());
                db.openConnection();

                if (command.ExecuteNonQuery() == 1)
                {
                    WindowRecord winadm = new WindowRecord();
                    MessageBox.Show("Запись успешно отредактирована!");
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
            WindowRecord winadm = new WindowRecord();
            this.Close();
            winadm.Show();
        }
    }
}
