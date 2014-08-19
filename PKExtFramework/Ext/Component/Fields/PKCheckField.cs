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
    public class PKCheckField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKCheckField()
        {
            this.Name = "Check Field";            
            this.ExtTypeName = "Ext.form.Checkbox";
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
                    18, 18);
            graphics.DrawRectangle(
                    PKPens.DarkGrayPen,
                    absPosition.X + this.Padding, 
                    absPosition.Y + 2,
                    18, 18);            
        }
    }
}
