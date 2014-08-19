using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PKExtFramework.Ext.Layout;
using PKExtFramework.Ext.Drawing;
using System.ComponentModel;
using PKExtFramework.Visitors;


namespace PKExtFramework.Ext.Component.Fields
{
    public class PKNumberField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKNumberField()
        {
            this.Name = "Number Field";            
            this.ExtTypeName = "Ext.form.NumberField";
        }

        public override void Accept(PKFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Render(PKGraphics graphics)
        {
            PKPoint absPosition = GetAbsPosition();
            base.Render(graphics);

            graphics.FillRectangle(PKBrushes.GrayWhiteBrush, absPosition.X + this.Padding, absPosition.Y + 2,
                    this.Width - this.Padding * 2, 20);

            graphics.DrawRectangle(
                    PKPens.DarkGrayPen,
                    absPosition.X + this.Padding,
                    absPosition.Y + 2,
                    this.Width - this.Padding * 2, 20);            
        }
    }
}
