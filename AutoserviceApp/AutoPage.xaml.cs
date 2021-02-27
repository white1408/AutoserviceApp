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
using System.Data.SqlClient; //For SQL Connection
using System.Data;

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для AutoPage.xaml
    /// </summary>
    public partial class AutoPage : Page
    {
        public AutoPage()
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

        private void AutoPage_Load(object sender, EventArgs e)
        {
            {
                GetList();
            }
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new RegPage());
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            {
                SqlConnection connection = new SqlConnection();
                SqlCommand comand = new SqlCommand();
                SqlDataAdapter adaptor = new SqlDataAdapter();
                DataSet dataset = new DataSet();
                connection.ConnectionString = (@"Data Source=.\SQLEXPRESS; Initial Catalog=Autoservice; Integrated Security=True");
                comand.CommandText = @"SELECT * FROM [Users] WHERE Login='" + Login.Text + "'AND Password='" + Password.Text + "';";
                connection.Open();
                comand.Connection = connection;
                adaptor.SelectCommand = comand;
                adaptor.Fill(dataset);
                int count = dataset.Tables[0].Rows.Count;
                if (Login.Text == "Админ")
                {
                    if (Password.Text == "123")
                    {
                        MessageBox.Show("Администратор вы успешно вошли.", "", MessageBoxButton.OK);
                        Manager.MainFrame.Navigate(new ClientPage());
                    }
                }
                else if (count > 0)
                {
                    MessageBox.Show("Вы успешно вошли.", "", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show("Не верный логин или пароль", "", MessageBoxButton.OK);
                    Login.Clear();
                    Password.Clear();
                }
            }
        }
    }
}
