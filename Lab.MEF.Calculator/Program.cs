using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.Calculator
{
    /**
     * Tutorial present in https://msdn.microsoft.com/en-us/library/dd460648.aspx
     */
    class Program
    {
        private CompositionContainer container;
        [Import(typeof(ICalculator))]
        public ICalculator calc;

        private Program()
        {
            // Can combine multiple catalogs
            var catalog = new AggregateCatalog();
            // Add all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));
            // Add the assemblies from directory Catalog
            catalog.Catalogs.Add(new DirectoryCatalog(".\\Extensions"));

            // create the composition container
            container = new CompositionContainer(catalog);

            //fill the imports of this object
            try
            {
                this.container.ComposeParts(this);
            }
            catch(CompositionException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main(string[] args)
        {
            Program p = new Calculator.Program();
            String s;
            Console.WriteLine("Enter Command: ");
            while (true)
            {
                s = Console.ReadLine();
                Console.WriteLine(p.calc.Calculate(s));
            }
        }
    }
}
