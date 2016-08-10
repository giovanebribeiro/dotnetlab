using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.HostView
{
    public interface ICalculator
    {
        double Add(double a, double b);
        double Sub(double a, double b);
        double Mult(double a, double b);
        double Div(double a, double b);
    }
}
