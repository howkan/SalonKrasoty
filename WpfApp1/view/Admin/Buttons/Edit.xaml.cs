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

namespace WpfApp1.view.Admin.Buttons
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {

        Service Service;
        public Edit(Service init)
        {
            InitializeComponent();

            this.Service = init;
            title_box.Text = init.Title;
            cost_box.Text = init.Costedit;
            time_box.Text = init.DurationInSeconds;
            skidka_box.Text = init.DiscountEdit;
        }

        private void go_save(object sender, RoutedEventArgs e)
        {
            DB db = new DB();
            SqlCommand command = new SqlCommand("UPDATE Service SET Name = @title, Price = @cost, Time = @time, Discount = @skidka, ImagePath = @image Where ID = @ids", db.getConnection());
            command.Parameters.AddWithValue("@title", title_box.Text);
            command.Parameters.AddWithValue("@cost", cost_box.Text);
            command.Parameters.AddWithValue("@time", time_box.Text);
            command.Parameters.AddWithValue("@skidka", skidka_box.Text);
            command.Parameters.AddWithValue("@image", Service.MainImagePath);
            command.Parameters.AddWithValue("@ids", Service.ID);
            db.openConnection();

            if (command.ExecuteNonQuery() == 1)
            {
                WindowAdminService winadm = new WindowAdminService();
                MessageBox.Show("Услуга успешно отредактирована!");
                this.Close();
                winadm.Show();
            }
        }

        private void select_image(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Service.MainImagePath = openFileDialog.FileName;
            }
            Console.WriteLine("path" + Service.MainImagePath);
        }


        private void go_back(object sender, RoutedEventArgs e)
        {
            WindowAdminService winadm = new WindowAdminService();
            this.Close();
            winadm.Show();
        }
    }
}
