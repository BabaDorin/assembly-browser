using AssemblyBrowser.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssemblyBrowser.Application.Models
{
    public class HierarchyGenerator : IHierarchyGenerator
    {
        public HierarchyItem GetTypeHierarchy(Type type)
        {
            var hierarchy = GetUpperTypes(type);
            return hierarchy;
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
                Type = hierarchy[hierarchy.Count - 1],
                RelationWithReference = Relation.Ancestor
            };

            HierarchyItem lowerHierarchy = upperHierarchy; // <= the pointer that goes down the hierarchy from object to our type 
            for(int i = hierarchy.Count - 2; i >=0; i--)
            {
                lowerHierarchy.Children.Add(new HierarchyItem(hierarchy[i], Relation.Ancestor));
                lowerHierarchy = lowerHierarchy.Children[0];
            }

            // Now we have the lowerHierarchy, which is our type
            lowerHierarchy.RelationWithReference = Relation.TheOne;

            // We send it further to the GetLowerTypes, which will append type's children and so on.
            GetChildTypes(type, ref lowerHierarchy);
            return upperHierarchy;
        }

        private void GetChildTypes(Type currentType, ref HierarchyItem hierarchy)
        {
            var childTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(q => q.GetTypes()
                .Where(t => t.BaseType == currentType)
                .ToArray());

            if (childTypes == null || childTypes.Count() == 0)
                return;

            foreach (var type in childTypes)
            {
                HierarchyItem childHierarchy = new();
                childHierarchy.Type = type;
                childHierarchy.RelationWithReference = Relation.Descendent;
                hierarchy.Children.Add(childHierarchy);
                GetChildTypes(type, ref childHierarchy);
            }
        }
    }
}
