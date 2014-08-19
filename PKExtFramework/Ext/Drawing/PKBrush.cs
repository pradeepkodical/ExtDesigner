using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKBrush
    {
        public int Size { get; protected set; }
    }

    public class PKSolidBrush : PKBrush
    {
        public PKSolidBrush(PKColor color, int size) 
        {
            this.Color = color;
            this.Size = size;
        }
        public PKColor Color { get; private set; }

        public override string ToString()
        {
            return "Solid Brush";
        }
    }

    public class PKLinearGradientBrush : PKBrush
    {
        public PKLinearGradientBrush(PKColor startColor, PKColor endColor, int size) 
        {
            this.Color1 = startColor;
            this.Color2 = endColor;
            this.Size = size;
        }
        public PKColor Color1 { get; private set; }
        public PKColor Color2 { get; private set; }

        public override string ToString()
        {
            return "Gradient Brush";
        }
    }
}
