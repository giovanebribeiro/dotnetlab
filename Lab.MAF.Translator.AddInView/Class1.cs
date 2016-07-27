using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Translator.AddInView
{
    [AddInBase]
    public abstract class TranslatorAddInView
    {
        public abstract string Translate(string input);
    }
}
