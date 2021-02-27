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

namespace AutoserviceApp
{
    /// <summary>
    /// Логика взаимодействия для EditClientServicePage.xaml
    /// </summary>
    public partial class EditClientServicePage : Page
    {
        private ClientService _currentClientServices = new ClientService();

        public EditClientServicePage(ClientService selectedClientServices)
        {
            InitializeComponent();

            if (selectedClientServices != null)
                _currentClientServices = selectedClientServices;

            DataContext = _currentClientServices;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentClientServices.ID == 0)
                AutoserviceEntities.GetContext().ClientServices.Add(_currentClientServices);

            try
            {
                AutoserviceEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!");
                Manager.MainFrame.Navigate(new ClientServicePage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
