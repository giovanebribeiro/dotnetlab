﻿using Lab.MEF.AppDomainTest.Runner;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.AppDomainTest.App
{
    /// <summary>
    /// See link: http://www.codeproject.com/Articles/633140/MEF-and-AppDomain-Remove-Assemblies-On-The-Fly
    /// </summary>
    class Program
    {
        private static AppDomain Domain;
        static void Main(string[] args)
        {
            var cachePath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "ShadowCopyCache");
            var pluginPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, "Plugins");

            if (!Directory.Exists(cachePath)) { Directory.CreateDirectory(cachePath); }
            if (!Directory.Exists(pluginPath)) { Directory.CreateDirectory(pluginPath); }

            var setup = new AppDomainSetup
            {
                CachePath = cachePath,
                ShadowCopyFiles = "true",
                ShadowCopyDirectories = pluginPath
            };

            // Create a new app domain
            Domain = AppDomain.CreateDomain("Host_AppDomain", AppDomain.CurrentDomain.Evidence, setup);
            var runner = (LabMEFRunner)Domain.CreateInstanceAndUnwrap(typeof(LabMEFRunner).Assembly.FullName, typeof(LabMEFRunner).FullName);

            Console.WriteLine("The main AppDomain is :{0}", AppDomain.CurrentDomain.FriendlyName);

            runner.DoWorkInShadowCopiedDomain();
            runner.DoSomething();

            Console.WriteLine("\nHere you can remove a DLL from the Plugins folder.");
            Console.WriteLine("Press any key when ready...");
            Console.ReadKey();

            // After remove a DLL, we can recompose the MEF parts and see that the 
            //removed DLL is no longer accessed.
            runner.Recompose();
            runner.DoSomething();
            Console.WriteLine("Press any key when ready.");
            Console.ReadKey();

            // clean up
            AppDomain.Unload(Domain);
        }
    }
}
