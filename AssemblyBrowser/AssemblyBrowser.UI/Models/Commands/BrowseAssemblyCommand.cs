using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models.Commands
{
    class BrowseAssemblyCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;
        MenuHandler _menuHandler;
        object _parameter;

        public BrowseAssemblyCommand(IAssemblyBrowser assemblyBrowser, MenuHandler menuHandler, object parameter)
        {
            _assemblyBrowser = assemblyBrowser;
            _menuHandler = menuHandler;
            _parameter = parameter;
        }

        public void Execute()
        {
            Console.Clear();

            object[] results = null;

            switch (_parameter)
            {
                case null:
                    results = _assemblyBrowser.GetApplicationAssemblies();
                    break;
                case Assembly a:
                    results = _assemblyBrowser.GetAssemblyTypes(a);
                    break;
                case Type t:
                    results = _assemblyBrowser.GetTypeMembersInfo(t);
                    break;
                default: throw new ArgumentException("Unsuported parameter value", nameof(_parameter));
            }

            Console.WriteLine("0: <= go back");
            Console.WriteLine();

            for (int i = 1; i <= results.Length; i++)
            {
                Console.WriteLine($"{i}: {results[i-1]}");
            }

            bool repeat = true;
            while (repeat)
            {
                Console.Write("Your option: ");
                int option = 0;

                if(int.TryParse(Console.ReadLine(), out option) && option >= 0 && option < results.Length)
                {
                    repeat = false;

                    if (option == 0)
                        _menuHandler.GoBack();
                    else
                        _menuHandler.PickAssemblyBrowseCommand(results[option - 1]);
                }
            }
        }

        public void Unexecute()
        {
            throw new NotImplementedException();
        }
    }
}
