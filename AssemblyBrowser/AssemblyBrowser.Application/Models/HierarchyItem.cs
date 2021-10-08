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
        public Relation RelationWithReference { get; set; }

        public HierarchyItem(Type type)
        {
            Type = type;
        }

        public HierarchyItem(Type type, Relation relationWithReference)
        {
            Type = type;
            RelationWithReference = relationWithReference;
        }

        public HierarchyItem()
        {

        }
    }

    public enum Relation
    {
        Ancestor, // Classes from Object to the type itself 
        TheOne, // The type itself
        Descendent, // Types starting from type's direct child and ending with it's last descendent
    }
}
