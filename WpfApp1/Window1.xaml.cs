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
using System.Configuration;
using Dapper;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        Configuration configuration;
        private const string db = @"Data Source=DESKTOP-CJJ68JS;Initial Catalog=Bootcamp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public Window1()
        {
            InitializeComponent();
        }

        public void refresh()
        {
            var view = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            dgvEmployee.ItemsSource = view;
        }

        private void DgvEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void True(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void DgvDept_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Department>("exec SP_View_Department").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void DgvRegion_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void DgvTrainer_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void DgvBootcamp_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Bootcamp>("exec SP_View_Bootcamp").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void BtnMenuInsert_Click(object sender, RoutedEventArgs e)
        {
            Window2 MenuInsert = new Window2();
            MenuInsert.Show();
            Close();
        }

    }
}
