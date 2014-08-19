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
    public class PKTextAreaField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKTextAreaField()
        {
            this.Name = "Text Field";
            this.ExtTypeName = "Ext.form.TextArea";
        }

        public override void Accept(PKFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Render(PKGraphics graphics)
        {
            PKPoint absPosition = GetAbsPosition();
            base.Render(graphics);

            graphics.FillRectangle(PKBrushes.GrayWhiteBrush, 
                    absPosition.X + this.Padding,
                    absPosition.Y + this.Padding,
                    this.Width - this.Padding * 2, 
                    this.Height-this.Padding * 2);

            graphics.DrawRectangle(
                    PKPens.DarkGrayPen,
                    absPosition.X + this.Padding,
                    absPosition.Y + this.Padding,
                    this.Width - this.Padding * 2, 
                    this.Height - this.Padding * 2);            
        }
    }
}
