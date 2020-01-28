
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
    /// Interaction logic for UserManager.xaml
    /// </summary>
    public partial class UserManager : Window
    {
        Configuration configuration;
        private const string db = @"Data Source=DESKTOP-CJJ68JS;Initial Catalog=Bootcamp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public UserManager()
        {
            InitializeComponent();
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            var insert = con.QueryAsync<insert>("exec SP_Employee_Insert @name, @phone, @address, @placebirth, @birthdate, @nik, @email, @religion, @npwp, @bachelor, @university, @joindate",
                new
                {
                    Name = tbNama.Text,
                    PhoneNumber = tbNomer.Text,
                    Address = tbAlamat.Text,
                    PlaceBirth = tbTmpt.Text,
                    BirthDate = tglLahir.SelectedDate,
                    NIK = tbNIK.Text,
                    Email = tbEmails.Text,
                    Religion = tbAgama.Text,
                    NPWP = tbNPWP.Text,
                    Bachelor = tbBachelor.Text,
                    University = tbUniv.Text,
                    JoinDate = tglJoin.SelectedDate
                });
            MessageBox.Show("Data Entered");
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var update = con.QueryAsync<insert>("exec SP_Employee_Update @name, @phone, @address, @placebirth, @birthdate, @nik, @email, @religion, @npwp, @bachelor, @university, @joindate",
                new
                {
                    Name = tbNama.Text,
                    PhoneNumber = tbNomer.Text,
                    Address = tbAlamat.Text,
                    PlaceBirth = tbTmpt.Text,
                    BirthDate = tglLahir.Text,
                    NIK = tbNIK,
                    Email = tbEmails.Text,
                    Religion = tbAgama.Text,
                    NPWp = tbNPWP.Text,
                    Bachelor = tbBachelor.Text,
                    University = tbUniv.Text,
                    JoinDate = tglJoin.Text
                });
            MessageBox.Show("Data Updated");
        }

        private void DgvEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
            var show = con.QueryAsync<Employee>("exec SP_View_EMP").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void DgvEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
