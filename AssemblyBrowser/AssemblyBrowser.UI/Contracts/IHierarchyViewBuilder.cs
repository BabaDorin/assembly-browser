using AssemblyBrowser.Application.Models;

namespace AssemblyBrowser.UI.Contracts
{
    interface IHierarchyViewBuilder
    {
        void DisplayHierarchy(HierarchyItem hierarchyItem);
    }
}