using AssemblyBrowser.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.Application.Models
{
    public class HierarchyGenerator : IHierarchyGenerator
    {
        public HierarchyItem GetTypeHierarchy(Type type)
        {
            var hierarchy = GetUpperTypes(type);
            return hierarchy;
            //HierarchyItem lowerHierarchy = upperHierarchy; // <= the pointer that goes down the hierarchy from object to our type 
            //for (int i = hierarchy.Count - 2; i >= 0; i--)
            //{
            //    lowerHierarchy.Children.Add(new HierarchyItem(hierarchy[i]));
            //    lowerHierarchy = lowerHierarchy.Children[0];
            //}
        }

        private HierarchyItem GetUpperTypes(Type type)
        {
            List<Type> hierarchy = new List<Type>();
            var auxType = type;

            while (auxType != null)
            {
                hierarchy.Add(auxType);
                auxType = auxType.BaseType;
            }

            HierarchyItem upperHierarchy = new()
            {
                Type = hierarchy[hierarchy.Count - 1]
            };

            HierarchyItem lowerHierarchy = upperHierarchy; // <= the pointer that goes down the hierarchy from object to our type 
            for(int i = hierarchy.Count - 2; i >=0; i--)
            {
                lowerHierarchy.Children.Add(new HierarchyItem(hierarchy[i]));
                lowerHierarchy = lowerHierarchy.Children[0];
            }

            // Now we have the lowerHierarchy, which is our type
            // We send it further to the GetLowerTypes, which will append type's children and so on.

            GetChildTypes(type, ref lowerHierarchy);
            return upperHierarchy;
        }

        private void GetChildTypes(Type currentType, ref HierarchyItem hierarchy)
        {
            var childTypes = Assembly
                .GetEntryAssembly()
                .GetTypes()
                .Where(t => t.BaseType == currentType)
                .ToArray();

            if (childTypes == null || childTypes.Count() == 0)
                return;

            foreach (var type in childTypes)
            {
                HierarchyItem childHierarchy = new();
                childHierarchy.Type = type;
                hierarchy.Children.Add(childHierarchy);
                GetChildTypes(type, ref childHierarchy);
            }
        }
    }
}
