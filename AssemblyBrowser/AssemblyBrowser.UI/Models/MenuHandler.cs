using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.Application.Models;
using AssemblyBrowser.UI.Contracts;
using AssemblyBrowser.UI.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models
{
    class MenuHandler
    {
        private Stack<ICommand> _commands;
        private IAssemblyBrowser _assemblyBrowser;

        public MenuHandler()
        {
            _commands = new Stack<ICommand>();
            _assemblyBrowser = new Application.Models.AssemblyBrowser(new CachingService(), new HierarchyGenerator());
        }

        public void Start()
        {
            PickAssemblyBrowseCommand(null);
        }

        public void PickAssemblyBrowseCommand(object parameter)
        {
            _commands.Push(new BrowseAssemblyCommand(_assemblyBrowser, this, parameter));
            _commands.Peek().Execute();
        }

        public void GoBack()
        {
            _commands.Pop();

            if (_commands.Count() == 0)
                Start();
            else
                _commands.Peek().Execute();
        }
    }
}
