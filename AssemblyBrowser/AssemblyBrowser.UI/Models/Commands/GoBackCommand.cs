using AssemblyBrowser.UI.Contracts;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GoBackCommand : ICommand
    {
        readonly MenuHandler _menuHandler;

        public GoBackCommand(MenuHandler menuHandler)
        {
            _menuHandler = menuHandler;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            submenuOptions = null;
            _menuHandler.GoBack();
        }
    }
}
