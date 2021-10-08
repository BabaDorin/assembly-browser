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

        // int => level (-1 - baseclass, 0 - class itself, 1 - child class etc)
        HierarchyItem GetTypeHierarchy(Type type); 
    }
}
