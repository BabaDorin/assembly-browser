using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using AssemblyBrowser.UI.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowser.UI.Models
{
    class MenuHandler
    {
        readonly IAssemblyBrowser _assemblyBrowser;
        readonly IHierarchyViewBuilder _hierarchyViewBuilder;

        private Stack<Tuple<ICommand, object>> _commandHistory;
        private List<ICommand> _availableCommands;
        
        public MenuHandler()
        {
            _assemblyBrowser = new Application.Models.AssemblyBrowser();
            _hierarchyViewBuilder = new HierarchyViewBuilder();
            _commandHistory = new();

            _availableCommands = new List<ICommand>()
            {
                new StartAppCommand(),
                new ExitCommand(),
                new GetAssembliesCommand(_assemblyBrowser),
                new GetAssemblyTypesCommand(_assemblyBrowser),
                new GetTypeMembersCommand(_assemblyBrowser),
                new GetMemberInfoCommand(_assemblyBrowser),
                new GoBackCommand(this),
                new GetTypeHierarchyCommand(_assemblyBrowser, _hierarchyViewBuilder),
                new GetTypeByNameCommand(_assemblyBrowser),
            };
        }

        public void Start()
        {
            // It starts with StartAppCommand, then it follows a kind of
            // composite-like behavior where each command builds it's own submenu.

            ICommand currentCommand = GetCommandOfType(typeof(StartAppCommand));
            object currentParameter = null;
            IEnumerable<MenuOption> submenu = null;

            while (true)
            {
                Console.Clear();

                if(!(currentCommand is GoBackCommand))
                    RememberCommand(currentCommand, currentParameter);

                currentCommand.Execute(currentParameter, out submenu);

                // If there is any submenu, then continue with displaying it 
                if (submenu == null || submenu.Count() == 0)
                    continue;

                DisplayMenuOptions(submenu);
                MenuOption selectedOption = PickAnOption(submenu);
                ICommand selectedCommand = GetCommandOfType(selectedOption.CommandType);
                
                currentCommand = selectedCommand;
                currentParameter = selectedOption.Parameter;
            }
        }

        private void RememberCommand(ICommand currentCommand, object currentParameter)
        {
            _commandHistory.Push(new Tuple<ICommand, object>(currentCommand, currentParameter));
        }

        private void DisplayMenuOptions(IEnumerable<MenuOption> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Label}:\t{option.DisplayName}");
            }
        }
        
        private MenuOption PickAnOption(IEnumerable<MenuOption> options)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write("Your option: ");
                string input = Console.ReadLine();

                var option = options
                    .FirstOrDefault(q => q.Label.ToLower() == input.ToLower());

                if (option == null)
                {
                    Console.WriteLine("Invalid option. Please, try again.");
                    continue;
                }

                return option;
            }
        }

        public void GoBack(out IEnumerable<MenuOption> previousSubmenu)
        {
            if(_commandHistory.Count() < 2)
            {
                throw new Exception("The operation can not be performed due to an empty history of commands");
            }

            // Pop the current command and peek the previous one.
            _commandHistory.Pop();
            var op = _commandHistory.Peek();
            op.Item1.Execute(op.Item2, out previousSubmenu);
        }

        private ICommand GetCommandOfType(Type t)
        {
            return _availableCommands.FirstOrDefault(q => q.GetType() == t);
        }
    }
}
