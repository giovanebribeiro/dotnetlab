using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.Calculator
{
    class Program
    {
        private CompositionContainer container;

        private Program()
        {
            // Can combine multiple catalogs
            var catalog = new AggregateCatalog();
            // Add all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            // create the composition container
            container = new CompositionContainer(catalog);
        }

        static void Main(string[] args)
        {
        }
    }
}
