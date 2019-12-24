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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfSampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DataGridDemo : Window
    {
        List<ReportInformation> createdReports;
        public DataGridDemo()
        {
            InitializeComponent();
            var reports = this.FindResource("AvailableReportInformations") as ReportInformations;
            reports.Clear();
            Fixture specimenBuilders = new Fixture();
            createdReports = specimenBuilders
                .CreateMany<ReportInformation>(40)
                .ToList();
            createdReports.ForEach(report => reports.Add(report));

            directoryInfoDatagrid.ItemsSource = createdReports;
        }
    }
}
