using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Calculator.Contracts
{
    [AddInContract]
    interface ICalculator
    {
        void Calculate(string input);
    }
}
