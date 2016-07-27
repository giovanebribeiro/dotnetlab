using Lab.MAF.Translator.Contract;
using Lab.MAF.Translator.HostView;
using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MAF.Translator.HostAdapter
{
    [HostAdapter]
    public class TranslatorHostViewToContract: TranslatorHostView
    {
        ITranslator contract;
        ContractHandle lifetime;

        public TranslatorHostViewToContract(ITranslator contract)
        {
            this.contract = contract;
            this.lifetime = new ContractHandle(contract);
        }

        public override string Translate(string input)
        {
            return this.contract.Translate(input);
        }
    }
