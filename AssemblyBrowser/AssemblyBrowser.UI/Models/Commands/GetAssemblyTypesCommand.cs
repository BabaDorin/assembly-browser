using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetAssemblyTypesCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetAssemblyTypesCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            if (parameter == null || !(parameter is Assembly))
                throw new ArgumentException("Parameter is not of type: Assembly.", nameof(parameter));

            Console.WriteLine($"Assembly: {parameter}\n");

            var results = _assemblyBrowser.GetAssemblyTypes((Assembly)parameter);

            var submenu = new List<MenuOption>();
            submenu.Add(new MenuOption("0", "Go Back", typeof(GoBackCommand)));

            for (int i = 0; i < results.Count(); i++)
            {
                var assembly = results[i];
                submenu.Add(new MenuOption($"{i+1}", $"{assembly}", typeof(GetTypeMembersCommand), assembly));
            }

            submenuOptions = submenu;
        }
    }
}
