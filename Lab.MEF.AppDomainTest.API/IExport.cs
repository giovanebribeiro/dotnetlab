using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab.MEF.AppDomainTest.API
{
    /// <summary>
    /// See link: http://www.codeproject.com/Articles/633140/MEF-and-AppDomain-Remove-Assemblies-On-The-Fly
    /// </summary>
    public interface IExport
    {
        void InHere();
    }
}
