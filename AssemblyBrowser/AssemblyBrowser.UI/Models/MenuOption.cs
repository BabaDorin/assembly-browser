using System;

namespace AssemblyBrowser.UI.Models
{
    public class MenuOption
    {
        // The string that user has to type in order to select a specific option
        public string Label { get; set; }
        public string DisplayName { get; set; }
        public object Parameter { get; set; }
        public Type CommandType { get; set; }

        public MenuOption(string label, string displayName, Type commandType, object parameter = null)
        {
            Label = label;
            DisplayName = displayName;
            Parameter = parameter;
            CommandType = commandType;
        }
    }
}
