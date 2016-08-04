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
        }
    }
}
