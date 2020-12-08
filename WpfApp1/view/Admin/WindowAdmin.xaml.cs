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

namespace WpfApp1.view
{
    /// <summary>
    /// Логика взаимодействия для WindowCompany.xaml
    /// </summary>
    /// 

    public partial class WindowAdmin : Window
    {
        public ObservableCollection<Service> Services { get; set; }
        public WindowAdmin()
        {
            InitializeComponent();
        }
        private void go_record(object sender, RoutedEventArgs e)
        {
            WindowRecord record = new WindowRecord();
            this.Close();
            record.Show();
        }
        private void go_service(object sender, RoutedEventArgs e)
        {
            WindowAdminService service = new WindowAdminService();
            this.Close();
            service.Show();
        }
        private void go_products(object sender, RoutedEventArgs e)
        {
            WindowProducts product = new WindowProducts();
            this.Close();
            product.Show();
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

        private void adminlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service p = (Service)adminlist.SelectedItem;
        }


    }
}
