using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetTypeHierarchyCommand : ICommand
    {
        readonly IAssemblyBrowser _assemblyBrowser;
        readonly IHierarchyViewBuilder _hierarchyViewBuilder;

        public GetTypeHierarchyCommand(IAssemblyBrowser assemblyBrowser, IHierarchyViewBuilder hierarchyViewBuilder)
        {
            _assemblyBrowser = assemblyBrowser;
            _hierarchyViewBuilder = hierarchyViewBuilder;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            var hierarchy = _assemblyBrowser.GetTypeHierarchy((Type)parameter);

            _hierarchyViewBuilder.DisplayHierarchy(hierarchy);

            submenuOptions = new List<MenuOption>()
            {
                new MenuOption("0", "Go Back", typeof(GoBackCommand))
            };
        }
    }
}
