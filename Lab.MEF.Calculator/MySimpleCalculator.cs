using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.Calculator
{
    [Export(typeof(ICalculator))]
    class MySimpleCalculator : ICalculator
    {
        [ImportMany(AllowRecomposition = true)]
        private IEnumerable<Lazy<IOperation, IOperationData>> Operations;

        private int FindFirstNonDigit(String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!(Char.IsDigit(s[i]))) return i;
            }
            return -1;
        }

        public string Calculate(string input)
        {
            int left;
            int right;
            Char operation;

            int fn = FindFirstNonDigit(input); //finds te operator
            if (fn < 0) return "Could not parse command.";

            try
            {
                left = int.Parse(input.Substring(0, fn));
                right = int.Parse(input.Substring(fn + 1));
            }
            catch
            {
                return "Could not parse command";
            }

            operation = input[fn];

            foreach(Lazy<IOperation, IOperationData> i in Operations)
            {
                if (i.Metadata.Symbol.Equals(operation)) return i.Value.Operate(left, right).ToString();
            }
            return "Operation not found!";
        }
    }
}
