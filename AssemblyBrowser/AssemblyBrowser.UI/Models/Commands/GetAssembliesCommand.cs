using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System.Collections.Generic;

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
            submenu.Add(new MenuOption("0", "Go back", typeof(GoBackCommand)));

            for (int i = 0; i < results.Length; i++)
            {
                var assembly = results[i];
                submenu.Add(new MenuOption($"{i+1}", $"{assembly}", typeof(GetAssemblyTypesCommand), assembly));
            }

            submenuOptions = submenu;
        }
    }
}
