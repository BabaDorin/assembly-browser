using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.Application.Models;
using AssemblyBrowser.UI.Contracts;
using AssemblyBrowser.UI.Models.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models
{
    class MenuHandler
    {
        private Stack<Tuple<ICommand, object>> _actionsHistory;
        private List<ICommand> commands;
        readonly IAssemblyBrowser _assemblyBrowser;

        ICommand currentCommand = null;
        object currentParameter = null;
        IEnumerable<MenuOption> submenu = null;


        public MenuHandler()
        {
            _assemblyBrowser = new Application.Models.AssemblyBrowser();
            _actionsHistory = new();

            commands = new List<ICommand>()
            {
                new GetAssembliesCommand(_assemblyBrowser),
                new GetAssemblyTypesCommand(_assemblyBrowser),
                new GetTypeMembersCommand(_assemblyBrowser),
                new GetMemberInfoCommand(_assemblyBrowser),
                new GoBackCommand(this),
                new GetTypeHierarchyCommand(_assemblyBrowser)
            };
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();

                if(submenu == null)
                {
                    currentCommand = GetCommandOfType(typeof(GetAssembliesCommand));
                }

                currentCommand.Execute(currentParameter, out submenu);

                if (submenu == null || submenu.Count() == 0)
                    continue;

                foreach (var option in submenu)
                {
                    Console.WriteLine(option.DisplayName);
                }

                bool optionSelected = false;

                while (!optionSelected)
                {
                    try
                    {
                        Console.WriteLine();
                        Console.Write("Your option: ");
                        int optionIndex = int.Parse(Console.ReadLine());

                        if (optionIndex >= 0 && optionIndex < submenu.Count())
                        {
                            optionSelected = true;
                            var op = submenu.ElementAt(optionIndex);

                            if (op.CommandType != typeof(GoBackCommand))
                                _actionsHistory.Push(new Tuple<ICommand, object>(currentCommand, currentParameter));

                            currentParameter = op.Parameter;
                            currentCommand = GetCommandOfType(op.CommandType);
                        }
                    }
                    catch (Exception)
                    {
                        optionSelected = false;
                    }
                }   
            }
        }

        public void GoBack()
        {
            if (_actionsHistory.Count() == 0)
                return;

            var op = _actionsHistory.Pop();
            op.Item1.Execute(op.Item2, out submenu);
        }

        private ICommand GetCommandOfType(Type t)
        {
            return commands.FirstOrDefault(q => q.GetType() == t);
        }
    }
}
