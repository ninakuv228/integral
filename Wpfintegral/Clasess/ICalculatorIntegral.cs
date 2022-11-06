using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpfintegral.Clasess
{
    public interface ICalculatorIntegral
    {
        double Calculate(double down, double up, int numIntaration, Func<double, double> subInterral);
        double CalculateParallel(double down, double up, int numIntaration, Func<double, double> subInterral);
    }
}
