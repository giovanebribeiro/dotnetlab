using Lab.MAF.Calculator.AddInView;
using System;
using System.AddIn;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.AddInCalc
{
    [AddIn("Calculator AddIn", Version = "01.00.00")]
    public class AddInCalcV1 : ICalculator
    {
        public double Add(double a, double b)
        {
            return a + b;
        }

        public double Div(double a, double b)
        {
            return a / b;
        }

        public double Mult(double a, double b)
        {
            return a * b;
        }

        public double Sub(double a, double b)
        {
            return a - b;
        }
    }
}
