using Lab.MEF.AppDomainTest.API;
using System.ComponentModel.Composition;
using System;

namespace Lab.MEF.AppDomainTest.Plugin1
{
    [Export(typeof(IExport))]
    public class PluginExport1 : MarshalByRefObject, IExport
    {
        public void InHere()
        {
            Console.WriteLine(">>>> Inside Plugin 1. AppDomain: {0} <<<<", AppDomain.CurrentDomain.FriendlyName);
        }
    }
}
