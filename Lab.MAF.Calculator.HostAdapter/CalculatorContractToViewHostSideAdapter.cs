using Lab.MAF.Calculator.Contract;
using Lab.MAF.Calculator.HostView;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.HostAdapter
{
    [HostAdapterAttribute()]
    public class CalculatorContractToViewHostSideAdapter : ICalculator
    {
        private ICalculatorContract contract;
        private ContractHandle contractHandle;

        public CalculatorContractToViewHostSideAdapter(ICalculatorContract contract)
        {
            this.contract = contract;
            this.contractHandle = new ContractHandle(contract);
        }

        public double Add(double a, double b)
        {
            return this.contract.Add(a, b);
        }

        public double Div(double a, double b)
        {
            return this.contract.Div(a, b);
        }

        public double Mult(double a, double b)
        {
            return this.contract.Mult(a, b);
        }

        public double Sub(double a, double b)
        {
            return this.contract.Sub(a, b);
        }
    }
}
