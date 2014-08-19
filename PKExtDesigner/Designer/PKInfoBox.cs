using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Component;
using System.Drawing;
using PKExtFramework.Ext.Drawing;
using PKExtFramework.Ext.Layout;
using System.Drawing.Drawing2D;
using PKExtFramework.Ext.GDI;

namespace PKExtDesigner.Designer
{
    internal class PKInfoBox
    {
        private PKBoxItem currentItem;
        
        private int boxWidth;
        private int boxHeight;

        public void SetItem(PKBoxItem item)
        {
            this.currentItem = item;
        }

        public void Render(Graphics graphics)
        {
            //RenderInternal(graphics);
        }
        private void RenderInternal(Graphics graphics)
        {            
            if (currentItem != null)
            {
                using (Font textFont = new Font("Tahoma", 8))
                {
                    string strSize = string.Format(" x:{0}, y:{1}, w:{2}, h:{3} ", 
                        currentItem.X, currentItem.Y, currentItem.Width, currentItem.Height);
                    var size = graphics.MeasureString(strSize, textFont);
                    size.Height += 2;

                    Rectangle rect = new Rectangle(
                        this.boxWidth - (int)size.Width - 2,
                        this.boxHeight - (int)size.Height,
                        (int)size.Width, 
                        (int)size.Height);
                    rect.Inflate(2, 3);
                    rect.Y -= 4;
                    rect.X -= 1;

                    DrawSizeInfoBackground(graphics, rect);

                    StringFormat sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    graphics.DrawString(strSize, textFont, Brushes.Black, rect, sf);
                }
            }            
        }

        private void DrawSizeInfoBackground(Graphics graphics, Rectangle rect)
        {
            var x= PKRoundedRectangle.Create(rect, (int)(rect.Height / 2), 
                PKRectangleCorners.None);
            using (Brush brush = new LinearGradientBrush(rect, Color.White, Color.Yellow,
                LinearGradientMode.Vertical))
            {
                graphics.FillPath(brush, x);
                graphics.DrawPath(Pens.Black, x);            
            }            
        }

        internal void SetPageSize(int w, int h)
        {
            this.boxWidth = w;
            this.boxHeight = h;
        }
    }
}
