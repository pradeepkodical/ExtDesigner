using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKFont
    {
        public PKFont(string name, int size, bool bold)
        {
            this.Name = name;
            this.Size = size;
            this.Bold = bold;
        }
        public string Name { get; private set; }
        public bool Bold { get; private set; }
        public int Size { get; private set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}", this.Name, this.Bold ? "Bold" : "Regular", this.Size);
        }
    }
}
