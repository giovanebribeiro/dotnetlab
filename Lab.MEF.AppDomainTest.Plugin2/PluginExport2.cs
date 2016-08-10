using Lab.MEF.AppDomainTest.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.AppDomainTest.Plugin2
{
    [Export(typeof(IExport))]
    public class PluginExport2 : IExport
    {
        public void InHere()
        {
            Console.WriteLine(" >>>> Inside Plugin 2. AppDomain: {0} <<<< ", AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
