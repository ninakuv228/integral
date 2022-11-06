using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfintegral.Clasess
{
    public class IntegralCalculateRectangle : ICalculatorIntegral
    {
        public double Calculate(double down, double up, int numIntaration, Func<double, double> subInterral)
        {
            double sum = 0;
            double h = (up - down) / numIntaration;

            for (int i = 0; i < numIntaration; i++)
            {
                sum += subInterral(down + h * 0.5 + h * i);
            }
            return h * sum;
        }
        public double CalculateParallel(double down, double up, int numIntaration, Func<double, double> subInterral)
        {

            if (numIntaration <= 0)
            {
                Exception ex = new ArgumentException("Некорректное значение разбиений");
                throw ex;
            }
            double h = (up - down) / numIntaration;
            double s = 0;
            down += h / 2;
            object k = new object();
            Parallel.For(0, numIntaration, () => 0.0,
                (i, state, localtotal) =>
                localtotal + subInterral(down + h * i),
                localtotal => { lock (k) s += localtotal; });
            return h * s;
        }
    }
}
