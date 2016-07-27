using System;
using System.AddIn.Contract;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Translator.Contract
{
    [AddInContract]
    public interface ITranslator:IContract
    {
        string Translate(string input);
    }
}
