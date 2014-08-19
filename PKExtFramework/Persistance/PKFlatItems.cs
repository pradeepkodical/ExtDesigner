using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;

namespace PKExtFramework.Persistance
{
    public class PKFlatColor
    {
        public int R { get; set; }
        public int B { get; set; }
        public int G { get; set; }
        public int A { get; set; }
    }
    public class PKFlatPen
    {
        public PKFlatColor Color { get; set; }
        public int Size { get; set; }
    }
    public class PKFlatBrush
    {
        public PKFlatColor[] Colors { get; set; }
    }
    public class PKFlatFont
    {
        public string Name { get; set; }
        public bool Bold { get; set; }
        public int Size { get; set; }
    }
    /// <summary>
    /// Object representing the flat object for all designer elements
    /// </summary>
    public class PKFlatItem
    {
        /// <summary>
        /// 
        /// </summary>
        public PKBoxItem BoxItem { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PKFlatItem ParentItem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ComponentFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsComponent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LayoutName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Padding { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Flex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int HAlign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Value { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string DataProperty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayMember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ValueMember { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Text { get; set; }
    }
}
