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
    public class PKRadioField : PKFieldItem
    {        
        /// <summary>
        /// 
        /// </summary>
        public PKRadioField()
        {
            this.Name = "Radio Field";            
            this.ExtTypeName = "Ext.form.Radio";
        }

        public override void Accept(PKFieldVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Render(PKGraphics graphics)
        {
            PKPoint absPosition = GetAbsPosition();
            base.Render(graphics);
            
            graphics.FillEllipse(PKBrushes.GrayWhiteBrush, absPosition.X + this.Padding, absPosition.Y + 2,
                    18, 18);
            graphics.DrawEllipse(
                    PKPens.DarkGrayPen,
                    absPosition.X+this.Padding, absPosition.Y+2,
                    18, 18);
        }
    }
}
