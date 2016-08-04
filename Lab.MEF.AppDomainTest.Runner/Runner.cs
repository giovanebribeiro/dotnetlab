using Lab.MEF.AppDomainTest.API;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Registration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.AppDomainTest.Runner
{
    public class Runner: MarshalByRefObject
    {
        private CompositionContainer container;
        private DirectoryCatalog directoryCatalog;
        [ImportMany]
        private IEnumerable<IExport> exports;
        private static readonly string pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

        public void DoWorkInShadowCopiedDomain()
        {
            var regBuilder = new RegistrationBuilder();
            regBuilder.ForTypesDerivedFrom<IExport>().Export();

            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Runner).Assembly, regBuilder));
            directoryCatalog = new DirectoryCatalog(pluginPath, regBuilder);
            catalog.Catalogs.Add(directoryCatalog);

            container = new CompositionContainer(catalog);
            container.ComposeExportedValue(container);

            exports = container.GetExportedValues<IExport>();
            Console.WriteLine("{0} exports in AppDomain {1}", exports.Count(), AppDomain.CurrentDomain.FriendlyName);
        }

        public void Recompose()
        {
            directoryCatalog.Refresh();
            container.ComposeParts(directoryCatalog.Parts);
            exports = container.GetExportedValues<IExport>();
        }

        public void DoSomething()
        {
            exports.ToList().ForEach(e => e.InHere());
        }
    }
}
