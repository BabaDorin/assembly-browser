using System;

namespace AssemblyBrowser.UI.Models
{
    public class MenuOption
    {
        public string DisplayName { get; set; }
        public object Parameter { get; set; }
        public Type CommandType { get; set; }

        public MenuOption(string displayName, object parameter, Type commandType)
        {
            DisplayName = displayName;
            Parameter = parameter;
            CommandType = commandType;
        }
    }
}
