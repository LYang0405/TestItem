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
using System.Windows.Threading;

namespace MiuiView
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        #region 霓虹灯边框效果

        public SolidColorBrush NeonBrush
        {
            get { return (SolidColorBrush)base.GetValue(NeonBrushProperty); }
            set { base.SetValue(NeonBrushProperty, value); }
        }
        public static readonly DependencyProperty NeonBrushProperty =
            DependencyProperty.Register("NeonBrush", typeof(SolidColorBrush), typeof(MainWindow),
                new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(78,201,250))));

        public Color NeonColor
        {
            get { return (Color)base.GetValue(NeonColorProperty); }
            set { base.SetValue(NeonColorProperty, value); }
        }
        public static readonly DependencyProperty NeonColorProperty =
            DependencyProperty.Register("NeonColor", typeof(Color), typeof(MainWindow),
                new FrameworkPropertyMetadata(Color.FromRgb(78, 201, 250), OnNeonColorChanged));
        private static void OnNeonColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow wnd = sender as MainWindow;
            if (sender != null)
                wnd.NeonBrush = new SolidColorBrush((Color)e.NewValue);
        }

        private void CreateBorderColorAnim()
        {
            ColorAnimationUsingKeyFrames colorKeyFrms = new ColorAnimationUsingKeyFrames();
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(245, 104, 5),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(236, 247, 8),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(10, 124, 238),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.5d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(69, 205, 199),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(8, 83, 158),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(2.5d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(234, 112, 112),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(6, 247, 203),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.5d))));
            colorKeyFrms.KeyFrames.Add(new EasingColorKeyFrame(Color.FromRgb(78, 201, 250),
                KeyTime.FromTimeSpan(TimeSpan.FromSeconds(4d))));

            colorKeyFrms.RepeatBehavior = RepeatBehavior.Forever;
            this.BeginAnimation(NeonColorProperty, colorKeyFrms);
        }
       
        #endregion


        #region 左下角参数变化

        internal double MinParamValLeftX = 0;
        internal double MaxParamValLeftX = 130;

        public double ParamValLeft
        {
            get { return (double)base.GetValue(ParamValLeftProperty); }
            set { base.SetValue(ParamValLeftProperty, value); }
        }
        public static readonly DependencyProperty ParamValLeftProperty =
            DependencyProperty.Register("ParamValLeft", typeof(double), typeof(MainWindow),
                new FrameworkPropertyMetadata(0d, OnParamValLeftChanged));
        private static void OnParamValLeftChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow wnd = sender as MainWindow;
            if (sender != null)
                wnd.PathTriangleLeft.Margin = new Thickness((double)e.NewValue - 4, 0, 0, 0);
        }

        public void SetParamValLeft(double dValue)
        {
            if (dValue < MinParamValLeftX)
                dValue = MinParamValLeftX;
            if (dValue > MaxParamValLeftX)
                dValue = MaxParamValLeftX;

            DoubleAnimation anim = new DoubleAnimation(dValue, TimeSpan.FromSeconds(0.5));
            anim.EasingFunction = new SineEase();
            this.BeginAnimation(ParamValLeftProperty, anim);
        }

        private void CreateParamValLeftChangeSample()
        {
            DispatcherTimer timerForTest = new DispatcherTimer();
            timerForTest.Interval = TimeSpan.FromSeconds(1.5);
            timerForTest.Tick += (s, e) => {
                Random oRandom = new Random();
                int nNewVal = oRandom.Next(0, 130);
                this.SetParamValLeft(nNewVal);
                this.TbkParamLeftVal.Text = nNewVal + "H";

                double dPercent = Math.Round(nNewVal / 130d, 3) * 100;
                this.TbkParamLeftValPercent.Text = dPercent.ToString() + "%";
            };
            timerForTest.Start();
        }
       
        #endregion


        #region 右下角参数变化

        internal double MinParamValRightX = 0;
        internal double MaxParamValRightX = 130;

        public double ParamValRight
        {
            get { return (double)base.GetValue(ParamValRightProperty); }
            set { base.SetValue(ParamValRightProperty, value); }
        }
        public static readonly DependencyProperty ParamValRightProperty =
            DependencyProperty.Register("ParamValRight", typeof(double), typeof(MainWindow),
                new FrameworkPropertyMetadata(0d, OnParamValRightChanged));
        private static void OnParamValRightChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow wnd = sender as MainWindow;
            if (sender != null)
            {
                double dMarginRight = (double)e.NewValue;
                wnd.PathTriangleRight.Margin = new Thickness(0, 0, dMarginRight - 6d, 0);
            }
        }

        public void SetParamValRight(double dValue)
        {
            if (dValue < MinParamValRightX)
                dValue = MinParamValRightX;
            if (dValue > MaxParamValRightX)
                dValue = MaxParamValRightX;

            DoubleAnimation anim = new DoubleAnimation(dValue, TimeSpan.FromSeconds(0.5));
            anim.EasingFunction = new SineEase();
            this.BeginAnimation(ParamValRightProperty, anim);
        }
        private void CreateParamValRightChangeSample()
        {
            DispatcherTimer timerForTest = new DispatcherTimer();
            timerForTest.Interval = TimeSpan.FromSeconds(1.2);
            timerForTest.Tick += (s, e) => {
                Random oRandom = new Random();
                int nNewVal = oRandom.Next(0, 130);
                this.SetParamValRight(nNewVal);

                double dPercent = Math.Round(nNewVal / 130d, 3) * 100;
                this.TbkParamRightValPercent.Text = dPercent.ToString() + "%";
                this.TbkParamRightVal.Text = dPercent * 10 + "MB";
            };
            timerForTest.Start();
        }
       
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            
            this.DataContext = this;
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.CreateBorderColorAnim();
            this.CreateParamValLeftChangeSample();
            this.CreateParamValRightChangeSample();
        }



        #region Window移动和关闭

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void BdrHeaderBg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    DragMove();
            }
        }

        #endregion
    }
}
