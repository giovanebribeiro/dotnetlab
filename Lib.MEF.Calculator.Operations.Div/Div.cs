using Lab.MEF.Calculator;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.MEF.Calculator.Operations.Div
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '%')]
    public class Div : IOperation
    {
        public int Operate(int left, int right)
        {
            return left % right;
        }
    }
}
