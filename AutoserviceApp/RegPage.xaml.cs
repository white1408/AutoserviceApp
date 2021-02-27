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
using System.Data;
using System.Data.SqlClient; //For SQL Connection
using System.Windows;
using System;

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;
        void GetList()
        {
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=Autoservice; Integrated Security=True");
            da = new SqlDataAdapter("Select *From Users", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "Users");
            con.Close();
        }

        private void RegPage_Load(object sender, EventArgs e)
        {
            {
                GetList();
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || Password.Text == "")
            {
                MessageBox.Show("Заполните все поля.", "", MessageBoxButton.OK);
            }
            else
            {
                SqlConnection Connection = new SqlConnection();
                SqlCommand comand = new SqlCommand();
                SqlDataAdapter adaptor = new SqlDataAdapter();
                DataSet dataset = new DataSet();
                Connection.ConnectionString = (@"Data Source=.\SQLEXPRESS; Initial Catalog=Autoservice ; Integrated Security=True");
                comand.CommandText = @"SELECT * FROM [Users] WHERE Login='" + Login.Text + "'AND Password='" + Password.Text + "';";
                comand = new SqlCommand();
                Connection.Open();
                comand.Connection = Connection;
                comand.CommandText = "insert into Users(Login,Password) values (" + "'" + Login.Text + "','" + Password.Text + "')";
                comand.ExecuteNonQuery();
                Connection.Close();
                GetList();
                MessageBox.Show("Вы успешно зарегистрировались.", "", MessageBoxButton.OK);
                Manager.MainFrame.Navigate(new AutoPage());
            }
        }
    }
}
