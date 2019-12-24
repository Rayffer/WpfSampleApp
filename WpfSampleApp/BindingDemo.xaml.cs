using Newtonsoft.Json;
using Ploeh.AutoFixture;
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

namespace WpfSampleApp
{
    /// <summary>
    /// Interaction logic for BindingDemo.xaml
    /// </summary>
    public partial class BindingDemo : Window
    {
        public BindingDemo()
        {
            InitializeComponent();
            var reports = this.FindResource("AvailableReportInformations") as ReportInformations;
            reports.Clear();
            Fixture specimenBuilders = new Fixture();
            var createdReports = specimenBuilders
                .CreateMany<ReportInformation>(5)
                .ToList();
            createdReports.ForEach(report => reports.Add(report));
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataContext = (sender as DataGrid).SelectedItem;
        }
    }
}
