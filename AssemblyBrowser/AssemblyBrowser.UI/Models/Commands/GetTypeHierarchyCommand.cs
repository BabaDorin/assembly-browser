﻿using AssemblyBrowser.Application.Contracts;
using AssemblyBrowser.Application.Models;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyBrowser.UI.Models.Commands
{
    class GetTypeHierarchyCommand : ICommand
    {
        private IAssemblyBrowser _assemblyBrowser;

        public GetTypeHierarchyCommand(IAssemblyBrowser assemblyBrowser)
        {
            _assemblyBrowser = assemblyBrowser;
        }

        public void Execute(object parameter, out IEnumerable<MenuOption> submenuOptions)
        {
            var hierarchy = _assemblyBrowser.GetTypeHierarchy((Type)parameter);
            DisplayHierarchy(hierarchy, 0, new List<int>());
            submenuOptions = new List<MenuOption>()
            {
                new MenuOption("0", "Go Back", typeof(GoBackCommand))
            };
        }

        private void DisplayHierarchy(HierarchyItem hierarchyItem, int depthLevel, List<int> drawLines)
        {
            StringBuilder line = new();
            DrawLines(depthLevel, drawLines, line);

            if (depthLevel > 0 && line[line.Length - 1] != '|')
                line[line.Length - 1] = '|';

            var lineContent = line.ToString() + "-- " + hierarchyItem.Type.Name;

            if (hierarchyItem.RelationWithReference == Relation.TheOne)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(lineContent);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine(lineContent);
            }

            if (hierarchyItem.Children.Count() > 1)
                drawLines.Add(depthLevel);

            for (int i = 0; i < hierarchyItem.Children.Count; i++)
            {
                if (i == hierarchyItem.Children.Count - 1)
                {
                    drawLines.Remove(depthLevel);
                }

                DisplayHierarchy(hierarchyItem.Children[i], depthLevel + 1, drawLines);
            }
        }

        private static void DrawLines(int depthLevel, List<int> drawLines, StringBuilder line)
        {
            for (int i = 0; i < depthLevel; i++)
            {
                if (drawLines.Exists(q => q == i))
                    line.Append("   |");
                else
                    line.Append("    ");
            }
        }
    }
}
