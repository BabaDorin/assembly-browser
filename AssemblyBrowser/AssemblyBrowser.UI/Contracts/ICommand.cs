using AssemblyBrowser.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Contracts
{
    interface ICommand
    {
        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions);
    }
}
