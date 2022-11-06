using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfintegral.Clasess
{
    public class IntegralCalculateSimpson : ICalculatorIntegral
    {
        public double Calculate(double down, double up, int numIntaration, Func<double, double> subInterral)
        {
            double h = (up - down) / numIntaration;
            double sum1 = 0d;
            double sum2 = 0d;

            for (int k = 1; k <= numIntaration; k++)
            {
                double xk = down + k * h;
                if (k <= numIntaration - 1)
                {
                    sum1 += subInterral(xk);
                }

                double xk_1 = down + (k - 1) * h;
                sum2 += subInterral((xk + xk_1) / 2);
            }

            double result = h / 3d * (1d / 2d * subInterral(down) + sum1 + 2 * sum2 + 1d / 2d * subInterral(up));
            return result;
        }
        public double CalculateParallel(double down, double up, int numIntaration, Func<double, double> subInterral)
        {
            if ((numIntaration <= 0) || (up <= down))
            {
                Exception ex = new ArgumentException("Error!");
                throw ex;
            }
            else
            {
                double h = (up - down) / numIntaration;
                double sum1 = 0d;
                double sum2 = 0d;
                object k1 = new object();
                Parallel.For(1, numIntaration, k => {
                    double xk = down + k * h;
                    if (k <= numIntaration - 1)
                    {
                        lock (k1) sum1 += subInterral(xk);
                    }

                    double xk_1 = down + (k - 1) * h;
                    lock (k1) sum2 += subInterral((xk + xk_1) / 2);
                });

                return h / 3d * ((1d / 2d) * subInterral(down) + sum1 + 2 * sum2 + (1d / 2d) * subInterral(up));
            }
        }
    }
}
