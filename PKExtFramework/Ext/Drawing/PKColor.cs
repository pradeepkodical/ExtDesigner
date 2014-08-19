using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKColor
    {
        public int R { get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }
        public int A { get; private set; }
        
        public PKColor(int r, int g, int b, int a)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
    }
}
