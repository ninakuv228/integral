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
using Wpfintegral.Clasess;
using OxyPlot;
using System.Diagnostics;

namespace Wpfintegral
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

        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            DoCalculate();
        }

        private double SubIntegral(double x)
        {
            return 2 * x - Math.Log(2 * x) + 234;
            //return x*x;
        }

        private void DoCalculate()
        {
            double upLimit = Convert.ToDouble(tbUpLimit.Text);
            double downLimit = Convert.ToDouble(tbDownLimit.Text);
            int count = Convert.ToInt32(tbCount.Text);
            //MessageBox.Show($"Верхний предел ={upLimit} Нижний предел = {downLimit} Количество разбиений = {count}");
            ICalculatorIntegral calculyator = GetCalculator();
            double answer = calculyator.Calculate(downLimit, upLimit, count, SubIntegral);
            tbAnswer.Text = Convert.ToString(answer);
        }

        private ICalculatorIntegral GetCalculator()
        {
            if (cmbVarietion.SelectedIndex == 0)
            {
                return new IntegralCalculateTrapecia();
            }
            else if (cmbVarietion.SelectedIndex == 1)
            {
                return new IntegralCalculateRectangle();
            }
            else if (cmbVarietion.SelectedIndex == 2)
            {
                return new IntegralCalculateSimpson();
            }
            else
            {
                throw new Exception("Выбран не поддерживаемый метод");
            }
        }

        private void btPlotGraph_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel Graph = this.DataContext as MainViewModel;
            Graph.Points.Clear();
            long[] massLong = new long[6];
            if (cmbParallelNoParallel.SelectedIndex == 0)
            {
                massLong = Timer();
            }
            else
            {
                massLong = TimerParallel();
            }
            for (int i = 0; i < 8; i++)
            {
                Graph.Points.Add(new DataPoint(Convert.ToInt32(Math.Pow(10, i)), massLong[i]));
            }
        }

        public static long[] Timer()
        {
            long[] time = new long[8];
            Stopwatch timeIntegrate = new Stopwatch();
            IntegralCalculateRectangle probe = new IntegralCalculateRectangle();
            for (int i = 0; i < 8; i++)
            {
                timeIntegrate.Restart();
                probe.Calculate(0, 1000, Convert.ToInt32(Math.Pow(10, i)), (x) => 2 * x - Math.Log(2 * x) + 234);
                timeIntegrate.Stop();
                time[i] = timeIntegrate.ElapsedMilliseconds;
            }
            return time;
        }

        public static long[] TimerParallel()
        {
            long[] time = new long[8];
            Stopwatch timeIntegrate = new Stopwatch();
            IntegralCalculateRectangle probe = new IntegralCalculateRectangle();
            for (int i = 0; i < 8; i++)
            {
                timeIntegrate.Restart();
                probe.CalculateParallel(0, 1000, Convert.ToInt32(Math.Pow(10, i)), (x) => 2 * x - Math.Log(2 * x) + 234);
                timeIntegrate.Stop();
                time[i] = timeIntegrate.ElapsedMilliseconds;
            }
            return time;
        }
    }
}
