using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetTypeHierarchyCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetTypeHierarchyCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            if (parameter == null || !(parameter is Type))
                throw new ArgumentException("Parameter is not of type: Type.", nameof(parameter));

            var hierarchy = _assemblyBrowser.GetTypeHierarchy((Type)parameter);

            int minLevel = hierarchy.Min(q => q.Item1);
            int maxLevel = hierarchy.Max(q => q.Item1);

            for(int i = minLevel; i <= maxLevel; i++)
            {
                hierarchy
                    .Where(q => q.Item1 == i)
                    .Select(q => q.Item2)
                    .ToList()
                    .ForEach(q => DisplayType(q));

                Console.WriteLine();
            }

            submenuOptions = new List<MenuOption>()
            {
                new MenuOption("0: Go Back", "Go back", typeof(GoBackCommand))
            };
        }

        private void DisplayType(Type q)
        {
            Console.Write(q + "   ");
        }
    }
}
