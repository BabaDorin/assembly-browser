using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models.Commands
{
    public class GetAssembliesCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetAssembliesCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            var results = _assemblyBrowser.GetApplicationAssemblies();

            var submenu = new List<MenuOption>();
            submenu.Add(new MenuOption("0: Go back", "Go back", typeof(GoBackCommand)));

            for(int i = 0; i < results.Length; i++)
            {
                var assembly = results[i];
                submenu.Add(new MenuOption($"{i+1}: {assembly}", assembly, typeof(GetAssemblyTypesCommand)));
            }

            submenuOptions = submenu;
        }
    }
}
