using Ploeh.AutoFixture;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

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
            this.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var redComponent = specimenBuilders.Create<byte>();
            var greenComponent = specimenBuilders.Create<byte>();
            var blueComponent = specimenBuilders.Create<byte>();
            var alphaComponent = (byte)255;

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

            DoubleAnimation opacityAnimation = new DoubleAnimation(0.4 + generatedOpacity * 0.6, animationDuration);

            this.BeginAnimation(OpacityProperty, opacityAnimation);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool animationFinished = true;

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (animationFinished && stackPanelLabelHolder.Children.Count > 0)
            {
                animationFinished = false;
                var firstControl = stackPanelLabelHolder.Children[0] as FrameworkElement;
                firstControl.Width = firstControl.ActualWidth;

                DoubleAnimation doubleAnimationWithRemoval = new DoubleAnimation(0, animationDuration);
                doubleAnimationWithRemoval.Completed += DoubleAnimation_Completed;
                doubleAnimationWithRemoval.AccelerationRatio = 1F;

                ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, firstControl.Margin.Top, 0, firstControl.Margin.Bottom), animationDuration);

                firstControl.BeginAnimation(MarginProperty, thicknessAnimation);
                firstControl.BeginAnimation(WidthProperty, doubleAnimationWithRemoval);
            }
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            animationFinished = true;
            stackPanelLabelHolder.Children.Remove(stackPanelLabelHolder.Children[0]);
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            stackPanelLabelHolder.Children.Clear();

            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 1", Brushes.Red));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 2", Brushes.DeepSkyBlue));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 3", Brushes.Green));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 4", Brushes.Yellow));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 5", Brushes.Pink));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 6", Brushes.Magenta));
            stackPanelLabelHolder.Children.Add(CreateLabel("Stack Panel Item 7", Brushes.Crimson));
        }

        private Label CreateLabel(string labelContent, Brush labelBackgroundBrush)
        {
            Label createdLabel = new Label();
            createdLabel.Content = labelContent;
            createdLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            createdLabel.VerticalContentAlignment = VerticalAlignment.Center;
            createdLabel.Margin = new Thickness(2);
            createdLabel.Background = labelBackgroundBrush;
            createdLabel.Width = 0;

            Dispatcher.BeginInvoke((Action)(() =>
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(MeasureString(createdLabel).Width, animationDuration);
                doubleAnimation.AccelerationRatio = 1F;

                ThicknessAnimation thicknessAnimation = new ThicknessAnimation(new Thickness(0, 2, 0, 2), new Thickness(2), animationDuration);

                createdLabel.BeginAnimation(WidthProperty, doubleAnimation);
                createdLabel.BeginAnimation(MarginProperty, thicknessAnimation);
            }), System.Windows.Threading.DispatcherPriority.Background);

            return createdLabel;
        }

        private Size MeasureString(Label candidate)
        {
            var formattedText = new FormattedText(
                candidate.Content as string,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface(candidate.FontFamily, candidate.FontStyle, candidate.FontWeight, candidate.FontStretch),
                candidate.FontSize,
                Brushes.Black,
                new NumberSubstitution(),
                1);

            return new Size(formattedText.Width + candidate.Padding.Left + candidate.Padding.Right,
                formattedText.Height + candidate.Padding.Top + candidate.Padding.Bottom);
        }

        private bool isDragging = false;

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            isDragging = !isDragging;
        }

        private void Label_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isDragging)
            {
                var button = (sender as Button);
                System.Windows.Point position = e.GetPosition(this);
                button.Margin = new Thickness(position.X - button.Width / 2, position.Y - button.Height / 2, 0, 0);
            }
        }

        private bool isRotating = false;

        private void Button_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (isRotating)
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(0, animationDuration);

                Storyboard storyboard = new Storyboard();
                storyboard.Duration = animationDuration;
                Storyboard.SetTarget(doubleAnimation, sender as Button);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                storyboard.Children.Add(doubleAnimation);

                storyboard.Begin(this, HandoffBehavior.SnapshotAndReplace);
            }
            else
            {
                DoubleAnimation doubleAnimation = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(1.5)));

                Storyboard storyboard = new Storyboard();
                storyboard.Duration = new Duration(TimeSpan.FromSeconds(1.5));
                Storyboard.SetTarget(doubleAnimation, sender as Button);
                Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));
                storyboard.RepeatBehavior = RepeatBehavior.Forever;
                storyboard.Children.Add(doubleAnimation);

                storyboard.Begin(this, HandoffBehavior.SnapshotAndReplace);
            }
            isRotating = !isRotating;
        }
    }
}