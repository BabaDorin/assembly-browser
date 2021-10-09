using AssemblyBrowser.Application.Models;
using System;
using System.Reflection;

namespace AssemblyBrowser.Application.Contracts
{
    public interface IAssemblyBrowser
    {
        Assembly[] GetApplicationAssemblies();
        Type[] GetAssemblyTypes(Assembly assembly);
        MemberInfo[] GetTypeMembersInfo(Type type);
        Type[] GetTypesByName(string typeName);
        HierarchyItem GetTypeHierarchy(Type type); 
    }
}
