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
    internal enum PKResizeDirection
    {
        LeftRight,
        TopBottom
    }
    internal class PKBoxResizer
    {
        private int SIZE = 5;
        private PKBoxItem currentItem = null;

        private PKPoint lastPoint = new PKPoint();

        private Rectangle boxRect = new Rectangle();
        private Rectangle resizeRect = new Rectangle();

        private bool hide;
        private bool canResize;
        public bool MouseHit{get; private set;}
        public bool IsVisible { get; set; }

        private PKResizeDirection direction;

        public event Action AfterMouseMove;

        private Type GetParentLayout(PKBoxItem item)
        {
            if (item.Parent != null && item.Parent.Layout != null)
            {
                return item.Parent.Layout.GetType();
            }
            return null;
        }

        public void Render(Graphics graphics)
        {
            if (!hide && currentItem != null)
            {
                if (canResize)
                {
                    DrawResizer(graphics);                    
                }
                DrawSizeInfo(graphics);
            }
        }

        private void DrawSizeInfo(Graphics graphics)
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
                        (int)(boxRect.X + boxRect.Width - size.Width), 
                        (int)(boxRect.Y - size.Height), 
                        (int)size.Width, (int)size.Height);
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
                PKRectangleCorners.TopRight);
            using (Brush brush = new LinearGradientBrush(rect, Color.White, 
                MouseHit?Color.Yellow:Color.LightYellow,
                LinearGradientMode.Vertical))
            {
                graphics.FillPath(brush, x);
                graphics.DrawPath(Pens.Black, x);            
            }            
        }

        private void DrawResizer(Graphics graphics)
        {
            GraphicsPath path;

            if (direction == PKResizeDirection.TopBottom)
            {
                path = PKRoundedRectangle.Create(resizeRect, SIZE, 
                    PKRectangleCorners.TopLeft | PKRectangleCorners.TopRight);
            }
            else
            {
                path = PKRoundedRectangle.Create(resizeRect, SIZE, 
                    PKRectangleCorners.TopLeft | PKRectangleCorners.BottomLeft);
            }

            using (Brush brush = new LinearGradientBrush(resizeRect, Color.White, 
                MouseHit?Color.Yellow:Color.LightYellow, 
                direction== PKResizeDirection.TopBottom?90:0))
            {
                graphics.FillPath(brush, path);
                graphics.DrawPath(Pens.Black, path);
            }

            Rectangle rect = resizeRect;
            
            rect.Inflate(-4, -4);
            graphics.DrawRectangle(Pens.Black, rect);
        }
                
        public void SetItem(PKBoxItem item)
        {
            currentItem = item;
            canResize = false;
            hide = true;
            if (item != null)
            {
                hide = false;

                UpdateBox();
            }
        }

        public void OnMouseDown(int x, int y)
        {
            if (canResize && currentItem != null)
            {
                if (resizeRect.Contains(x, y))
                {
                    MouseHit = true;
                    lastPoint.Set(x, y);
                    lastPoint.Add(-resizeRect.X, -resizeRect.Y);
                }
            }
        }

        public void OnMouseUp(int x, int y)
        {
            SetItem(currentItem);
            MouseHit = false;
        }

        public void OnMouseMove(int x, int y)
        {
            if (MouseHit && canResize)
            {
                if (currentItem != null)
                {
                    currentItem.Flex = 0;
                    if(direction == PKResizeDirection.LeftRight)
                    {                        
                        currentItem.Width = (x - lastPoint.X) - boxRect.X + resizeRect.Width;
                    }
                    else
                    {
                        currentItem.Height = (y - lastPoint.Y) - boxRect.Y + resizeRect.Height;                        
                    }
                    if (currentItem.Width <= 0) currentItem.Width = 0;
                    if (currentItem.Height <= 0) currentItem.Height = 0;

                    if (this.AfterMouseMove != null)
                    {
                        this.AfterMouseMove();
                    }
                    UpdateBox();
                }
            }
        }

        private void UpdateBox()
        {
            var layout = GetParentLayout(currentItem);

            PKPoint pos = currentItem.GetAbsPosition();
            boxRect.X = pos.X;
            boxRect.Y = pos.Y;

            boxRect.Width = currentItem.Width;
            boxRect.Height = currentItem.Height;

            if (layout == typeof(PKHBox))
            {
                canResize = true;
                direction = PKResizeDirection.LeftRight;
                resizeRect.X = boxRect.X + boxRect.Width - 3 * SIZE;
                resizeRect.Y = boxRect.Y + (boxRect.Height - 6 * SIZE) / 2;
                resizeRect.Width = 3 * SIZE;
                resizeRect.Height = 6 * SIZE;
            }
            if (layout == typeof(PKVBox))
            {
                canResize = true;
                direction = PKResizeDirection.TopBottom;
                resizeRect.X = boxRect.X + (boxRect.Width - 6 * SIZE) / 2;
                resizeRect.Y = boxRect.Y + boxRect.Height - 3 * SIZE;
                resizeRect.Width = 6 * SIZE;
                resizeRect.Height = 3 * SIZE;
            }
        }
    }
}
