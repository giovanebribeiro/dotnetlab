using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.HostView
{
    public abstract class ICalculator
    {
        public abstract double Add(double a, double b);
        public abstract double Sub(double a, double b);
        public abstract double Mult(double a, double b);
        public abstract double Div(double a, double b);
    }
}
