using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.Calculator.Operations.Mult
{
    [Export(typeof(IOperation))]
    [ExportMetadata("Symbol", '*')]
    public class Mult : IOperation
    {
        public int Operate(int left, int right)
        {
            return left * right;
        }
    }
}
