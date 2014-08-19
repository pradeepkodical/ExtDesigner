using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKPen
    {
        public PKPen(PKColor color, int size) 
        {
            this.Color = color;
            this.Size = size;
            this.DashStyle = 0;
        }
        public PKColor Color { get; private set; }
        public int Size { get; private set; }
        public int DashStyle { get; set; }
        public override string ToString()
        {
            return "Pen";
        }
    }
}
