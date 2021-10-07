using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.UI.Models
{
    public class MenuOption
    {
        public object Parameter { get; set; }
        public Type CommandType { get; set; }

        public MenuOption(object parameter, Type commandType)
        {
            Parameter = parameter;
            CommandType = commandType;
        }
    }
}
