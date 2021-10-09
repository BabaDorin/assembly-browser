using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;

namespace AssemblyBrowser.UI.Models.Commands
{
    class StartAppCommand : ICommand
    {
        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            Console.WriteLine(@"  __    __   __   ____  _      ___   _     _         ___   ___   ___   _       __   ____  ___  ");
            Console.WriteLine(@" / /\  ( (` ( (` | |_  | |\/| | |_) | |   \ \_/     | |_) | |_) / / \ \ \    /( (` | |_  | |_) ");
            Console.WriteLine(@"/_/--\ _)_) _)_) |_|__ |_|  | |_|_) |_|__  |_|      |_|_) |_| \ \_\_/  \_\/\/ _)_) |_|__ |_| \ ");
            Console.WriteLine("------------------------------------------------------------------------------------------------\n");

            submenuOptions = new List<MenuOption>()
            {
                new MenuOption("exit", "Exit", typeof(ExitCommand)),
                new MenuOption("1", "Search type by name", typeof(GetTypeByNameCommand)),
                new MenuOption("2", "Browse assemblies", typeof(GetAssembliesCommand))
            };
        }
    }
}
