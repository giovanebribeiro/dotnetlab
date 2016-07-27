using Lab.MAF.Translator.AddInView;
using Lab.MAF.Translator.Contract;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Translator.AddInAdapter
{
    public class TranslatorAddInViewToContract : ContractBase, ITranslator
    {
        TranslatorAddInView view;

        public TranslatorAddInViewToContract(TranslatorAddInView view)
        {
            this.view = view;
        }
        
        public string Translate(string input)
        {
            return this.view.Translate(input);
        }
    }
}
