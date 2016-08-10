using Lab.MAF.Calculator.AddInView;
using Lab.MAF.Calculator.Contract;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.AddInAdapter
{
    [AddInAdapter()]
    public class CalculatorViewToContractAddInSideAdapter : ContractBase, ICalculatorContract
    {
        private ICalculator view;

        public CalculatorViewToContractAddInSideAdapter(ICalculator view)
        {
            this.view = view;
        }

        public virtual double Add(double a, double b)
        {
            return this.view.Add(a, b);
        }

        public virtual double Div(double a, double b)
        {
            return this.view.Div(a, b);
        }

        public virtual double Mult(double a, double b)
        {
            return this.view.Mult(a, b);
        }

        public virtual double Sub(double a, double b)
        {
            return this.view.Sub(a, b);
        }
    }
}
