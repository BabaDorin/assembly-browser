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
        public Tuple<int, Type>[] GetTypeHierarchy(Type type)
        {
            return GetLowerTypes(type)
                .Concat(GetUpperTypes(type))
                .ToArray();
        }

        private List<Tuple<int, Type>> GetUpperTypes(Type type)
        {
            List<Tuple<int, Type>> upperTypes = new();

            var auxType = type;
            int currentLevel = 0;

            while (auxType != null)
            {
                upperTypes.Add(new Tuple<int, Type>(currentLevel--, auxType));
                auxType = auxType.BaseType;
            }

            return upperTypes ?? new List<Tuple<int, Type>>();
        }

        private List<Tuple<int, Type>> GetLowerTypes(Type type)
        {
            List<Tuple<int, Type>> lowerTypes = new();
            int currentLevel = 1;

            GetChildTypes(type, currentLevel, ref lowerTypes);

            return lowerTypes ?? new List<Tuple<int, Type>>();
        }

        private void GetChildTypes(Type currentType, int currentLevel, ref List<Tuple<int, Type>> hierarchy)
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
                hierarchy.Add(new Tuple<int, Type>(currentLevel, type));
            }

            foreach (var type in childTypes)
            {
                GetChildTypes(type, currentLevel + 1, ref hierarchy);
            }
        }
    }
}
