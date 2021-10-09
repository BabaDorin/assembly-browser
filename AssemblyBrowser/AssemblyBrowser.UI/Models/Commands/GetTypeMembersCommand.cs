using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetTypeMembersCommand : ICommand
    {
        readonly IAssemblyBrowser _assemblyBrowser;

        public GetTypeMembersCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            if (parameter == null || !(parameter is Type))
                throw new ArgumentException("Parameter is not of type: Assembly.", nameof(parameter));

            var results = _assemblyBrowser.GetTypeMembersInfo((Type)parameter);

            var submenu = new List<MenuOption>();
            submenu.Add(new MenuOption("0", "Go Back", typeof(GoBackCommand)));
            submenu.Add(new MenuOption("1", "View type hierarchy", typeof(GetTypeHierarchyCommand), parameter));

            for (int i = 0; i < results.Length; i++)
            {
                var member = results[i];
                submenu.Add(new MenuOption(
                    $"{i + 2}",
                    $"{GetMemberNature(member)}\t\t{member}",
                    typeof(GetMemberInfoCommand),
                    member));
            }

            submenuOptions = submenu;
        }

        /// <summary>
        /// Determines the nature of a memberinfo object
        /// </summary>
        /// <returns>Either "Property" or "Method", depending on member's nature</returns>
        private string GetMemberNature(MemberInfo member)
        {
            return member is PropertyInfo
                ? "Property"
                : "Method";
        }
    }
}
