using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyBrowser.Application.Models
{
    public class HierarchyItem
    {
        public Type Type { get; set; }
        public List<HierarchyItem> Children { get; set; } = new List<HierarchyItem>();

        public HierarchyItem(Type type)
        {
            Type = type;
        }

        public HierarchyItem()
        {

        }
    }
}
