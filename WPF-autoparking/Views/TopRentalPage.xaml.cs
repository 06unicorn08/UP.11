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
using WPF_autoparking.ViewModel;

namespace WPF_autoparking.Views
{
    /// <summary>
    /// Логика взаимодействия для TopRentalPage.xaml
    /// </summary>
    public partial class TopRentalPage : Page
    {
        public TopRentalPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = AutoParkEntities.GetContext().TopEmployeeRentals.ToList();
        }

        private async void btnReport_Click(object sender, RoutedEventArgs e)
        {
            ReportModel report = new ReportModel();
            await report.TopEmployeesGenAsync();
        }
    }
}
