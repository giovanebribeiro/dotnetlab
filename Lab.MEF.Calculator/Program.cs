using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Security.Permissions;
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
        private DirectoryCatalog directoryCatalog;

        [Import(typeof(ICalculator))]
        public ICalculator calc;

        private void SetShadowCopy()
        {
            AppDomain.CurrentDomain.SetShadowCopyFiles();
            string extensionsCache = @"C:\ExtensionsCache";
            if (!Directory.Exists(extensionsCache))
            {
                Directory.CreateDirectory(extensionsCache);
            }
            AppDomain.CurrentDomain.SetCachePath(extensionsCache);
        }

        [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
        private void InitPluginManager()
        {
            string extensionsPath = "C:\\Extensions";
            if (!Directory.Exists(extensionsPath))
            {
                Directory.CreateDirectory(extensionsPath);
            }

            SetShadowCopy();

            // Can combine multiple catalogs
            var catalog = new AggregateCatalog();
            // Add all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Program).Assembly));

            // Add the assemblies from directory Catalog
            directoryCatalog = new DirectoryCatalog(extensionsPath);
            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = extensionsPath;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            //add event handlers
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            catalog.Catalogs.Add(directoryCatalog);

            // create the composition container
            container = new CompositionContainer(catalog);

            //fill the imports of this object
            try
            {
                this.container.ComposeParts(this);
            }
            catch (CompositionException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            directoryCatalog.Refresh(); 
        }

        private Program()
        {
            InitPluginManager();   
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
