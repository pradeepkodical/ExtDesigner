using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;
using System.Drawing.Design;
using System.ComponentModel.Design;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Component.Fields;
using PKExtFramework.Ext.GDI;

namespace PKExtFramework.Ext.Component.Tab
{
    public class PKTabButton : PKText
    {    
        /// <summary>
        /// 
        /// </summary>
        public PKTabButton()
        {
            this.Name = "Button";
            this.ExtTypeName = "Ext.Button";

            Value = string.Empty;            
            Align = PKHAlign.Center;

            this.Padding = 5;

            this.BorderPen = PKPens.BorderPen;
            this.TextBrush = PKBrushes.SteelBlueBrush;
            this.TextFont = PKFonts.LabelFont;
            this.BackgroundBrush = PKBrushes.ButtonBackgroundBrush;
            this.Corners = PKRectangleCorners.TopLeft | PKRectangleCorners.TopRight;
        }

        public PKBoxItem TabPage { get; set; }

        public override void Render(PKGraphics graphics)
        {
            base.Render(graphics);

            if (IsSelected)
            {
                PKPoint absPosition = GetAbsPosition();

                graphics.DrawRectangle(
                    PKPens.ActiveItem,
                    absPosition.X, absPosition.Y,
                    this.Width, this.Height, this.Corners);
            }
        }

        public override void Accept(PKVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
