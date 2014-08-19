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
    public class PKDateField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKDateField()
        {
            this.Name = "Date Field";            
            this.ExtTypeName = "Ext.form.DateField";
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
            graphics.DrawRectangle(
                    PKPens.DarkGrayPen,
                    absPosition.X - this.Padding + this.Width - 18,
                    absPosition.Y + 2,
                    18, 20);
            graphics.WriteText("[]", PKBrushes.BlackBrush, this.TextFont, PKHAlign.Center,
                    absPosition.X - this.Padding + this.Width - 18,
                    absPosition.Y + 2,
                    18, 20);
        }
    }
}
