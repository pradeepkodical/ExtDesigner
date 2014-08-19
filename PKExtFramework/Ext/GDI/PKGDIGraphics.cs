using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PKExtFramework.Ext.GDI
{
    public class PKGDIGraphics: PKGraphics
    {
        private Graphics graphics;
        public PKGDIGraphics(Graphics graphics)
        {
            this.graphics = graphics;
        }

        private Brush FromBrush(PKBrush brush, int x, int y, int w, int h)
        {
            Brush b = null;
            if (brush is PKSolidBrush)
            {
                PKSolidBrush sb = brush as PKSolidBrush;
                b = new SolidBrush(Color.FromArgb(sb.Color.A, sb.Color.R, sb.Color.G, sb.Color.B));
            }
            else if (brush is PKLinearGradientBrush)
            {
                PKLinearGradientBrush lgb = brush as PKLinearGradientBrush;
                b = new LinearGradientBrush(new Rectangle(x, y, w, h),
                    Color.FromArgb(lgb.Color1.A, lgb.Color1.R, lgb.Color1.G, lgb.Color1.B),
                    Color.FromArgb(lgb.Color2.A, lgb.Color2.R, lgb.Color2.G, lgb.Color2.B), 90);
            }
            return b;
        }

        public override void FillRectangle(PKBrush brush, int x, int y, int w, int h, PKRectangleCorners corners)
        {
            if (h > 0 & w > 0)
            {
                var rect = PKRoundedRectangle.Create(x,y, w, h, 5, corners);

                using (Brush b = FromBrush(brush, x, y, w, h))
                {
                    graphics.FillPath(b, rect);
                    graphics.DrawPath(Pens.Black, rect);            
                }
            }
        }

        public override void FillRectangle(PKBrush brush, int x, int y, int w, int h)
        {
            if (h > 0 & w > 0)
            {
                using (Brush b = FromBrush(brush, x, y, w, h))
                {
                    graphics.FillRectangle(b, x, y, w, h);
                }
            }
        }

        public override void FillEllipse(PKBrush brush, int x, int y, int w, int h)
        {
            if (h > 0 & w > 0)
            {
                using (Brush b = FromBrush(brush, x, y, w, h))
                {
                    graphics.FillEllipse(b, x, y, w, h);
                }
            }
        }
        public override void DrawRectangle(PKPen pen, int x, int y, int w, int h, PKRectangleCorners corners)
        {
            if (h > 0 & w > 0)
            {
                var rect = PKRoundedRectangle.Create(x, y, w, h, 5, corners);

                using (Pen p = new Pen(Color.FromArgb(pen.Color.A, pen.Color.R, pen.Color.G, pen.Color.B), pen.Size))
                {
                    graphics.DrawPath(p, rect);
                }
            }
        }

        public override void DrawRectangle(PKPen pen, int x, int y, int w, int h)
        {
            if (h > 0 & w > 0)
            {
                using (Pen p = new Pen(Color.FromArgb(pen.Color.A, pen.Color.R, pen.Color.G, pen.Color.B), pen.Size))
                {
                    p.DashStyle = (DashStyle)pen.DashStyle;

                    graphics.DrawRectangle(p, x, y, w, h);
                }
            }
        }

        public override void DrawEllipse(PKPen pen, int x, int y, int w, int h)
        {
            if (h > 0 & w > 0)
            {
                using (Pen p = new Pen(Color.FromArgb(pen.Color.A, pen.Color.R, pen.Color.G, pen.Color.B), pen.Size))
                {
                    graphics.DrawEllipse(p, x, y, w, h);
                }
            }
        }

        public override void WriteText(
            string text,
            PKBrush brush,
            PKFont font,
            PKHAlign align, 
            int x, int y, int w, int h)
        {
            if (h > 0 & w > 0)
            {
                using (Brush b = FromBrush(brush, x, y, w, h))
                {
                    var f = new Font(font.Name, font.Size, font.Bold ? FontStyle.Bold : FontStyle.Regular);
                    var s = graphics.MeasureString(text, f, w);
                    if (align == PKHAlign.Left)
                    {
                        graphics.DrawString(text, f, b, x, y + (h - s.Height) / 2);
                    }
                    else if (align == PKHAlign.Right)
                    {
                        graphics.DrawString(text, f, b, x + w - s.Width, y + (h - s.Height) / 2);
                    }
                    else if (align == PKHAlign.Center)
                    {
                        graphics.DrawString(text, f, b, x + (w - s.Width) / 2, y + (h - s.Height) / 2);
                    }
                }
            }
        }
        public override int GetTextWidth(string text, PKFont font)
        {
            var f = new Font(font.Name, font.Size, font.Bold ? FontStyle.Bold : FontStyle.Regular);
            var s = graphics.MeasureString(text, f);
            return (int)s.Width;
        }
    }
}
