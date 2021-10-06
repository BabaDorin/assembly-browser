using AssemblyBrowser.Application.Contracts;
using System;
using System.Reflection;
using System.Reflection.Metadata;

namespace AssemblyBrowser.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            IAssemblyBrowser assemblyBrowser = new Application.AssemblyBrowser();
        }


    }
}
