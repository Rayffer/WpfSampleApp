using Ploeh.AutoFixture;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace WpfSampleApp
{
    /// <summary>
    /// Interaction logic for AnimationsDemo.xaml
    /// </summary>
    public partial class AnimationsDemo : Window
    {
        private Fixture specimenBuilders;
        private Thread unblockingThread;
        private Duration animationDuration;

        public AnimationsDemo()
        {
            InitializeComponent();
            specimenBuilders = new Fixture();
            unblockingThread = new Thread(new ThreadStart(ThreadMethod));
            animationDuration = new Duration(TimeSpan.FromSeconds(0.5F));
            this.Background = new SolidColorBrush(Colors.Transparent);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var redComponent = specimenBuilders.Create<byte>();
            var greenComponent = specimenBuilders.Create<byte>();
            var blueComponent = specimenBuilders.Create<byte>();
            var alphaComponent = specimenBuilders.Create<byte>();

            var generatedColor = (Color)System.Windows.Media.ColorConverter.ConvertFromString($"#{alphaComponent.ToString("X2")}{redComponent.ToString("X2")}{greenComponent.ToString("X2")}{blueComponent.ToString("X2")}");

            ColorAnimation colorAnimation = new ColorAnimation(generatedColor, animationDuration);

            this.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var generatedWidth = specimenBuilders.Create<double>();
            DoubleAnimation doubleAnimation = new DoubleAnimation(140 + generatedWidth, animationDuration);

            (sender as Button).BeginAnimation(WidthProperty, doubleAnimation);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Thread.Sleep(5000);

            var fromColor = Colors.LimeGreen;
            var toColor = Colors.Transparent;

            ColorAnimation colorAnimation = new ColorAnimation(fromColor, toColor, animationDuration);

            this.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (unblockingThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                unblockingThread = new Thread(new ThreadStart(ThreadMethod));
                unblockingThread.Start();
            }
        }

        private void ThreadMethod()
        {
            Thread.Sleep(5000);

            Dispatcher.Invoke(() =>
            {
                var fromColor = Colors.LimeGreen;
                var toColor = Colors.Transparent;

                ColorAnimation colorAnimation = new ColorAnimation(fromColor, toColor, animationDuration);

                this.Background.BeginAnimation(SolidColorBrush.ColorProperty, colorAnimation);
            });
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            var generatedOpacity = random.NextDouble();

            DoubleAnimation opacityAnimation = new DoubleAnimation(generatedOpacity, animationDuration);

            this.BeginAnimation(OpacityProperty, opacityAnimation);
        }
    }
}