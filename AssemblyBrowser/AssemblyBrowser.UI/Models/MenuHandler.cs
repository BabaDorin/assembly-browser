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

        private Stack<Tuple<ICommand, object>> _commandHistory;
        private List<ICommand> availableCommands;
        
        public MenuHandler()
        {
            _assemblyBrowser = new Application.Models.AssemblyBrowser();
            _commandHistory = new();

            availableCommands = new List<ICommand>()
            {
                new StartAppCommand(),
                new ExitCommand(),
                new GetAssembliesCommand(_assemblyBrowser),
                new GetAssemblyTypesCommand(_assemblyBrowser),
                new GetTypeMembersCommand(_assemblyBrowser),
                new GetMemberInfoCommand(_assemblyBrowser),
                new GoBackCommand(this),
                new GetTypeHierarchyCommand(_assemblyBrowser),
                new GetTypeByNameCommand(_assemblyBrowser),
            };
        }

        public void Start()
        {
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

        public void GoBack(ref IEnumerable<MenuOption> previousSubmenu)
        {
            if (_commandHistory.Count() == 0)
                return;

            //_commandHistory.Pop(); // Pop current command
            _commandHistory.Pop(); // Pop previous command
            var op = _commandHistory.Peek();
            op.Item1.Execute(op.Item2, out previousSubmenu);
        }

        private ICommand GetCommandOfType(Type t)
        {
            return availableCommands.FirstOrDefault(q => q.GetType() == t);
        }
    }
}
