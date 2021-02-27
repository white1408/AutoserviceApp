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
    /// Логика взаимодействия для ClientServicePage.xaml
    /// </summary>
    public partial class ClientServicePage : Page
    {
        public ClientServicePage()
        {
            InitializeComponent();
            DGridClientService.ItemsSource = AutoserviceEntities.GetContext().ClientServices.ToList();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditClientServicePage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var clientServiceForRemoving = DGridClientService.SelectedItems.Cast<ClientService>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {clientServiceForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AutoserviceEntities.GetContext().ClientServices.RemoveRange(clientServiceForRemoving);
                    AutoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridClientService.ItemsSource = AutoserviceEntities.GetContext().ClientServices.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                AutoserviceEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                DGridClientService.ItemsSource = AutoserviceEntities.GetContext().ClientServices.ToList();
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditClientServicePage((sender as Button).DataContext as ClientService));
        }
    }
}