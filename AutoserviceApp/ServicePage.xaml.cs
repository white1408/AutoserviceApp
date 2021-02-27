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
    /// Логика взаимодействия для ServicePage.xaml
    /// </summary>
    public partial class ServicePage : Page
    {
        public ServicePage()
        {
            InitializeComponent();
            DGridService.ItemsSource = AutoserviceEntities.GetContext().Services.ToList();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var serviceForRemoving = DGridService.SelectedItems.Cast<Service>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {serviceForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    AutoserviceEntities.GetContext().Services.RemoveRange(serviceForRemoving);
                    AutoserviceEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    DGridService.ItemsSource = AutoserviceEntities.GetContext().Services.ToList();
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
                DGridService.ItemsSource = AutoserviceEntities.GetContext().Services.ToList();
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new ClientServicePage());
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditServicePage(null));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new EditServicePage((sender as Button).DataContext as Service));
        }
    }
}
