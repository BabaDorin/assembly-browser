using AssemblyBrowser.Application.Models;
using System;

namespace AssemblyBrowser.Application.Contracts
{
    public interface IHierarchyGenerator
    {
        HierarchyItem GetTypeHierarchy(Type type);
    }
}