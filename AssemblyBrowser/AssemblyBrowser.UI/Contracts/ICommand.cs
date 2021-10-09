using AssemblyBrowser.UI.Models;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Contracts
{
    interface ICommand
    {
        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions);
    }
}
