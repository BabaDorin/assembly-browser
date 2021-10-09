using AssemblyBrowser.Application.Models;
using AssemblyBrowser.UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AssemblyBrowser.UI.Models
{
    class HierarchyViewBuilder : IHierarchyViewBuilder
    {
        public void DisplayHierarchy(HierarchyItem hierarchyItem)
        {
            BuildHierarchyViewRecursive(hierarchyItem, 0, new List<int>());
        }

        private void BuildHierarchyViewRecursive(HierarchyItem hierarchyItem, int depthLevel, List<int> drawLines)
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

                BuildHierarchyViewRecursive(hierarchyItem.Children[i], depthLevel + 1, drawLines);
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
