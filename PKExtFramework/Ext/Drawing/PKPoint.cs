using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PKExtFramework.Ext.Drawing
{
    public class PKPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PKPoint Clone()
        {
            return new PKPoint
            {
                X = this.X,
                Y = this.Y
            };
        }

        public PKPoint Add(int x, int y) 
        {
            this.X += x;
            this.Y += y;
            return this;
        }
        public PKPoint Add(PKPoint p)
        {
            this.X += p.X;
            this.Y += p.Y;
            return this;
        }
        public void Set(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
