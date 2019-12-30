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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AnimationsDemo demo = new AnimationsDemo();
            demo.WindowStyle = WindowStyle.None;
            demo.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BindingDemo demo = new BindingDemo();
            demo.ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ControlInputDemo demo = new ControlInputDemo();
            demo.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            DataGridDemo demo = new DataGridDemo();
            demo.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            throw new Exception("Excepción lanzada que no cierra la aplicación");
        }
    }
}
