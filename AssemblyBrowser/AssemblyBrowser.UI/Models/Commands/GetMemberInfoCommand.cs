using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetMemberInfoCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetMemberInfoCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            if (parameter == null || !(parameter is MemberInfo))
                throw new ArgumentException("Parameter is not of type: MemberInfo.", nameof(parameter));

            Console.WriteLine($"Member: {parameter}\n");

            PrintMemberInfoDetails((MemberInfo)parameter);

            var submenu = new List<MenuOption>();
            submenu.Add(new MenuOption("0", "Go Back", typeof(GoBackCommand)));
            submenuOptions = submenu;
        }

        private void PrintMemberInfoDetails(MemberInfo member)
        {
            var memberProperties = member.GetType().GetProperties();
            
            Console.WriteLine($"Details of member: {member.Name}");
            Console.WriteLine();

            foreach (var property in memberProperties)
            {
                Console.WriteLine($"{property.Name}:\t{property.GetValue(member)}");
            }
        }
    }
}
