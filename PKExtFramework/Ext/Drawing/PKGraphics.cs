using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Drawing
{
    public abstract class PKGraphics
    {
        public abstract void FillRectangle(PKBrush brush, int x, int y, int w, int h, PKRectangleCorners corners);
        public abstract void FillRectangle(PKBrush brush, int x, int y, int w, int h);
        public abstract void FillEllipse(PKBrush brush, int x, int y, int w, int h);

        public abstract void DrawRectangle(PKPen pen, int x, int y, int w, int h, PKRectangleCorners corners);
        public abstract void DrawRectangle(PKPen pen, int x, int y, int w, int h);
        public abstract void DrawEllipse(PKPen pen, int x, int y, int w, int h);
        public abstract void WriteText(
            string text,
            PKBrush brush,
            PKFont font,
            PKHAlign align,
            int x, int y, int w, int h);        
        public abstract int GetTextWidth(string p, PKFont pKFont);        
    }
}
