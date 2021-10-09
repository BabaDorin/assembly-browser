using AssemblyBrowser.Application.Contracts;
using System;
using System.Linq;
using System.Reflection;

namespace AssemblyBrowser.Application.Models
{
    public class AssemblyBrowser : IAssemblyBrowser
    {
        private ICachingService _cachingService;
        private IHierarchyGenerator _hierarchyGenerator;

        public AssemblyBrowser(ICachingService cachingService, IHierarchyGenerator hierarchyGenerator)
        {
            _cachingService = cachingService;
            _hierarchyGenerator = hierarchyGenerator;
        }

        public AssemblyBrowser()
            : this(new CachingService(), new HierarchyGenerator())
        {

        }

        public Assembly[] GetApplicationAssemblies()
        {
            return (Assembly[])_cachingService.GetOrSearch(
                "application_assemblies",
                () => AppDomain.CurrentDomain.GetAssemblies());
        }

        public Type[] GetAssemblyTypes(Assembly assembly)
        {
            return (Type[])_cachingService.GetOrSearch(
                assembly.FullName,
                () => assembly.GetTypes());
        }

        public MemberInfo[] GetTypeMembersInfo(Type type)
        {
            return ((MemberInfo[])_cachingService.GetOrSearch(
                type.FullName,
                () => type.GetMembers()));
        }

        public HierarchyItem GetTypeHierarchy(Type type)
        {
            return (HierarchyItem)_cachingService.GetOrSearch(
                type.FullName + "_hierarchy",
                () => _hierarchyGenerator.GetTypeHierarchy(type));
        }

        public Type[] GetTypesByName(string typeName)
        {
            return (Type[])_cachingService.GetOrSearch(
                typeName,
                () => AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(q => q.GetTypes())
                    .Where(q => q.FullName.ToLower().Contains(typeName.ToLower()))
                    ?.ToArray());
        }
    }
}
