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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Net;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Configuration configuration;
        private const string db = @"Data Source=DESKTOP-CJJ68JS;Initial Catalog=Bootcamp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public MainWindow()
        {
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string myPassword = tbPass.Password;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string Hash = BCrypt.Net.BCrypt.HashPassword(myPassword);
            var getPassword = con.QueryAsync<Login>("select * from TB_M_LOGIN where username = @username",
            new { Username = tbEmail.Text }).Result.SingleOrDefault();
            var result = BCrypt.Net.BCrypt.Verify(myPassword, getPassword.password);
            if (getPassword.role == "admin")
            {
                Window1 Dashboard = new Window1();
                Dashboard.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Maaf Kamu Bukan Admin");
            }
        }

        private void BtnForgot_Click(object sender, RoutedEventArgs e)
        {
            ForgotPass forgot = new ForgotPass();
            forgot.Show();
            Close();
        }

        private void BtnRegis_Click(object sender, RoutedEventArgs e)
        {
            string pswd = tbPass.Password;
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(pswd, salt);
            var insert = con.Execute("exec SP_Login_Insert @username, @password", 
                new { username = tbEmail.Text, password = hash });
            if (insert < 0)
            {
                MessageBox.Show("failed to register");
            }
            else
            {
                MessageBox.Show("Success to Register");
            }
        }
    }
}
