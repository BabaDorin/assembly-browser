using System;

namespace AssemblyBrowser.Application.Contracts
{
    public interface IHierarchyGenerator
    {
        Tuple<int, Type>[] GetTypeHierarchy(Type type);
    }
}