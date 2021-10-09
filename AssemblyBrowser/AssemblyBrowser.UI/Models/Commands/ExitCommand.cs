using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Models.Commands
{
    class ExitCommand : ICommand
    {
        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            submenuOptions = null;
            Environment.Exit(0);
        }
    }
}
