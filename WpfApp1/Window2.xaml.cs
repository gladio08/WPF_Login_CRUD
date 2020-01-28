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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        Configuration configuration;
        private const string db = @"Data Source=DESKTOP-CJJ68JS;Initial Catalog=Bootcamp;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnection"].ConnectionString);
        public Window2()
        {
            InitializeComponent();
            var listreligion = con.QueryAsync<Religion>("SELECT Name FROM TB_M_RELIGION").Result.ToList();
            cbReligion.ItemsSource = listreligion;
        }

        public void clear()
        {
            tbName.Text = "";
            tbPhone.Text = "";
            tbAddress.Text = "";
            tbPlaceBirth.Text = "";
            tglLahir.Text = "";
            tbReligion.Text = "";
            tbNIK.Text = "";
            tbEmails.Text = "";
            tbNPWP.Text = "";
            tbDegree.Text = "";
            tbUniv.Text = "";
            tbDept.Text = "";
            tbMajors.Text = "";
            tbJob.Text = "";
            tglJoin.Text = "";

        }

        public void refresh()
        {
            var view = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            dgvEmployee.ItemsSource = view;
        }

        private void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            var insert = con.ExecuteAsync("exec SP_Employee_Insert @name, " +
                "@phone, " +
                "@address, " +
                "@placebirth, " +
                "@birthdate," +
                "@religion," +
                "@nik, " +
                "@email, " +
                "@npwp, " +
                "@degree, " +
                "@university, " +
                "@department, " +
                "@majors, " +
                "@joindate ",

                new
                {
                    Name = tbName.Text,
                    Phone = tbPhone.Text,
                    Address = tbAddress.Text,
                    PlaceBirth = tbPlaceBirth.Text,
                    BirthDate = tglLahir.SelectedDate.Value.Date,
                    Religion = tbReligion.Text,
                    NIK = tbNIK.Text,
                    Email = tbEmails.Text,
                    NPWP = tbNPWP.Text,
                    Degree = tbDegree.Text,
                    University = tbUniv.Text,
                    Department = tbDept.Text,
                    Majors = tbMajors.Text,
                    JoinDate = tglJoin.SelectedDate.Value.Date
                });
            MessageBox.Show("Data Entered");
            clear();
            refresh();
            
        }

        private void DgvEmployee_Loaded(object sender, RoutedEventArgs e)
        {
            var show = con.QueryAsync<Employee>("exec SP_View_Employee").Result.ToList();
            var grid = sender as DataGrid;
            grid.ItemsSource = show;
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var update = con.ExecuteAsync("exec SP_Employee_Update @name, " +
                "@phone, " +
                "@address, " +
                "@placebirth, " +
                "@birthdate," +
                "@religion," +
                "@nik, " +
                "@email, " +
                "@npwp, " +
                "@degree, " +
                "@university, " +
                "@department, " +
                "@majors, " +
                "@joindate ",

                new
                {
                    Name = tbName.Text,
                    Phone = tbPhone.Text,
                    Address = tbAddress.Text,
                    PlaceBirth = tbPlaceBirth.Text,
                    BirthDate = tglLahir.SelectedDate.Value.Date,
                    Religion = cbReligion.SelectedValuePath.ToString(),
                    NIK = tbNIK.Text,
                    Email = tbEmails.Text,
                    NPWP = tbNPWP.Text,
                    Degree = tbDegree.Text,
                    University = tbUniv.Text,
                    Department = tbDept.Text,
                    Majors = tbMajors.Text,
                    JoinDate = tglJoin.SelectedDate.Value.Date
                });
            MessageBox.Show("Data Has Been Changed");
            clear();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
