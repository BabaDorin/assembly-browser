using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            //if (parameter == null || !(parameter is Assembly))
            //throw new ArgumentException("Parameter is not of type: Assembly.", nameof(parameter));

            var results = _assemblyBrowser.GetApplicationAssemblies();

            var submenu = new List<MenuOption>();
            submenu.Add(new MenuOption("Go back", typeof(GoBackCommand)));

            foreach(Assembly assembly in results)
            {
                submenu.Add(new MenuOption(parameter, typeof(GetAssemblyTypesCommand)));
            }

            submenuOptions = submenu;
        }
    }
}
