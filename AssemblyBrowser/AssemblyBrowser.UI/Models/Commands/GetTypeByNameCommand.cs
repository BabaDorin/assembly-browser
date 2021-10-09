using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetTypeByNameCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetTypeByNameCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            Console.Write("Type name: ");
            string typeName = Console.ReadLine();

            var types = _assemblyBrowser.GetTypesByName(typeName);

            var submenu = new List<MenuOption>()
            {
                new MenuOption("0", "Go Back", typeof(GoBackCommand))
            };

            if (types != null)
            {
                for (int i = 0; i < types.Length; i++)
                {
                    submenu.Add(new MenuOption(
                        $"{i + 1}", 
                        $"{types[i].FullName}", 
                        typeof(GetTypeMembersCommand), 
                        types[i]));
                }
            }

            if(types == null)
                Console.WriteLine("The specified name does not match any type.\n");

            submenuOptions = submenu;
        }
    }
}
